using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using TacticalEditor.Helpers;
using TacticalEditor.Models.NavPoint;
using TacticalEditor.VisualObject.VisAerodrome;
using TacticalEditor.VisualObject.VisPpm;


namespace TacticalEditor.Send
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	class SendToSvvo
	{
		
		private UdpClient _sendClient;
		private RouteToIup _routeToIup = new RouteToIup();

		public SendToSvvo()
		{
			_sendClient = new UdpClient();
			EventsHelper.PpmCollectionEvent += PpmCollection;
			EventsHelper.ChangeAerodromeEvent += ChangeAerodrome;
			
		}

		
		private void ChangeAerodrome(AerodromePoint aerodromePoint)
		{
			SetAerodrome(aerodromePoint, _routeToIup.ArrivalAerodrome);
			SetAerodrome(aerodromePoint, _routeToIup.DepartureAerodrome);
		}

		private void SetAerodrome(AerodromePoint aerodromePoint, Aerodrome aerodrome)
		{
			Array.Copy(aerodromePoint.AerodromeInfo.RusName, aerodrome.Name, aerodrome.Name.Length);
			aerodrome.Runway = aerodromePoint.AerodromeInfo.Runway;
		}

		private void PpmCollection(PpmPoint[] ppmPoints)
		{
			_routeToIup.CountPoints = 0;

			for (int i = 0; i < ppmPoints.Length; i++)
			{
				if (ppmPoints[i] == null)
				{
					_routeToIup.NavigationPoints[i] = new NavigationPoint(){Type = -1};
					continue;
				}
				_routeToIup.CountPoints++;
				_routeToIup.NavigationPoints[i]= ppmPoints[i].NavigationPoint;
			}
		}

		List<byte> result = new List<byte>();

		public byte[] GetByteNp()
		{
			_routeToIup.Head = _routeToIup.SetHead("Route", "Route");
			result.Clear();
			result.AddRange(_routeToIup.Head);
			result.AddRange(BitConverter.GetBytes(_routeToIup.CountPoints));
			result.AddRange(ConvertHelper.ObjectToByte(_routeToIup.DepartureAerodrome));
			foreach (var rnp in _routeToIup.NavigationPoints)
			{
				var np = PrepareNavigationPoint(rnp);
				result.AddRange(ConvertHelper.ObjectToByte(np));
			}
			result.AddRange(ConvertHelper.ObjectToByte(_routeToIup.ArrivalAerodrome));
			byte[] bytes = result.ToArray();
			return bytes;
		}


		private NavigationPoint PrepareNavigationPoint(NavigationPoint navigationPoint)
		{
			var np = new NavigationPoint();
			Array.Copy(navigationPoint.Name, np.Name, np.Name.Length);
			np.Executable = navigationPoint.Executable;
			np.PrPro = navigationPoint.PrPro;
			np.Type = navigationPoint.Type;
			np.GeoCoordinate.H = navigationPoint.GeoCoordinate.H + 20;
			np.GeoCoordinate = navigationPoint.GeoCoordinate;
			np.Measure = navigationPoint.Measure;
			return np;
		}
	}
}
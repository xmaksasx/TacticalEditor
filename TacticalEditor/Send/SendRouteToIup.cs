using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using TacticalEditor.Helpers;
using TacticalEditor.Models;
using TacticalEditor.Models.NavPoint;
using TacticalEditor.VisualObject.VisAerodrome;
using TacticalEditor.VisualObject.VisPpm;

namespace TacticalEditor.Send
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class SendRouteToIup
    {
        private RouteToIup _routeToIup = new RouteToIup();
        private AerodromePoint _aerodromePoint = new AerodromePoint();

        public SendRouteToIup()
        {
            EventsHelper.PpmCollectionEvent += PpmCollection;
            EventsHelper.ChangeAerodromeEvent += ChangeAerodrome;
            _routeToIup.Head = new byte[]
            {
                82, 111, 117, 116, 101, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                82, 111, 117, 116, 101, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 16
            };
        }

        private void ChangeAerodrome(AerodromePoint aerodromePoint)
        {
	        _aerodromePoint = aerodromePoint;
            SetAerodrome(aerodromePoint, _routeToIup.ArrivalAerodrome);
            SetAerodrome(aerodromePoint, _routeToIup.DepartureAerodrome);
        }

        private void SetAerodrome(AerodromePoint aerodromePoint, Aerodrome aerodrome)
        {
            Array.Copy(ToBigEndian(aerodromePoint.AerodromeInfo.RusName), aerodrome.Name, aerodrome.Name.Length);
            aerodrome.Runway = aerodromePoint.AerodromeInfo.Runway;
        }

        private void PpmCollection(PpmPoint[] ppmPoints)
        {
            _routeToIup.CountPoints = 0;

            for (int i = 0; i < ppmPoints.Length; i++)
            {
                if (ppmPoints[i] == null) continue;
                    _routeToIup.CountPoints++;
                _routeToIup.NavigationPoints[i] = ppmPoints[i].NavigationPoint;

            }
        }

        List<byte> result = new List<byte>();

        public byte[] GetByteNp()
        {
            result.Clear();
            result.AddRange(_routeToIup.Head);
            result.AddRange(BitConverter.GetBytes(_routeToIup.CountPoints));
          

            foreach (var rnp in _routeToIup.NavigationPoints)
            {
                if (rnp == null)
                {
                    var np = PrepareNavigationPoint(new NavigationPoint {Type = -1});
                    result.AddRange(ConvertHelper.ObjectToByte(np));
                }
                else
                {
                    var np = PrepareNavigationPoint(rnp);
                    result.AddRange(ConvertHelper.ObjectToByte(np));
                }
            }
            byte[] bytes = result.ToArray();

            for (int i = 68; i < bytes.Length; i += 8)
                Array.Reverse(bytes, i, 8);
            return bytes;
        }

        public byte[] GetByteAerodromes()
        {
            result.Clear();
            result.AddRange(new byte[]
            {
          
                65, 105, 114, 112, 111, 114, 116, 115, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                65, 105, 114, 112, 111, 114, 116, 115, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 16
            });
            _routeToIup.DepartureAerodrome.Runway = _aerodromePoint.AerodromeInfo.Runway;
            _routeToIup.ArrivalAerodrome.Runway = _aerodromePoint.AerodromeInfo.Runway;
            result.AddRange(ConvertHelper.ObjectToByte(_routeToIup.DepartureAerodrome));
       //     result.AddRange(ConvertHelper.ObjectToByte(_routeToIup.ArrivalAerodrome));
            byte[] bytes = result.ToArray();// ConvertHelper.ObjectToByte(_routeToIup);

            for (int i = 68; i < bytes.Length; i += 8)
                Array.Reverse(bytes, i, 8);
            return bytes;
        }

        private NavigationPoint PrepareNavigationPoint(NavigationPoint navigationPoint)
        {
            var np = new NavigationPoint();
            Array.Copy(ToBigEndian(navigationPoint.Name), np.Name, np.Name.Length);
            np.Executable = navigationPoint.Executable;
            np.PrPro = navigationPoint.PrPro;
            np.Type = navigationPoint.Type;
            np.GeoCoordinate = navigationPoint.GeoCoordinate;
            np.Measure = navigationPoint.Measure;
            return np;
        }

        private byte[] ToBigEndian(char[] str)
        {
            var nameBytes = Encoding.BigEndianUnicode.GetBytes(new string(str).Trim('\0'));
            var bytes = new byte[str.Length];
            nameBytes.CopyTo(bytes, 0);
            for (int i = 0; i < bytes.Length; i = i + 8)
                Array.Reverse(bytes, i, 8);
            return bytes;
        }
    }



    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class RouteToIup : Header
    {
        public double CountPoints;
        public Aerodrome DepartureAerodrome = new Aerodrome();
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public NavigationPoint[] NavigationPoints = new NavigationPoint[20];
        public Aerodrome ArrivalAerodrome = new Aerodrome();
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class Aerodrome 
    {
        /// <summary>
        /// Название аэропорта на кириллице
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        public char[] Name = new char[40];

        /// <summary>
        /// Информация о взлетной полосе
        /// </summary>
        public RunwayInfo Runway;
        
    }


}

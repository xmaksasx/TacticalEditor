using System;
using System.IO;
using System.Xml.Serialization;
using TacticalEditor.Models;
using TacticalEditor.Models.NavPoint;

namespace TacticalEditor.Helpers
{
	class RouteHelper
	{
		private readonly Route _route;

		private static RouteHelper _instance;

		private RouteHelper()
		{
			_route = new Route();
		}

		public static RouteHelper GetInstance()
		{
			return _instance ?? (_instance = new RouteHelper());
		}

		public void AddDepartureAerodrome(Guid guid)
		{
			_route.DepartureAerodrome = guid.ToString();
		}

		public void AddArrivalAerodrome(Guid guid)
		{
			_route.ArrivalAerodrome = guid.ToString();
		}

		public void AddNavigationPoint(NavigationPoint navigationPoint)
		{
			_route.NavigationPoints.Add(navigationPoint);
		}

		public void SaveRoute(string path)
		{
			using(var writer = new StreamWriter(path))
			{
			    var serializer = new XmlSerializer(typeof(Route));
			    serializer.Serialize(writer, _route);
			    writer.Flush();
			}
		}

		public Route LoadRoute(string path)
		{
			ClearRoute();
			XmlSerializer serializer = new XmlSerializer(typeof(Route));
			using(FileStream fileStream = new FileStream(path, FileMode.Open))
				return (Route)serializer.Deserialize(fileStream);
	
		}

		public void ClearRoute()
		{
			_route.DepartureAerodrome = "";
			_route.NavigationPoints.Clear();
			_route.ArrivalAerodrome = "";
		}
	}
}

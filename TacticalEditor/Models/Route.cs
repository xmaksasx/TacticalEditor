using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TacticalEditor.Models.NavPoint;

namespace TacticalEditor.Models
{	
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public class Route
	{
		public Route()
		{
			NavigationPoints = new List<NavigationPoint>();
		}

		public string DepartureAerodrome;
		public List<NavigationPoint> NavigationPoints;
		public string ArrivalAerodrome;
    }
}

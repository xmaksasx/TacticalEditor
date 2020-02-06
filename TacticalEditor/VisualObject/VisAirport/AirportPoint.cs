﻿using TacticalEditor.Models;
using TacticalEditor.Models.NavPoint;

namespace TacticalEditor.VisualObject.VisAirport
{
    public class AirportPoint
    {
        public NavigationPoint NavigationPoint;

        public ScreenCoordinate Screen { get; set; }

        public AirportInfo AirportInfo;

        public AirportPoint()
        {
            Screen = new ScreenCoordinate();
            NavigationPoint = new NavigationPoint();
        }
    }
}

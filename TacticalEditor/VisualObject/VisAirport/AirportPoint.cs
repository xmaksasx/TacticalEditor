﻿using TacticalEditor.Models;
using TacticalEditor.Models.NavPoint;

namespace TacticalEditor.VisualObject.VisAirport
{
    public class AirportPoint
    {
        /// <summary>
        /// Курс взлетной полосы
        /// </summary>
        public double HeadingRunway { get; set; }

        /// <summary>
        /// Признак того что маршрут начинается с этого аэропорта
        /// </summary>
        public bool ActiveAirport { get; set; }

        /// <summary>
        /// номер навигационной точки в маршруте (для ППМ)
        /// </summary>
        public int NumberInRoute { get; set; }

        public NavigationPoint NavigationPoint;

        public ScreenCoordinate Screen { get; set; }

        public AirportPoint()
        {
            Screen = new ScreenCoordinate();
            NavigationPoint = new NavigationPoint();
        }
    }
}
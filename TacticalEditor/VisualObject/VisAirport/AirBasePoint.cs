using System;
using TacticalEditor.Models;
using TacticalEditor.Models.NavPoint;

namespace TacticalEditor.VisualObject.VisAirport
{
    public class AirBasePoint
    {

        public AirBaseInfo AirportInfo;

        /// <summary>
        /// Геодезические координаты
        /// </summary>
        public NavigationPoint NavigationPoint;
        
        /// <summary>
        /// Экранные координаты
        /// </summary>
        public ScreenCoordinate Screen;

        public Guid Guid;

        public AirBasePoint()
        {
            Screen = new ScreenCoordinate();
            NavigationPoint = new NavigationPoint();
            AirportInfo= new AirBaseInfo();
            Guid = Guid.NewGuid();
        }
    }
}

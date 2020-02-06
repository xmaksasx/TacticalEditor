using TacticalEditor.Models;
using TacticalEditor.Models.NavPoint;

namespace TacticalEditor.VisualObject.VisAirport
{
    public class AirportPoint
    {
        public AirportInfo AirportInfo;

        /// <summary>
        /// Геодезические координаты
        /// </summary>
        public NavigationPoint NavigationPoint;
        
        /// <summary>
        /// Экранные координаты
        /// </summary>
        public ScreenCoordinate Screen;

        public AirportPoint()
        {
            Screen = new ScreenCoordinate();
            NavigationPoint = new NavigationPoint();
        }
    }
}

using TacticalEditor.Models;
using TacticalEditor.Models.NavPoint;

namespace TacticalEditor.VisualObject.VisAircraft
{
    public class AircraftPoint
    {
        /// <summary>
        /// Геодезические координаты
        /// </summary>
        public NavigationPoint NavigationPoint;

        /// <summary>
        /// Экранные координаты
        /// </summary>
        public ScreenCoordinate Screen;

        public AircraftPoint()
        {
            Screen = new ScreenCoordinate();
            NavigationPoint = new NavigationPoint();
        }
    }
}

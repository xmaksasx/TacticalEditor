using TacticalEditor.Models;
using TacticalEditor.Models.NavPoint;

namespace TacticalEditor.VisualObject.VisPpm
{

    public class PpmPoint
    {
        /// <summary>
        /// номер навигационной точки в маршруте (для ППМ)
        /// </summary>
        public int NumberInRoute { get; set; }

        /// <summary>
        /// Геодезические координаты
        /// </summary>
        public NavigationPoint NavigationPoint;

        /// <summary>
        /// Экранные координаты
        /// </summary>
        public ScreenCoordinate Screen;
        public PpmPoint()
        {
            Screen = new ScreenCoordinate();
            NavigationPoint = new NavigationPoint();

        }
    }
}

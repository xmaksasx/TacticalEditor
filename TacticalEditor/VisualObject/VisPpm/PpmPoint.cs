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
        public ScreenCoordinate Screen { get; set; }
        public NavigationPoint NavigationPoint;
        public PpmPoint()
        {
            Screen = new ScreenCoordinate();
        }
    }
}

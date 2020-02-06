using TacticalEditor.Models;
using TacticalEditor.Models.NavPoint;

namespace TacticalEditor.VisualObject.VisAircraft
{
    public class AircraftPoint
    {
        public NavigationPoint NavigationPoint;

        public ScreenCoordinate Screen { get; set; }

        public AircraftPoint()
        {
            Screen = new ScreenCoordinate();
            NavigationPoint = new NavigationPoint();
        }
    }
}

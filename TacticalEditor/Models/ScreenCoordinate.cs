using System.Windows.Shapes;

namespace TacticalEditor.Models
{
    public class ScreenCoordinate
    {
        public Line RouteLineIn { get; set; }
        public Line RouteLineOut { get; set; }
        public uint SizeMap { get; set; }
        public double RelativeX { get; set; }
        public double RelativeY { get; set; }
    }
}

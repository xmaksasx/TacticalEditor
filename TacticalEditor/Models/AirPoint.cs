using System.Runtime.InteropServices;

namespace TacticalEditor.Models
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class AirPoint 
    {
        /// <summary>
        /// Тип точки маршрута
        /// </summary>
        public long Type;

        public GeodesicCoordinate GeodesicCoordinate;
    }
}

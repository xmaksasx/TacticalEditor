using System.Runtime.InteropServices;

namespace TacticalEditor.Models
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class InfoPoint 
    {
        /// <summary>
        /// Тип точки маршрута
        /// </summary>
        public long Type;

        public GeoCoordinate GeoCoordinate;
    }
}

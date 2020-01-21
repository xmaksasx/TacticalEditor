using System.Runtime.InteropServices;

namespace TacticalEditor.Models.NavPoint
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct GeoCoordinate
    {
        /// <summary>
        /// широта [град]
        /// </summary>
        public double Lat { get; set; }

        /// <summary>
        /// долгота [град]
        /// </summary>
        public double Lon { get; set; }

        /// <summary>
        /// координата X от ЛА [км]
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// координата Z от ЛА [км]
        /// </summary>
        public double Z { get; set; }

        /// <summary>
        /// высота навигационной точки над уровнем моря [м]
        /// </summary>
        public double H { get; set; }
    }
}

using System;
using System.Runtime.InteropServices;

namespace TacticalEditor.Models
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class GeodesicCoordinate
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

        /// <summary>
        /// угол курса относительно ЛА
        /// </summary>
        public double Course { get; set; }

        /// <summary>
        /// угол курса относительно ЛА
        /// </summary>
        public double Distance { get; set; }
    }
}

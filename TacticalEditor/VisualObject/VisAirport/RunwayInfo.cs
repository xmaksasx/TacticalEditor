using System.Runtime.InteropServices;

namespace TacticalEditor.VisualObject.VisAirport
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RunwayInfo
    {
        /// <summary>
        /// координата X от ЛА [км]
        /// </summary>
        public decimal X;

        /// <summary>
        /// координата Z от ЛА [км]
        /// </summary>
        public decimal Y;

        /// <summary>
        /// Курс ВПП
        /// </summary>
        public double Heading;

        /// <summary>
        /// Длина ВПП
        /// </summary>
        public decimal Length;

        /// <summary>
        /// Ширина ВПП
        /// </summary>
        public decimal Width;

        /// <summary>
        /// Широта начала ВПП
        /// </summary>
        public double Latitude;

        /// <summary>
        /// Долгота начала ВПП
        /// </summary>
        public double Longitude;

    }
}

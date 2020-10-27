using System.Runtime.InteropServices;
using TacticalEditor.Models.NavPoint;

namespace TacticalEditor.VisualObject.VisAirport
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RunwayInfo
    {
        ///// <summary>
        ///// координата X от ЛА [км]
        ///// </summary>
        //public double X;

        ///// <summary>
        ///// координата Z от ЛА [км]
        ///// </summary>
        //public double Y;

        /// <summary>
        /// Курс ВПП
        /// </summary>
        public double Heading;

        /// <summary>
        /// Длина ВПП
        /// </summary>
        public double Length;

        /// <summary>
        /// Ширина ВПП
        /// </summary>
        public double Width;

        ///// <summary>
        ///// Широта начала ВПП
        ///// </summary>
        //public double Latitude;

        ///// <summary>
        ///// Долгота начала ВПП
        ///// </summary>
        //public double Longitude;

        /// <summary>
        /// Торец ВПП
        /// </summary>
        public GeoCoordinate Threshold;

        /// <summary>
        /// Глиссадный маяк
        /// </summary>
        public GeoCoordinate GlideSlope;

        /// <summary>
        /// Курсовой маяк
        /// </summary>
        public GeoCoordinate Localizer;

        /// <summary>
        /// БПРМ
        /// </summary>
        public GeoCoordinate LocatorMiddle;

        /// <summary>
        /// ДПРМ
        /// </summary>
        public GeoCoordinate LocatorOuter;

    }
}

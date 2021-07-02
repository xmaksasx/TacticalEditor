using System.Runtime.InteropServices;
using TacticalEditor.Models.NavPoint;

namespace TacticalEditor.VisualObject.VisAerodrome
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class RunwayInfo
    {
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

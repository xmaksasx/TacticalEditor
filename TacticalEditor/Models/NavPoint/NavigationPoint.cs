using System.Runtime.InteropServices;

namespace TacticalEditor.Models.NavPoint
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class NavigationPoint
    {
        /// <summary>
        /// 1-АЭР, 2-ППМ
        /// </summary>
        public double TypePpm;

        /// <summary>
        /// признак исполняемой НТ 0-нет, 1-да
        /// </summary>
        public double Executable;


        /// <summary>
        /// признак прохода
        /// </summary>
        public double PrPro;

        /// <summary>
        /// Геодезические координаты
        /// </summary>
        public GeoCoordinate GeoCoordinate;

        /// <summary>
        /// Рассчетные параметры 
        /// </summary>
        public Measure Measure;
    }
}

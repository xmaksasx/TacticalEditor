using System.Runtime.InteropServices;

namespace TacticalEditor.VisualObject.VisAirport
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AirportInfo
    {
        /// <summary>
        /// Название аэропорта на кириллице
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public char[] Name;

        /// <summary>
        /// Название аэропорта на латинице
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public char[] RusName;

        /// <summary>
        /// Название страны
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public char[] Country;

        /// <summary>
        /// Признак того что маршрут начинается с этого аэропорта
        /// </summary>
        public bool ActiveAirport;

        /// <summary>
        /// Информация о взлетной полосе
        /// </summary>
        public RunwayInfo Runway;


    }
}

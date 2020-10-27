using System.Runtime.InteropServices;

namespace TacticalEditor.VisualObject.VisAirport
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class  AirBaseInfo 
    {
        /// <summary>
        /// Название аэропорта на кириллице
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public char[] Name = new char[20];

        /// <summary>
        /// Название аэропорта на латинице
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public char[] RusName = new char[20];

        /// <summary>
        /// Название страны
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public char[] Country = new char[20];

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

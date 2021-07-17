using System.Runtime.InteropServices;


namespace TacticalEditor.VisualObject.VisAerodrome
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class AerodromeInfo
    {
	    public AerodromeInfo()
	    {
		    Runway = new RunwayInfo();
	    }
        /// <summary>
        /// Название аэропорта на кириллице
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        public char[] Name = new char[40];

        /// <summary>
        /// Название аэропорта на латинице
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        public char[] RusName = new char[40];

        /// <summary>
        /// Название страны
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        public char[] Country = new char[40];

        /// <summary>
        /// Признак того что маршрут начинается с этого аэропорта
        /// </summary>
        public bool ActiveAerodrome;

        /// <summary>
        /// Информация о взлетной полосе
        /// </summary>
        public RunwayInfo Runway;


    }
}

using System;
using TacticalEditor.Models.NavPoint;


namespace TacticalEditor.VisualObject.VisAerodrome
{
    public class AerodromePoint
    {

        public AerodromeInfo AerodromeInfo;

        /// <summary>
        /// Геодезические координаты
        /// </summary>
        public NavigationPoint NavigationPoint;
        
        /// <summary>
        /// Экранные координаты
        /// </summary>
        public ScreenCoordinate Screen;

        public Guid Guid;

        public AerodromePoint()
        {
            Screen = new ScreenCoordinate();
            NavigationPoint = new NavigationPoint();
            AerodromeInfo = new AerodromeInfo();
        }
    }
}

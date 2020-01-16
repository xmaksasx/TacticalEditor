namespace TacticalEditor.Models.Points
{

    public class PpmPoint
    {
        /// <summary>
        /// номер навигационной точки в маршруте (для ППМ)
        /// </summary>
        public int NumberInRoute { get; set; }
        public ScreenCoordinate Screen { get; set; }
        public AirPoint AirPoint; 
        public PpmPoint()
        {
            Screen = new ScreenCoordinate();
        }
    }
}

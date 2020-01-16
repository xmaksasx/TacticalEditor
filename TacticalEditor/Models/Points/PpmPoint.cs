namespace TacticalEditor.Models.Points
{

    public class PpmPoint: AirPoint
    {
        /// <summary>
        /// номер навигационной точки в маршруте (для ППМ)
        /// </summary>
        public int NumberInRoute { get; set; }
        public ScreenCoordinate Screen { get; set; }

        public PpmPoint()
        {
            Screen = new ScreenCoordinate();
        }
    }
}

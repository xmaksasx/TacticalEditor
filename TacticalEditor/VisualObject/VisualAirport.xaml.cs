using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TacticalEditor.Helpers;
using TacticalEditor.Models;
using TacticalEditor.Models.Points;

namespace TacticalEditor.VisualObject
{
    /// <summary>
    /// Interaction logic for Airport.xaml
    /// </summary>
    public partial class VisualAirport
    {
        private readonly AirportPoint _airportPoint;
        private CoordinateHelper _coordinateHelper;

        public VisualAirport(AirportPoint airportPoint)
        {
            InitializeComponent();
            EventsHelper.ChangeAirportEvent += ChangeAirportEvent;
            _coordinateHelper = new CoordinateHelper();
            _airportPoint = airportPoint;
            PrepareRouteLine(_airportPoint);
            PrepareAirport(_airportPoint);
            EventsHelper.ChangeOfSizeEvent += ChangeOfSize;
        }
        private void PrepareRouteLine(AirportPoint airportPoint)
        {
            airportPoint.Screen.RouteLineOut = new Line();
            airportPoint.Screen.RouteLineOut.X1 = airportPoint.Screen.RelativeX;
            airportPoint.Screen.RouteLineOut.Y1 = airportPoint.Screen.RelativeY;
            airportPoint.Screen.RouteLineOut.Stroke = Brushes.Black;
            airportPoint.Screen.RouteLineOut.StrokeThickness = 1;
        }
   
        private void PrepareAirport(AirportPoint airportPoint)
        {
           Heading.Angle = _airportPoint.HeadingRunway;
           if (_airportPoint.ActiveAirport)
               VisualAirport_OnPreviewMouseDoubleClick(null, null);
        }

        private void ChangeAirportEvent(AirportPoint e)
        {
            _airportPoint.ActiveAirport = false;
            El.Stroke = new SolidColorBrush(Colors.Black);
            El.StrokeThickness = 0.6;
        }

        private void ChangeOfSize(uint sizeMap)
        {
            _coordinateHelper.LatLonToPixel(_airportPoint.Lat, _airportPoint.Lon, sizeMap, out var px, out var py);
            _airportPoint.Screen.RouteLineOut.X1 = px;
            _airportPoint.Screen.RouteLineOut.Y1 = py;
            Canvas.SetLeft(this, px);
            Canvas.SetTop(this, py);
        }

        private void VisualAirport_OnPreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EventsHelper.OnChangeAirportEvent(_airportPoint);
            _airportPoint.ActiveAirport = true;
            El.Stroke = new SolidColorBrush(Colors.LimeGreen);
            El.StrokeThickness = 1.5;
        }
    }
}
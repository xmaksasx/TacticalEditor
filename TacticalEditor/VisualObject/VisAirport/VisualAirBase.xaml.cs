using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TacticalEditor.Helpers;
using TacticalEditor.Models;

namespace TacticalEditor.VisualObject.VisAirport
{
    /// <summary>
    /// Interaction logic for Airport.xaml
    /// </summary>
    public partial class VisualAirBase
    {
        private readonly AirBasePoint _airportPoint;
        private CoordinateHelper _coordinateHelper;

        public VisualAirBase(AirBasePoint airportPoint)
        {
            InitializeComponent();
            EventsHelper.ChangeAirportEvent += ChangeAirportEvent;
            EventsHelper.ChangeOfSizeEvent += ChangeOfSize;
            EventsHelper.ChangeNpDEvent += ChangeNpDEvent;
            _coordinateHelper = new CoordinateHelper();
            _airportPoint = airportPoint;
            PrepareRouteLine(_airportPoint);
            PrepareAirport(_airportPoint);

        }

        private void ChangeNpDEvent(ChangeNp changeNp)
        {
        }
        private void PrepareRouteLine(AirBasePoint airportPoint)
        {
            airportPoint.Screen.LineOut = new Line();
            airportPoint.Screen.LineOut.X1 = airportPoint.Screen.RelativeX;
            airportPoint.Screen.LineOut.Y1 = airportPoint.Screen.RelativeY;
            airportPoint.Screen.LineOut.Stroke = Brushes.Black;
            airportPoint.Screen.LineOut.StrokeThickness = 1;
        }
   
        private void PrepareAirport(AirBasePoint airportPoint)
        {
           Heading.Angle = _airportPoint.AirportInfo.Runway.Heading;
           if (_airportPoint.AirportInfo.ActiveAirport)
               VisualAirport_OnPreviewMouseDoubleClick(null, null);
        }

        private void ChangeAirportEvent(AirBasePoint e)
        {
            if (e.Guid !=_airportPoint.Guid)
            {
                _airportPoint.AirportInfo.ActiveAirport = false;
                El.Stroke = new SolidColorBrush(Colors.Black);
                El.StrokeThickness = 0.6;
            }

        }

        private void ChangeOfSize(uint sizeMap)
        {
            _coordinateHelper.LatLonToPixel(_airportPoint.NavigationPoint.GeoCoordinate.Latitude, _airportPoint.NavigationPoint.GeoCoordinate.Longitude, sizeMap, out var px, out var py);
            _airportPoint.Screen.LineOut.X1 = px;
            _airportPoint.Screen.LineOut.Y1 = py;
            Canvas.SetLeft(this, px);
            Canvas.SetTop(this, py);
        }

        private void VisualAirport_OnPreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _airportPoint.AirportInfo.ActiveAirport = true;
            El.Stroke = new SolidColorBrush(Colors.LimeGreen);
            El.StrokeThickness = 1.5;
            EventsHelper.OnChangeAirportEvent(_airportPoint);
            EventsHelper.OnOutLineFromLastPoint(_airportPoint.Screen.LineOut);
        }
    }
}
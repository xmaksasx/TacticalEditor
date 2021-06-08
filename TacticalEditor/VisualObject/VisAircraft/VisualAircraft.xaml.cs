using System.Windows.Controls;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using TacticalEditor.Helpers;
using TacticalEditor.Models;
using TacticalEditor.VisualObject.VisAerodrome;


namespace TacticalEditor.VisualObject.VisAircraft
{
    /// <summary>
    /// Interaction logic for Aircraft.xaml
    /// </summary>
    public partial class VisualAircraft
    {
        private readonly AircraftPoint _aircraftPoint;
        private CoordinateHelper _coordinateHelper;
        private AerodromePoint _aerodromePoint;
        public VisualAircraft(AircraftPoint aircraftPoint, AerodromePoint aerodromePoint)
        {
            InitializeComponent();
            _aircraftPoint = aircraftPoint;
            _aerodromePoint = aerodromePoint;
            _coordinateHelper = new CoordinateHelper();
            Prepare3DModel();
            EventsHelper.ChangeOfSizeEvent += ChangeOfSize;
            EventsHelper.ChangeAircraftCoordinateEvent += ChangeAircraftCoordinate;
            EventsHelper.ChangeAerodromeEvent += ChangeAerodrome;

        }

        private void ChangeAerodrome(AerodromePoint e)
        {
            _aerodromePoint = e;
            _coordinateHelper.LatLonToPixel(e.NavigationPoint.GeoCoordinate.Latitude, e.NavigationPoint.GeoCoordinate.Longitude,  out var px, out var py);
            zzz.Angle = 360 - e.AerodromeInfo.Runway.Heading;
            Dispatcher.Invoke(() => Canvas.SetLeft(this, px));
            Dispatcher.Invoke(() => Canvas.SetTop(this, py));
        }

        private void ChangeAircraftCoordinate(AircraftPosition aircraft)
        {
            _coordinateHelper.LatLonToPixel(aircraft.GeoCoordinate.Latitude, aircraft.GeoCoordinate.Longitude,  out var px, out var py);
            Dispatcher.Invoke(() => Canvas.SetLeft(this, px));
            Dispatcher.Invoke(() => Canvas.SetTop(this, py));
            Dispatcher.Invoke(() => zzz.Angle = 360 - aircraft.Risk);
            Dispatcher.Invoke(() => l1.Content = px);
            Dispatcher.Invoke(() => l2.Content = py);
        }

        private void Prepare3DModel()
        {
            ObjReader helixObjReader = new ObjReader();
            var modelAircraft3D = helixObjReader.Read(@"untitled8.obj");
            model.Content = modelAircraft3D;
            model.Children.Add(new DefaultLights());
            myView.Camera.UpDirection = new Vector3D(0, 1, 0);
            myView.Camera.LookDirection = new Vector3D(0, 0, -100);
            myView.Camera.Position = new Point3D(0, 0, 100);
        }

        private void ChangeOfSize()
        {
            _coordinateHelper.LatLonToPixel(_aircraftPoint.NavigationPoint.GeoCoordinate.Latitude, _aircraftPoint.NavigationPoint.GeoCoordinate.Longitude,  out var px, out var py);
            zzz.Angle = 360 -_aircraftPoint.NavigationPoint.Measure.Psi;
            Canvas.SetLeft(this, px);
            Canvas.SetTop(this, py);
        }
    }
}

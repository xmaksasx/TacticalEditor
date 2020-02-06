using System.Windows.Controls;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using TacticalEditor.Helpers;
using TacticalEditor.VisualObject.VisAircraft;

namespace TacticalEditor.VisualObject.VisAirCraft
{
    /// <summary>
    /// Interaction logic for Aircraft.xaml
    /// </summary>
    public partial class VisualAircraft : UserControl
    {
        private readonly AircraftPoint _aircraftPoint;
        private CoordinateHelper _coordinateHelper;
        public VisualAircraft(AircraftPoint aircraftPoint)
        {
            InitializeComponent();
            _aircraftPoint = aircraftPoint;
            _coordinateHelper = new CoordinateHelper();
            Prepare3DModel();
            EventsHelper.ChangeOfSizeEvent += ChangeOfSize;
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

        private void ChangeOfSize(uint sizeMap)
        {
            _aircraftPoint.Screen.SizeMap = sizeMap;
            _coordinateHelper.LatLonToPixel(_aircraftPoint.NavigationPoint.GeoCoordinate.Latitude, _aircraftPoint.NavigationPoint.GeoCoordinate.Longitude, sizeMap, out var px, out var py);
            zzz.Angle = _aircraftPoint.NavigationPoint.Measure.Psi*-1;
            Canvas.SetLeft(this, px);
            Canvas.SetTop(this, py);
        }
    }
}

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TacticalEditor.Helpers;
using TacticalEditor.VisualObject.VisPpm;


namespace TacticalEditor.VisualObject
{
    /// <summary>
    /// Interaction logic for VisualNavigationPoint.xaml
    /// </summary>
    public partial class VisualPpm
    {
        private PpmPoint _ppmPoint;
        private CoordinateHelper _coordinateHelper;
        private bool _isDragging;
        private MenuStates _stateMenu;

        public VisualPpm(PpmPoint ppmPoint)
        {
            InitializeComponent();
            _ppmPoint = ppmPoint;
            NumberInRoute.Text = _ppmPoint.NumberInRoute.ToString();
            _coordinateHelper = new CoordinateHelper();
            EventsHelper.MenuStatusEvent += StatePpmEvent;
            EventsHelper.ChangeOfSizeEvent += ChangeOfSize;
           PrepareRouteLine(ppmPoint);
        }
        
        private void PrepareRouteLine(PpmPoint ppmPoint)
        {
            _ppmPoint.Screen.RouteLineIn.X2 = ppmPoint.Screen.RelativeX * ppmPoint.Screen.SizeMap;
            _ppmPoint.Screen.RouteLineIn.Y2 = ppmPoint.Screen.RelativeY * ppmPoint.Screen.SizeMap;
            _ppmPoint.Screen.RouteLineIn.Stroke = Brushes.Black;
            _ppmPoint.Screen.RouteLineIn.StrokeThickness = 1;
            _ppmPoint.Screen.RouteLineOut = new Line();
            _ppmPoint.Screen.RouteLineOut.X1 = ppmPoint.Screen.RelativeX * ppmPoint.Screen.SizeMap;
            _ppmPoint.Screen.RouteLineOut.Y1 = ppmPoint.Screen.RelativeY * ppmPoint.Screen.SizeMap;
            _ppmPoint.Screen.RouteLineOut.Stroke = Brushes.Black;
            _ppmPoint.Screen.RouteLineOut.StrokeThickness = 1;

            EventsHelper.OnAddLineToRoute(ppmPoint.Screen.RouteLineIn, _ppmPoint.Screen.RouteLineOut);
        }

        private void ChangeOfSize(uint sizeMap)
        {
            _ppmPoint.Screen.SizeMap = sizeMap;

            _ppmPoint.Screen.RouteLineIn.X2 =  _ppmPoint.Screen.RelativeX * sizeMap;
            _ppmPoint.Screen.RouteLineIn.Y2 =  _ppmPoint.Screen.RelativeY * sizeMap;
            _ppmPoint.Screen.RouteLineOut.X1 = _ppmPoint.Screen.RelativeX * sizeMap;
            _ppmPoint.Screen.RouteLineOut.Y1 = _ppmPoint.Screen.RelativeY * sizeMap;

            Canvas.SetLeft(this, _ppmPoint.Screen.RelativeX * sizeMap);
            Canvas.SetTop(this, _ppmPoint.Screen.RelativeY * sizeMap);
        }

        private void StatePpmEvent(MenuStates e)
        {
            _stateMenu = e;
        }

        private void VisualPoint_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_stateMenu == MenuStates.Edit)
            {
                El.Stroke = new SolidColorBrush(Colors.Red);
                _isDragging = true;
                CaptureMouse();
            }
        }

        private void VisualPoint_OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDragging) return;
            var point = e.GetPosition(Parent as UIElement);
            _coordinateHelper.PixelToLatLon(point, _ppmPoint.Screen.SizeMap, out var lat, out var lon);
            _ppmPoint.NavigationPoint.GeoCoordinate.Lat = lat;
            _ppmPoint.NavigationPoint.GeoCoordinate.Lon = lon;
            _ppmPoint.Screen.RelativeX = point.X / _ppmPoint.Screen.SizeMap;
            _ppmPoint.Screen.RelativeY = point.Y / _ppmPoint.Screen.SizeMap;

            _ppmPoint.Screen.RouteLineIn.X2 = point.X;
            _ppmPoint.Screen.RouteLineIn.Y2 = point.Y;

            _ppmPoint.Screen.RouteLineOut.X1 = point.X;
            _ppmPoint.Screen.RouteLineOut.Y1 = point.Y;

            Canvas.SetLeft(this, point.X);
            Canvas.SetTop(this, point.Y);
        }

        private void VisualPoint_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isDragging = false;
            ReleaseMouseCapture();
            El.Stroke = new SolidColorBrush(Colors.Black);
        }
    }
}

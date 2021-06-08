using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TacticalEditor.Helpers;
using TacticalEditor.Models;

namespace TacticalEditor.VisualObject.VisPpm
{
    /// <summary>
    /// Interaction logic for VisualNavigationPoint.xaml
    /// </summary>
    public partial class VisualPpm
    {
	    private RouteHelper _routeHelper;
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
            _routeHelper = RouteHelper.GetInstance();
            _routeHelper.AddNavigationPoint(_ppmPoint.NavigationPoint);
            EventsHelper.MenuStatusEvent += StatePpmEvent;
            EventsHelper.ChangeOfSizeEvent += ChangeOfSize;
			EventsHelper.ChangeNpDEvent += ChangeNpDEvent;
            PrepareRouteLine(ppmPoint);
            ChangeNpDEvent(new ChangeNp() {Action = 1, TypeOfNp = 2, IdNp = 1});
        }

        private void ChangeNpDEvent(ChangeNp changeNp)
        {
	        if (changeNp.TypeOfNp != _ppmPoint.NavigationPoint.Type)
	        {
		        SetColorNonActivePpm();
		        return;
	        }
            
	        if (changeNp.Action == 1)
		        if (changeNp.IdNp == _ppmPoint.NumberInRoute)
			        SetColorActivePpm();
		        else
			        SetColorNonActivePpm();
        }

        private void SetColorActivePpm()
		{
			_ppmPoint.NavigationPoint.Executable = 1;
            Dispatcher.Invoke(() => El.Fill = new SolidColorBrush(Color.FromArgb(35, 35, 75, 255)));
			Dispatcher.Invoke(() => El.Stroke = new SolidColorBrush(Colors.DarkViolet));
			Dispatcher.Invoke(() => El.StrokeThickness = 1.5);
		}

		private void SetColorNonActivePpm()
		{
			_ppmPoint.NavigationPoint.Executable = 0;
            Dispatcher.Invoke(() => El.Fill = new SolidColorBrush(Color.FromArgb(35, 155, 155, 155)));
			Dispatcher.Invoke(() => El.Stroke = new SolidColorBrush(Colors.Black));
			Dispatcher.Invoke(() => El.StrokeThickness = 0.6);
        }

		private void PrepareRouteLine(PpmPoint ppmPoint)
		{
			UpdateStartOfLine(_ppmPoint.Screen.LineIn);
			_ppmPoint.Screen.LineOut = new Line();
			UpdateEndOfLine(_ppmPoint.Screen.LineOut);
			EventsHelper.OnAddVisualLine(ppmPoint.Screen.LineIn);
			EventsHelper.OnOutLineFromLastPoint(_ppmPoint.Screen.LineOut);
		}

		private Point GetXy()
		{
			var lat = _ppmPoint.NavigationPoint.GeoCoordinate.Latitude;
			var lon = _ppmPoint.NavigationPoint.GeoCoordinate.Longitude;
			_coordinateHelper.LatLonToPixel(lat, lon, out var px, out var py);
			return new Point(px, py);
		}

		private void UpdateStartOfLine(Line line)
        {
	       var p = GetXy();
           line.X2 = p.X;
	       line.Y2 = p.Y;
	       line.Stroke = Brushes.Black;
	       line.StrokeThickness = 1;
        }

		private void UpdateEndOfLine(Line line)
		{
			var p = GetXy();
			line.X1 = p.X;
			line.Y1 = p.Y;
			line.Stroke = Brushes.Black;
			line.StrokeThickness = 1;
		}

        private void ChangeOfSize()
        {
	        var p = GetXy();
	        UpdateStartOfLine(_ppmPoint.Screen.LineIn);
	        UpdateEndOfLine(_ppmPoint.Screen.LineOut);
            Canvas.SetLeft(this, p.X);
            Canvas.SetTop(this, p.Y);
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
            _coordinateHelper.PixelToLatLon(point, out var lat, out var lon);
            _ppmPoint.NavigationPoint.GeoCoordinate.Latitude = lat;
            _ppmPoint.NavigationPoint.GeoCoordinate.Longitude = lon;
            _ppmPoint.Screen.LineIn.X2 = point.X;
            _ppmPoint.Screen.LineIn.Y2 = point.Y;
            _ppmPoint.Screen.LineOut.X1 = point.X;
            _ppmPoint.Screen.LineOut.Y1 = point.Y;
            Canvas.SetLeft(this, point.X);
            Canvas.SetTop(this, point.Y);
        }

        private void VisualPoint_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isDragging = false;
            ReleaseMouseCapture();
            if (_ppmPoint.NavigationPoint.Executable == 1)
	            SetColorActivePpm();
            else
	            SetColorNonActivePpm();
        }

		private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			EventsHelper.OnChangeNpDEvent(new ChangeNp() { Action = 1, TypeOfNp = 2, IdNp = _ppmPoint.NumberInRoute });
        }
	}
   
}

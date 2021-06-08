using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TacticalEditor.Helpers;
using TacticalEditor.Models;

namespace TacticalEditor.VisualObject.VisAerodrome
{

    public partial class VisualAerodrome
    {
        private readonly AerodromePoint _aerodromePoint;
        private CoordinateHelper _coordinateHelper;
        private RouteHelper _routeHelper;

        public VisualAerodrome(AerodromePoint aerodromePoint)
        {
	        InitializeComponent();
	        EventsHelper.ChangeAerodromeEvent += ChangeAerodrome;
	        EventsHelper.ChangeOfSizeEvent += ChangeOfSize;
	        EventsHelper.ChangeNpDEvent += ChangeNpDEvent;
	        _coordinateHelper = new CoordinateHelper();
	        _routeHelper = RouteHelper.GetInstance();
	        _aerodromePoint = aerodromePoint;
	        PrepareRouteLine(_aerodromePoint);
	        PrepareAerodrome(_aerodromePoint);
        }

        private void ChangeNpDEvent(ChangeNp changeNp)
        {
        }
        private void PrepareRouteLine(AerodromePoint aerodromePoint)
        {
	        aerodromePoint.Screen.LineOut = new Line();
        }
   
        private void PrepareAerodrome(AerodromePoint aerodromePoint)
        {
           Heading.Angle = _aerodromePoint.AerodromeInfo.Runway.Heading;
           if (_aerodromePoint.AerodromeInfo.ActiveAerodrome)
                VisualAerodrome_OnPreviewMouseDoubleClick(null, null);
        }

        private void ChangeAerodrome(AerodromePoint e)
        {
	        if (e.Guid != _aerodromePoint.Guid)
	        {
		        _aerodromePoint.AerodromeInfo.ActiveAerodrome = false;
		        El.Stroke = new SolidColorBrush(Colors.Black);
		        El.StrokeThickness = 0.6;
	        }
        }

        private void ChangeOfSize()
        {
	        var p = GetXy();
            _aerodromePoint.Screen.LineOut.X1 = p.X;
            _aerodromePoint.Screen.LineOut.Y1 = p.Y; 
            Canvas.SetLeft(this, p.X);
            Canvas.SetTop(this, p.Y);
        }

        private Point GetXy()
        {
	        var lat = _aerodromePoint.NavigationPoint.GeoCoordinate.Latitude;
	        var lon = _aerodromePoint.NavigationPoint.GeoCoordinate.Longitude;
	        _coordinateHelper.LatLonToPixel(lat, lon, out var px, out var py);
	        return new Point(px, py);
        }

        private void VisualAerodrome_OnPreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _aerodromePoint.AerodromeInfo.ActiveAerodrome = true;
            _routeHelper.AddDepartureAerodrome(_aerodromePoint.Guid);
            El.Stroke = new SolidColorBrush(Colors.LimeGreen);
            El.StrokeThickness = 1.5;
            EventsHelper.OnChangeAerodromeEvent(_aerodromePoint);
            EventsHelper.OnOutLineFromLastPoint(_aerodromePoint.Screen.LineOut);
        }
    }
}
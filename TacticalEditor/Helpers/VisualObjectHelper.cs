using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using TacticalEditor.Models;
using TacticalEditor.VisualObject.VisAerodrome;
using TacticalEditor.VisualObject.VisAircraft;
using TacticalEditor.WorkingPoints;

namespace TacticalEditor.Helpers
{
	class VisualObjectHelper
	{
		private readonly Canvas _plotter;
		private readonly CoordinateHelper _coordinateHelper;
		private PpmWorker _ppmWorker;
		private AerodromeWorker _aerodromeWorker;
		private AircraftWorker _aircraftWorker;
		private AerodromePoint _currentAerodrome;


		public VisualObjectHelper(Canvas plotter)
		{
			_plotter = plotter;
			_plotter.SizeChanged += PlotterOnSizeChanged;
			EventsHelper.AddVisualLineEvent += AddVisualLine;
			EventsHelper.ChangeAerodromeEvent += ChangeAerodrome;
			_coordinateHelper = new CoordinateHelper();
			_ppmWorker = new PpmWorker();
			_aerodromeWorker = new AerodromeWorker();
			_aircraftWorker = new AircraftWorker();
			AddVisualAerodrome();
			AddVisualAircraft();
		}

		private void AddVisualAircraft()
		{
			var lat = _currentAerodrome.AerodromeInfo.Runway.Threshold.Latitude;
			var lon = _currentAerodrome.AerodromeInfo.Runway.Threshold.Longitude;
			_coordinateHelper.LatLonToPixel(lat, lon, out var px, out var py);
			AddVisualToPlotter(_aircraftWorker.CrateVisualAircraft(), new Point(px, py));
		}

		public void AddVisualPpm(Point point)
		{
			AddVisualToPlotter(_ppmWorker.CrateVisualPpm(point), point);
		}

		private void AddVisualAerodrome()
		{
			var aerodromeXmls = _aerodromeWorker.LoadAerodromeXml();
			foreach (var aerodromeXml in aerodromeXmls)
			{
				var visualAerodrome = _aerodromeWorker.CrateVisualAerodrome(aerodromeXml);
				_coordinateHelper.LatLonToPixel(aerodromeXml.Latitude, aerodromeXml.Longitude, out var px, out var py);
				AddVisualToPlotter(visualAerodrome, new Point(px, py));
			}
		}

		private void AddVisualLine(Line inLine)
		{
			_plotter.Children.Add(inLine);
		}

		private void AddVisualToPlotter(UIElement ui, Point point)
		{
			if (ui == null) return;
			_plotter.Children.Add(ui);
			Canvas.SetLeft(ui, point.X);
			Canvas.SetTop(ui, point.Y);
			Panel.SetZIndex(ui, 10);
			var t = ui as VisualAircraft;

			if (t != null)
			{
				Panel.SetZIndex(ui, 100);
			}
		}

		public void Clear()
		{
			_ppmWorker.Clear();
			for (int i = _plotter.Children.Count - 1; i >= 0; i--)
			{
				if (_plotter.Children[i] is VisualAircraft) continue;
				if (_plotter.Children[i] is VisualAerodrome) continue;
				_plotter.Children.Remove(_plotter.Children[i]);
			}

			EventsHelper.OnOutLineFromLastPoint(_currentAerodrome.Screen.LineOut);
		}

		private void ChangeAerodrome(AerodromePoint aerodromePoint)
		{
			_currentAerodrome = aerodromePoint;
		}

		private void PlotterOnSizeChanged(object sender, SizeChangedEventArgs e)
		{
			EventsHelper.OnChangeOfSizeEvent();
		}

		public void LoadRoute(Route route)
		{
			foreach (var np in route.NavigationPoints)
			{
				_coordinateHelper.LatLonToPixel(np.GeoCoordinate.Latitude, np.GeoCoordinate.Longitude, out var x,
					out var y);
				AddVisualPpm(new Point(x, y));
			}
		}
	}
}

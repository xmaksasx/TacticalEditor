using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Xml.Serialization;
using TacticalEditor.ModelsXml;
using TacticalEditor.VisualObject.VisAircraft;
using TacticalEditor.VisualObject.VisAirCraft;
using TacticalEditor.VisualObject.VisAirport;
using TacticalEditor.WorkingPoints;

namespace TacticalEditor.Helpers
{
    class VisualObjectHelper
    {
        private readonly Canvas _plotter;
        private readonly CoordinateHelper _coordinateHelper;
        private PpmWorker _ppmWorker;
        private AirBaseWorker _airBaseWorker;
        private RoutePoints _routePoints;
        private AirBasePoint _currentAirport;
        private double _lX =0;
        private double _lY = 0;


        public VisualObjectHelper(Canvas plotter)
        {
            _plotter = plotter;
            _plotter.SizeChanged += PlotterOnSizeChanged;
            EventsHelper.AddVisualLineEvent += AddVisualLine;
            EventsHelper.ChangeAirportEvent += ChangeAirportEvent;
            _coordinateHelper = new CoordinateHelper();
            _ppmWorker = new PpmWorker();
            _airBaseWorker = new AirBaseWorker();
            AddVisualAirBase();
            AddVisualAircraft();

            _routePoints = new RoutePoints();
        }
      
        #region Aircraft

        private void AddVisualAircraft()
        {
            var aircraftPoint = PrepareAircraftPoint(_currentAirport);
           AddVisualToPlotter(new VisualAircraft(aircraftPoint, _currentAirport), new Point(aircraftPoint.Screen.RelativeX, aircraftPoint.Screen.RelativeY));
        }

        private AircraftPoint PrepareAircraftPoint(AirBasePoint _currentAirport)
        {
            var sizeMap = (uint) _plotter.Height;
            AircraftPoint aircraftPoint = new AircraftPoint();
            aircraftPoint.Screen.SizeMap = sizeMap;
            aircraftPoint.Screen.RelativeX = _currentAirport.Screen.RelativeX;
            aircraftPoint.Screen.RelativeY = _currentAirport.Screen.RelativeY;
            aircraftPoint.NavigationPoint.GeoCoordinate.Latitude = _currentAirport.AirportInfo.Runway.Threshold.Latitude;
            aircraftPoint.NavigationPoint.GeoCoordinate.Longitude = _currentAirport.AirportInfo.Runway.Threshold.Longitude;
            aircraftPoint.NavigationPoint.Measure.Psi = _currentAirport.AirportInfo.Runway.Heading;
            //aircraftPoint.Screen.SizeMap = sizeMap;
            //aircraftPoint.Screen.RelativeX = (double) px / sizeMap;
            //aircraftPoint.Screen.RelativeY = (double) py / sizeMap;
            //aircraftPoint.NavigationPoint.GeoCoordinate.Latitude = airport.Runway.latitude;
            //aircraftPoint.NavigationPoint.GeoCoordinate.Longitude = airport.Runway.longitude;
            //aircraftPoint.NavigationPoint.Measure.Psi = airport.Runway.heading;
            //aircraftPoint.NavigationPoint.Type = 1;
            return aircraftPoint;
        }

        #endregion

        public void AddVisualPpm(Point point)
        {
            var sizeMap = (uint)_plotter.Height;
            _routePoints.PPM.Add(new Ppm { RelativeX = point.X / sizeMap, RelativeY = point.Y / sizeMap });
            AddVisualToPlotter(_ppmWorker.CrateVisualPpm(point), point);
        }


        public void BuildBox()
        {
            Point point;
            var sizeMap = (uint)_plotter.Height;
            _coordinateHelper.LocalCordToLatLon(_currentAirport.NavigationPoint.GeoCoordinate.Latitude, _currentAirport.NavigationPoint.GeoCoordinate.Longitude, -7100 * 1, 7100 * 1, out var LatT, out var LonT);

            GetLatLonOfPoint(_currentAirport.NavigationPoint.GeoCoordinate.Latitude,
                _currentAirport.NavigationPoint.GeoCoordinate.Longitude, 20000, 160, out double latLocator,
                out double lonLocator);

            _coordinateHelper.LatLonToPixel(LatT, LonT, sizeMap, out var PX, out var PY);
            point = new Point(PX, PY);
            _routePoints.PPM.Add(new Ppm { RelativeX = point.X / sizeMap, RelativeY = point.Y / sizeMap });
            AddVisualToPlotter(_ppmWorker.CrateVisualPpm(point), point);

        }

        private void GetLatLonOfPoint(double latThreshold, double lonThreshold, double distance, double courseThreshold, out double latLocator, out double lonLocator)
        {
            latLocator = (latThreshold + distance * Math.Cos(courseThreshold * Math.PI / 180) / (6371000 * Math.PI / 180));
            lonLocator = (lonThreshold + distance * Math.Sin(courseThreshold * Math.PI / 180) / Math.Cos(latThreshold * Math.PI / 180) / (6371000 * Math.PI / 180));
        }

        public void AddDebugPm()
        {
            Point point;
            var sizeMap = (uint)_plotter.Height;

               _coordinateHelper.LocalCordToLatLon(52.6618694, 39.4261806, -7100 * 1, 7100*1, out var LatT, out var LonT);
               _coordinateHelper.LatLonToPixel(LatT, LonT, sizeMap, out var PX, out var PY);
               point = new Point(PX, PY);
               _routePoints.PPM.Add(new Ppm { RelativeX = point.X / sizeMap, RelativeY = point.Y / sizeMap });
               AddVisualToPlotter(_ppmWorker.CrateVisualPpm(point), point);

               _coordinateHelper.LocalCordToLatLon(52.6618694, 39.4261806, -7100 * 2, 7100 * 2, out  LatT, out  LonT);
               _coordinateHelper.LatLonToPixel(LatT, LonT, sizeMap, out  PX, out  PY);
               point = new Point(PX, PY);
               _routePoints.PPM.Add(new Ppm { RelativeX = point.X / sizeMap, RelativeY = point.Y / sizeMap });
               AddVisualToPlotter(_ppmWorker.CrateVisualPpm(point), point);

               _coordinateHelper.LocalCordToLatLon(52.6618694, 39.4261806, -7100 * 4, 7100 * 4, out LatT, out LonT);
               _coordinateHelper.LatLonToPixel(LatT, LonT, sizeMap, out PX, out PY);
               point = new Point(PX, PY);
               _routePoints.PPM.Add(new Ppm { RelativeX = point.X / sizeMap, RelativeY = point.Y / sizeMap });
               AddVisualToPlotter(_ppmWorker.CrateVisualPpm(point), point);

               _coordinateHelper.LocalCordToLatLon(52.6618694, 39.4261806, -7100 * 8, 7100 * 8, out LatT, out LonT);
               _coordinateHelper.LatLonToPixel(LatT, LonT, sizeMap, out PX, out PY);
               point = new Point(PX, PY);
               _routePoints.PPM.Add(new Ppm { RelativeX = point.X / sizeMap, RelativeY = point.Y / sizeMap });
               AddVisualToPlotter(_ppmWorker.CrateVisualPpm(point), point);




               _coordinateHelper.LocalCordToLatLon(52.6618694, 39.4261806, 7100 * 8, 7100 * 8, out LatT, out LonT);
               _coordinateHelper.LatLonToPixel(LatT, LonT, sizeMap, out PX, out PY);
               point = new Point(PX, PY);
               _routePoints.PPM.Add(new Ppm { RelativeX = point.X / sizeMap, RelativeY = point.Y / sizeMap });
               AddVisualToPlotter(_ppmWorker.CrateVisualPpm(point), point);

               _coordinateHelper.LocalCordToLatLon(52.6618694, 39.4261806, 7100 * 4, 7100 * 4, out LatT, out LonT);
               _coordinateHelper.LatLonToPixel(LatT, LonT, sizeMap, out PX, out PY);
               point = new Point(PX, PY);
               _routePoints.PPM.Add(new Ppm { RelativeX = point.X / sizeMap, RelativeY = point.Y / sizeMap });
               AddVisualToPlotter(_ppmWorker.CrateVisualPpm(point), point);

               _coordinateHelper.LocalCordToLatLon(52.6618694, 39.4261806, 7100 * 2, 7100 * 2, out LatT, out LonT);
               _coordinateHelper.LatLonToPixel(LatT, LonT, sizeMap, out PX, out PY);
               point = new Point(PX, PY);
               _routePoints.PPM.Add(new Ppm { RelativeX = point.X / sizeMap, RelativeY = point.Y / sizeMap });
               AddVisualToPlotter(_ppmWorker.CrateVisualPpm(point), point);

               _coordinateHelper.LocalCordToLatLon(52.6618694, 39.4261806, 7100 * 1, 7100 * 1, out  LatT, out  LonT);
               _coordinateHelper.LatLonToPixel(LatT, LonT, sizeMap, out  PX, out  PY);
               point = new Point(PX, PY);
               _routePoints.PPM.Add(new Ppm { RelativeX = point.X / sizeMap, RelativeY = point.Y / sizeMap });
               AddVisualToPlotter(_ppmWorker.CrateVisualPpm(point), point);
        }
  
        public void AddVisualAirBase()
        {
            var airBaseXmls = _airBaseWorker.LoadAirBaseXml();
            foreach (var airBaseXml in airBaseXmls)
            {
                var sizeMap = (uint)_plotter.Height;
                var visualAirBase = _airBaseWorker.CrateVisualAirBase(airBaseXml);
                _coordinateHelper.LatLonToPixel(airBaseXml.latitude, airBaseXml.longitude, sizeMap, out var px, out var py);
                AddVisualToPlotter(visualAirBase, new Point(px, py));
            }
          
        }

        private void AddVisualLine(Line inLine)
        {
            _plotter.Children.Add(inLine);
        }

        public void Clear()
        {
            _ppmWorker.Clear();
            for (int i = _plotter.Children.Count - 1; i >= 0; i--)
            {
                if(_plotter.Children[i] is VisualAircraft) continue;
                if (_plotter.Children[i] is VisualAirBase) continue;
                _plotter.Children.Remove(_plotter.Children[i]);
            }

            EventsHelper.OnOutLineFromLastPoint(_currentAirport.Screen.LineOut);
        }

        private void AddVisualToPlotter(UIElement ui, Point point)
        {
            if (ui == null) return;
            _plotter.Children.Add(ui);
            Canvas.SetLeft(ui, point.X);
            Canvas.SetTop(ui, point.Y);
            Panel.SetZIndex(ui, 10);
            var t = ui as VisualAircraft;

            if (t!=null)
            {
                Panel.SetZIndex(ui, 100);
            }
        }

        private void ChangeAirportEvent(AirBasePoint airportPoint)
        {
              var sizeMap = (uint)_plotter.Height;
            _currentAirport = airportPoint;
            _coordinateHelper.LatLonToPixel(_currentAirport.AirportInfo.Runway.Threshold.Latitude, _currentAirport.AirportInfo.Runway.Threshold.Longitude, sizeMap, out _lX, out  _lY);

        }

        private void PlotterOnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var sizeMap = (uint) e.NewSize.Height;
            EventsHelper.OnChangeOfSizeEvent(sizeMap);
        }

        #region Route

        public void SaveRoute(string path)
        {
            using(var writer = new StreamWriter(path))
            {
                var serializer = new XmlSerializer(typeof(RoutePoints));
                serializer.Serialize(writer, _routePoints);
                writer.Flush();
            }
        }

        public void OpenRoute(string path)
        {
            Clear();
            RoutePoints routePoints;
            XmlSerializer serializer = new XmlSerializer(typeof(RoutePoints));
            using(FileStream fileStream = new FileStream(path, FileMode.Open))
                routePoints = (RoutePoints)serializer.Deserialize(fileStream);
            var sizeMap = (uint)_plotter.Height;
            foreach(var point in routePoints.PPM)
                AddVisualPpm(new Point(point.RelativeX * sizeMap, point.RelativeY * sizeMap));
        }

        #endregion
    }
}

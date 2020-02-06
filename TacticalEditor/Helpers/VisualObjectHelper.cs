using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Xml.Serialization;
using TacticalEditor.Models.ModelXml;
using TacticalEditor.Models.NavPoint;
using TacticalEditor.VisualObject;
using TacticalEditor.VisualObject.VisAircraft;
using TacticalEditor.VisualObject.VisAirCraft;
using TacticalEditor.VisualObject.VisAirport;
using TacticalEditor.VisualObject.VisPpm;

namespace TacticalEditor.Helpers
{
    class VisualObjectHelper
    {
        private readonly Canvas _plotter;
        private readonly CoordinateHelper _coordinateHelper;
        private int _countNavigationPoint;
        private Line _lastLine;
        private AirportPoint _currentAirport;
        private RoutePoints _routePoints;
        private NavigationPoint[] _airPoints;

        public VisualObjectHelper(Canvas plotter)
        {
            _plotter = plotter;
            _plotter.SizeChanged += PlotterOnSizeChanged;
            _coordinateHelper = new CoordinateHelper();
            _routePoints = new RoutePoints();
            _airPoints = new NavigationPoint[20];
            EventsHelper.AddLineToRouteEvent += AddLineToRouteEvent;
            EventsHelper.ChangeAirportEvent += ChangeAirportEvent;
            LoadAirports();
        }

        #region PPM

        public void AddVisualPpm(Point point)
        {
            if(_countNavigationPoint >= 19)
            {
                MessageBox.Show("Маршрут более чем из 20 точек слишком токсичен для летчика!!!");
                return;
            }
            _countNavigationPoint++;
            var ppmPoint = PreparePpmPoint(point);
            _airPoints[_countNavigationPoint] = ppmPoint.NavigationPoint;
            EventsHelper.OnPpmCollectionEvent(_airPoints);
            AddVisualToPlotter(new VisualPpm(ppmPoint), point);

        }

        private PpmPoint PreparePpmPoint(Point point)
        {
            var sizeMap = (uint)_plotter.Height;
            PpmPoint ppmPoint = new PpmPoint();
            ppmPoint.NumberInRoute = _countNavigationPoint;
            ppmPoint.Screen.RelativeX = point.X / sizeMap;
            ppmPoint.Screen.RelativeY = point.Y / sizeMap;
            ppmPoint.Screen.SizeMap = sizeMap;
            ppmPoint.NavigationPoint = PrepareAirPoint(point);
            ppmPoint.Screen.RouteLineIn = _lastLine;
            _routePoints.PPM.Add(new Ppm { RelativeX = point.X / sizeMap, RelativeY = point.Y / sizeMap });
            return ppmPoint;
        }

        private NavigationPoint PrepareAirPoint(Point point)
        {
            var sizeMap = (uint)_plotter.Height;
            NavigationPoint airPoint = new NavigationPoint();
            _coordinateHelper.PixelToLatLon(point, sizeMap, out var lat, out var lon);
            airPoint.GeoCoordinate.Latitude = lat;
            airPoint.GeoCoordinate.Longitude = lon;
            return airPoint;
        }

        #endregion

        #region Airport

        private void LoadAirports()
        {
            string[] fileEntries = Directory.GetFiles("Airports");
            foreach(string fileName in fileEntries)
            {
                Airport air;
                XmlSerializer serializer = new XmlSerializer(typeof(Airport));
                using(FileStream fileStream = new FileStream(fileName, FileMode.Open))
                    air = (Airport)serializer.Deserialize(fileStream);
                AddVisualAirport(air);
            }
        }

        private void AddVisualAirport(Airport airport)
        {
            var airportPoint = PreparePpmPoint(airport);
            AddVisualToPlotter(new VisualAirport(airportPoint), new Point(airportPoint.Screen.RelativeX, airportPoint.Screen.RelativeY));
        }

        private AirportPoint PreparePpmPoint(Airport airport)
        {
            var sizeMap = (uint)_plotter.Height;
            _coordinateHelper.LatLonToPixel(airport.Local.latitude, airport.Local.longitude, sizeMap, out var px, out var py);
            AirportPoint airportPoint = new AirportPoint();

            airportPoint.NavigationPoint.TypePpm = 1;
            airportPoint.NavigationPoint.GeoCoordinate.H = airport.Local.altitude;
            airportPoint.NavigationPoint.GeoCoordinate.Latitude = airport.Local.latitude;
            airportPoint.NavigationPoint.GeoCoordinate.Longitude = airport.Local.longitude;

            airportPoint.AirportInfo.Name = airport.Local.name.ToCharArray();
            airportPoint.AirportInfo.Country = airport.Local.country.ToCharArray();
            airportPoint.AirportInfo.RusName = airport.Local.rusname.ToCharArray();

            airportPoint.AirportInfo.Runway.Width = airport.Runway.width;
            airportPoint.AirportInfo.Runway.Length = airport.Runway.length;
            airportPoint.AirportInfo.Runway.Heading = airport.Runway.heading;
            airportPoint.AirportInfo.Runway.Latitude = airport.Runway.latitude;
            airportPoint.AirportInfo.Runway.Longitude = airport.Runway.longitude;
            
            airportPoint.Screen.SizeMap = sizeMap;
            airportPoint.Screen.RelativeX = px;
            airportPoint.Screen.RelativeY = py;
            
            if(airport.Local.name == "Lipetsk")
            {
                airportPoint.AirportInfo.ActiveAirport = true;
                _airPoints[0] = airportPoint.NavigationPoint;
                EventsHelper.OnPpmCollectionEvent(_airPoints);
                AddVisualAircraft(airport);
            }
            return airportPoint;
        }

        #endregion

        #region Aircraft

        private void AddVisualAircraft(Airport airport)
        {
            var aircraftPoint = PrepareAircraftPoint(airport);
            AddVisualToPlotter(new VisualAircraft(aircraftPoint), new Point(aircraftPoint.Screen.RelativeX, aircraftPoint.Screen.RelativeY));
        }

        private AircraftPoint PrepareAircraftPoint(Airport airport)
        {
            var sizeMap = (uint)_plotter.Height;
            _coordinateHelper.LatLonToPixel(airport.Local.latitude, airport.Local.longitude, sizeMap, out var px, out var py);
            AircraftPoint aircraftPoint = new AircraftPoint();
            aircraftPoint.Screen.SizeMap = sizeMap;
            aircraftPoint.Screen.RelativeX = (double) px / sizeMap;
            aircraftPoint.Screen.RelativeY = (double) py / sizeMap;
            aircraftPoint.NavigationPoint.GeoCoordinate.Latitude = airport.Runway.latitude;
            aircraftPoint.NavigationPoint.GeoCoordinate.Longitude = airport.Runway.longitude;
            aircraftPoint.NavigationPoint.Measure.Psi = airport.Runway.heading;
            aircraftPoint.NavigationPoint.TypePpm = 1;
            return aircraftPoint;
        }

        #endregion

        public void Clear()
        {
            for (int i = _plotter.Children.Count - 1; i >= 0; i--)
            {
                if (_plotter.Children[i] is VisualAirport) continue;
                _plotter.Children.Remove(_plotter.Children[i]);
            }

            ChangeAirportEvent(_currentAirport);
            _countNavigationPoint = 0;
            _routePoints.PPM.Clear();
            for (int i = 1; i < _airPoints.Length; i++)
                _airPoints[i] = null;
        }

        private void AddLineToRouteEvent(Line oldLine, Line newLine)
        {
            _plotter.Children.Add(oldLine);
            _lastLine = newLine;
        }

        private void AddVisualToPlotter(UIElement ui, Point point)
        {
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

        private void ChangeAirportEvent(AirportPoint airportPoint)
        {
            _lastLine = airportPoint.Screen.RouteLineOut;
            _currentAirport = airportPoint;
            _airPoints[0] = airportPoint.NavigationPoint;
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

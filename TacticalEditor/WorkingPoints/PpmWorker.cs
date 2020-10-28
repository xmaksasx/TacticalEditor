using System;
using System.Windows;
using System.Windows.Shapes;
using TacticalEditor.Helpers;
using TacticalEditor.Models.NavPoint;
using TacticalEditor.VisualObject.VisAirport;
using TacticalEditor.VisualObject.VisPpm;

namespace TacticalEditor.WorkingPoints
{
    class PpmWorker
    {
        private int _countNavigationPoint;
        private uint _sizeMap;
        private Line _inLine;
        private readonly CoordinateHelper _coordinateHelper;
        private PpmPoint[] _navigationPoints;
        private AirBasePoint _activeAirbase;

        public PpmWorker()
        {
            _coordinateHelper = new CoordinateHelper();
            _navigationPoints = new PpmPoint[20];
            EventsHelper.ChangeOfSizeEvent += ChangeOfSizeEvent;
            EventsHelper.ChangeAirportEvent += ChangeAirportEvent; ;
            EventsHelper.OutLineFromLastPointEvent += OutLineFromLastPointEvent;
        }

        private void ChangeAirportEvent(AirBasePoint e)
        {
            _activeAirbase = e;
        }

        private void OutLineFromLastPointEvent(Line outline)
        {
            _inLine = outline;
        }

        private void ChangeOfSizeEvent(uint sizeMap)
        {
            _sizeMap = sizeMap;
        }

        public VisualPpm CrateVisualPpm(Point point)
        {
            if(_countNavigationPoint >= 19)
            {
                MessageBox.Show("Маршрут более чем из 20 точек слишком токсичен для летчика!!!");
                return null;
            }
            _countNavigationPoint++;
            var ppmPoint = PreparePpmPoint(point);
            _navigationPoints[_countNavigationPoint-1] = ppmPoint;
            EventsHelper.OnPpmCollectionEvent(_navigationPoints);
            return new VisualPpm(ppmPoint);

        }

        private PpmPoint PreparePpmPoint(Point point)
        {

            PpmPoint ppmPoint = new PpmPoint();
            ppmPoint.NavigationPoint.Name = new char[16];
            ppmPoint.NumberInRoute = _countNavigationPoint;
            ppmPoint.Screen.RelativeX = point.X / _sizeMap;
            ppmPoint.Screen.RelativeY = point.Y / _sizeMap;
            ppmPoint.Screen.SizeMap = _sizeMap;
            ppmPoint.NavigationPoint = PrepareAirPoint(point);
            ppmPoint.Screen.LineIn = _inLine;
            var name = ("ППМ" + _countNavigationPoint).ToCharArray();
            Array.Copy(name, ppmPoint.NavigationPoint.Name, name.Length);


            return ppmPoint;
        }

        private NavigationPoint PrepareAirPoint(Point point)
        {

            NavigationPoint airPoint = new NavigationPoint();
            _coordinateHelper.PixelToLatLon(point, _sizeMap, out var lat, out var lon);
            if (_countNavigationPoint == 1)
                airPoint.Executable = 1;
            airPoint.GeoCoordinate.Latitude = lat;
            airPoint.GeoCoordinate.Longitude = lon;
            _coordinateHelper.LocalCordToXZ(_activeAirbase.AirportInfo.Runway.Threshold.Latitude, _activeAirbase.AirportInfo.Runway.Threshold.Longitude, lat,lon, out var x, out var z);
            airPoint.GeoCoordinate.X = x;
            airPoint.GeoCoordinate.Z = z;
            airPoint.Measure.RDetect = 1000;
            airPoint.Type = 2;
            return airPoint;
        }

        public void Clear()
        {
            for (int i = 0; i < _navigationPoints.Length; i++)
            {
                _navigationPoints[i] = null;
            }
            _countNavigationPoint = 0;

        }
   
    }
}

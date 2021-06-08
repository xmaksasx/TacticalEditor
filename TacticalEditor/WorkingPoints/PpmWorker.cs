using System;
using System.Windows;
using System.Windows.Shapes;
using TacticalEditor.Helpers;
using TacticalEditor.Models.NavPoint;
using TacticalEditor.VisualObject.VisAerodrome;
using TacticalEditor.VisualObject.VisPpm;

namespace TacticalEditor.WorkingPoints
{
    class PpmWorker
    {
        private int _countNavigationPoint;
     
        private Line _inLine;
        private readonly CoordinateHelper _coordinateHelper;
        private PpmPoint[] _ppmPoints;
        private AerodromePoint _activeAerodrome;

        public PpmWorker()
        {
            _coordinateHelper = new CoordinateHelper();
            _ppmPoints = new PpmPoint[20];
            EventsHelper.ChangeAerodromeEvent += ChangeAerodrome;
            EventsHelper.OutLineFromLastPointEvent += OutLineFromLastPointEvent;
        }

        private void ChangeAerodrome(AerodromePoint e)
        {
            _activeAerodrome = e;
        }

        private void OutLineFromLastPointEvent(Line outline)
        {
            _inLine = outline;
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
            _ppmPoints[_countNavigationPoint-1] = ppmPoint;
            EventsHelper.OnPpmCollectionEvent(_ppmPoints);
            return new VisualPpm(ppmPoint);

        }

        private PpmPoint PreparePpmPoint(Point point)
        {

            PpmPoint ppmPoint = new PpmPoint();
            ppmPoint.NavigationPoint.Name = new char[16];
            ppmPoint.NumberInRoute = _countNavigationPoint;
            ppmPoint.NavigationPoint = PrepareAirPoint(point);
            ppmPoint.Screen.LineIn = _inLine;
            var name = ("ППМ" + _countNavigationPoint).ToCharArray();
            Array.Copy(name, ppmPoint.NavigationPoint.Name, name.Length);


            return ppmPoint;
        }

        private NavigationPoint PrepareAirPoint(Point point)
        {
            NavigationPoint airPoint = new NavigationPoint();
            _coordinateHelper.PixelToLatLon(point,  out var lat, out var lon);
            if (_countNavigationPoint == 1)
                airPoint.Executable = 1;
            airPoint.GeoCoordinate.Latitude = lat;
            airPoint.GeoCoordinate.Longitude = lon;
            _coordinateHelper.LocalCordToXZ(_activeAerodrome.AerodromeInfo.Runway.Threshold.Latitude, _activeAerodrome.AerodromeInfo.Runway.Threshold.Longitude, lat,lon, out var x, out var z);
            airPoint.GeoCoordinate.X = x;
            airPoint.GeoCoordinate.Z = z;
            airPoint.GeoCoordinate.H = _coordinateHelper.GetElevation(lat, lon,185);
            airPoint.Measure.RDetect = 1000;
            airPoint.Type = 2;
            return airPoint;
        }

        public void Clear()
        {
            for (int i = 0; i < _ppmPoints.Length; i++)
            {
                _ppmPoints[i] = null;
            }
            _countNavigationPoint = 0;
            EventsHelper.OnPpmCollectionEvent(_ppmPoints);
        }
   
    }
}

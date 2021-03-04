using TacticalEditor.Helpers;
using TacticalEditor.Models;
using TacticalEditor.VisualObject.VisAirport;


namespace TacticalEditor.Calculate
{
    class CalculateAirBases
    {
        private AirBasePoint[] _airBasePoints;
        private MeasureHelper _measureHelper = new MeasureHelper();
        private CoordinateHelper _coordinateHelper = new CoordinateHelper();
        private AirBasePoint _airBasePoint;

        public CalculateAirBases()
        {
            EventsHelper.AirBaseCollectionEvent += AirBaseCollectionChange;
            EventsHelper.ChangeAircraftCoordinateEvent += AircraftCoordinateChange;
            EventsHelper.ChangeAirportEvent += AirportChange;
        }

        private void AirportChange(AirBasePoint e)
        {
            _airBasePoint = e;
        }

        private void AirBaseCollectionChange(AirBasePoint[] airbases)
        {
            _airBasePoints = airbases;
        }

        private void AircraftCoordinateChange(AircraftPosition aircraft)
        {
          
            for (int i = 0; i < _airBasePoints?.Length; i++)
            {
                if (_airBasePoints[i] == null) continue;

                var abp = _airBasePoints[i].NavigationPoint;
                var abpR = _airBasePoints[i].AirportInfo.Runway;
                var acG = aircraft.GeoCoordinate;

                _coordinateHelper.LocalCordToXZ(19.71766146, -155.0541873, 19.72135904, -155.0293497,
                    out var XX, out var ZZ);

                abp.Measure.Distance =
                    _measureHelper.GetDistanceInKmLatLon(abp.GeoCoordinate.Latitude, abp.GeoCoordinate.Longitude, acG.Latitude, acG.Longitude);

                abp.Measure.Psi =
                    _measureHelper.GetDegreesAzimuthLatLon(abp.GeoCoordinate.Latitude, abp.GeoCoordinate.Longitude, acG.Latitude, acG.Longitude);

                _coordinateHelper.LocalCordToXZ(abp.GeoCoordinate.Latitude, abp.GeoCoordinate.Longitude, abpR.Threshold.Latitude, abpR.Threshold.Longitude,
                    out var x, out var z);
                _airBasePoints[i].AirportInfo.Runway.Threshold.X = x - acG.X;
                _airBasePoints[i].AirportInfo.Runway.Threshold.Z = z - acG.Z;

                _coordinateHelper.LocalCordToXZ(abp.GeoCoordinate.Latitude, abp.GeoCoordinate.Longitude, abpR.GlideSlope.Latitude, abpR.GlideSlope.Longitude,
                    out  x, out  z);
                _airBasePoints[i].AirportInfo.Runway.GlideSlope.X = x - acG.X;
                _airBasePoints[i].AirportInfo.Runway.GlideSlope.Z = z -acG.Z;

                _coordinateHelper.LocalCordToXZ(abp.GeoCoordinate.Latitude, abp.GeoCoordinate.Longitude, abpR.Localizer.Latitude, abpR.Localizer.Longitude, 
                    out x, out z);
                _airBasePoints[i].AirportInfo.Runway.Localizer.X = x - acG.X;
                _airBasePoints[i].AirportInfo.Runway.Localizer.Z = z - acG.Z;

                _coordinateHelper.LocalCordToXZ(abp.GeoCoordinate.Latitude, abp.GeoCoordinate.Longitude, abpR.LocatorMiddle.Latitude, abpR.LocatorMiddle.Longitude,
                    out x, out z);
                _airBasePoints[i].AirportInfo.Runway.LocatorMiddle.X = x - acG.X;
                _airBasePoints[i].AirportInfo.Runway.LocatorMiddle.Z = z - acG.Z;

                _coordinateHelper.LocalCordToXZ(abp.GeoCoordinate.Latitude, abp.GeoCoordinate.Longitude, abpR.LocatorOuter.Latitude, abpR.LocatorOuter.Longitude, 
                    out x, out z);
                _airBasePoints[i].AirportInfo.Runway.LocatorOuter.X = x - acG.X;
                _airBasePoints[i].AirportInfo.Runway.LocatorOuter.Z = z - acG.Z;
            }
        }
    }
}
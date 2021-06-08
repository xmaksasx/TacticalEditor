using TacticalEditor.Helpers;
using TacticalEditor.Models;
using TacticalEditor.VisualObject.VisAerodrome;


namespace TacticalEditor.Calculate
{
    class CalculateAerodromes
    {
        private AerodromePoint[] _aerodromePoints;
        private readonly MeasureHelper _measureHelper = new MeasureHelper();
        private readonly CoordinateHelper _coordinateHelper = new CoordinateHelper();

        public CalculateAerodromes()
        {
            EventsHelper.AerodromeCollectionEvent += AerodromeCollectionChange;
            EventsHelper.ChangeAircraftCoordinateEvent += AircraftCoordinateChange;
        }
     
        private void AerodromeCollectionChange(AerodromePoint[] aerodromePoints)
        {
            _aerodromePoints = aerodromePoints;
        }

        private void AircraftCoordinateChange(AircraftPosition aircraft)
        {
          
            for (int i = 0; i < _aerodromePoints?.Length; i++)
            {
                if (_aerodromePoints[i] == null) continue;

                var abp = _aerodromePoints[i].NavigationPoint;
                var abpR = _aerodromePoints[i].AerodromeInfo.Runway;
                var acG = aircraft.GeoCoordinate;

                abp.Measure.Distance =
                    _measureHelper.GetDistanceInKmLatLon(abp.GeoCoordinate.Latitude, abp.GeoCoordinate.Longitude, acG.Latitude, acG.Longitude);

                abp.Measure.Psi =
                    _measureHelper.GetDegreesAzimuthLatLon(abp.GeoCoordinate.Latitude, abp.GeoCoordinate.Longitude, acG.Latitude, acG.Longitude);

                _coordinateHelper.LocalCordToXZ(abp.GeoCoordinate.Latitude, abp.GeoCoordinate.Longitude, abpR.Threshold.Latitude, abpR.Threshold.Longitude,
                    out var x, out var z);
                _aerodromePoints[i].AerodromeInfo.Runway.Threshold.X = x - acG.X;
                _aerodromePoints[i].AerodromeInfo.Runway.Threshold.Z = z - acG.Z;

                _coordinateHelper.LocalCordToXZ(abp.GeoCoordinate.Latitude, abp.GeoCoordinate.Longitude, abpR.GlideSlope.Latitude, abpR.GlideSlope.Longitude,
                    out  x, out  z);
                _aerodromePoints[i].AerodromeInfo.Runway.GlideSlope.X = x - acG.X;
                _aerodromePoints[i].AerodromeInfo.Runway.GlideSlope.Z = z -acG.Z;

                _coordinateHelper.LocalCordToXZ(abp.GeoCoordinate.Latitude, abp.GeoCoordinate.Longitude, abpR.Localizer.Latitude, abpR.Localizer.Longitude, 
                    out x, out z);
                _aerodromePoints[i].AerodromeInfo.Runway.Localizer.X = x - acG.X;
                _aerodromePoints[i].AerodromeInfo.Runway.Localizer.Z = z - acG.Z;

                _coordinateHelper.LocalCordToXZ(abp.GeoCoordinate.Latitude, abp.GeoCoordinate.Longitude, abpR.LocatorMiddle.Latitude, abpR.LocatorMiddle.Longitude,
                    out x, out z);
                _aerodromePoints[i].AerodromeInfo.Runway.LocatorMiddle.X = x - acG.X;
                _aerodromePoints[i].AerodromeInfo.Runway.LocatorMiddle.Z = z - acG.Z;

                _coordinateHelper.LocalCordToXZ(abp.GeoCoordinate.Latitude, abp.GeoCoordinate.Longitude, abpR.LocatorOuter.Latitude, abpR.LocatorOuter.Longitude, 
                    out x, out z);
                _aerodromePoints[i].AerodromeInfo.Runway.LocatorOuter.X = x - acG.X;
                _aerodromePoints[i].AerodromeInfo.Runway.LocatorOuter.Z = z - acG.Z;
            }
        }
    }
}
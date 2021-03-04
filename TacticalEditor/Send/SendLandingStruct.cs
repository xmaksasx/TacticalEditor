using TacticalEditor.Helpers;
using TacticalEditor.Models;
using TacticalEditor.VisualObject.VisAirport;

namespace TacticalEditor.Send
{
    class SendLandingStruct
    {
        LandingHelper _landingHelper;
        Landing _landing;
        AircraftPosition _aircraft;
        AirBasePoint _airBase;
        CoordinateHelper _coordinateHelper;

        public SendLandingStruct()
        {
            _landingHelper = new LandingHelper();
            _aircraft = new AircraftPosition();
            _airBase = new AirBasePoint();
            _landing = new Landing();
            _coordinateHelper = new CoordinateHelper();
            _landing.Head = _landing.GetHead("TacticalEditor_Landing");
            EventsHelper.ChangeAircraftCoordinateEvent += ChangeAircraftCoordinate;
            EventsHelper.ChangeAirportEvent += ChangeAirportEvent;
        }

        private void ChangeAirportEvent(AirBasePoint airBase)
        {
            _airBase = airBase;
        }
        public byte[] GetByte()
        {
            var aBGeo = _airBase.NavigationPoint.GeoCoordinate;
            var runwayGeo = _airBase.AirportInfo.Runway;
         
            double X= _aircraft.GeoCoordinate.X + runwayGeo.Threshold.X;
            double Z = _aircraft.GeoCoordinate.Z + runwayGeo.Threshold.Z;
            _landing.DistanceToRwy = _landingHelper.GetDistance(_aircraft.GeoCoordinate.X, _aircraft.GeoCoordinate.Z, X, Z);

            X = _aircraft.GeoCoordinate.X + runwayGeo.LocatorOuter.X;
            Z = _aircraft.GeoCoordinate.Z + runwayGeo.LocatorOuter.Z;
            _landing.PassedLocatorOuter = _landingHelper.PassedLocatorOuter(_aircraft.GeoCoordinate.X, _aircraft.GeoCoordinate.Z, _aircraft.GeoCoordinate.H, X, Z) ? 1 : 0;


            X = _aircraft.GeoCoordinate.X + runwayGeo.LocatorMiddle.X;
            Z = _aircraft.GeoCoordinate.Z + runwayGeo.LocatorMiddle.Z;
            _landing.PassedLocatorMiddle = _landingHelper.PassedLocatorMiddle(_aircraft.GeoCoordinate.X, _aircraft.GeoCoordinate.Z, _aircraft.GeoCoordinate.H, X, Z) ? 1 : 0;


            X = _aircraft.GeoCoordinate.X + runwayGeo.Localizer.X;
            Z = _aircraft.GeoCoordinate.Z + runwayGeo.Localizer.Z;
            var distanceLoc = _landingHelper.GetDistance(_aircraft.GeoCoordinate.X, _aircraft.GeoCoordinate.Z, X, Z);
            _landing.IndicatorLoc = _landingHelper.IndicatorLoc(_aircraft.GeoCoordinate.X, _aircraft.GeoCoordinate.Z, X, Z, runwayGeo.Heading);
            _landing.IndicatorLocIsVisible = _landingHelper.IndicatorLocIsVisible(distanceLoc, _aircraft.GeoCoordinate.H, aBGeo.H, _landing.IndicatorLoc) ? 1 : 0;

            X = _aircraft.GeoCoordinate.X + runwayGeo.GlideSlope.X;
            Z = _aircraft.GeoCoordinate.Z + runwayGeo.GlideSlope.Z;
            var distanceGs = _landingHelper.GetDistance(_aircraft.GeoCoordinate.X, _aircraft.GeoCoordinate.Z, X, Z);
            _landing.IndicatorGs = _landingHelper.IndicatorGs(_aircraft.GeoCoordinate.H, aBGeo.H, distanceGs);
            _landing.IndicatorGsIsVisible = _landingHelper.IndicatorGsIsVisible(distanceGs, _aircraft.GeoCoordinate.H, aBGeo.H, _landing.IndicatorGs) ? 1 : 0;


            X = _aircraft.GeoCoordinate.X + runwayGeo.LocatorOuter.X;
            Z = _aircraft.GeoCoordinate.Z + runwayGeo.LocatorOuter.Z;
            _landing.CourseOM = _landingHelper.CourseToLocator(_aircraft.GeoCoordinate.X, _aircraft.GeoCoordinate.Z, _aircraft.Risk, X, Z);


            X = _aircraft.GeoCoordinate.X + runwayGeo.LocatorOuter.X;
            Z = _aircraft.GeoCoordinate.Z + runwayGeo.LocatorOuter.Z;
            _landing.DistanceToOM = _landingHelper.GetDistance(_aircraft.GeoCoordinate.X, _aircraft.GeoCoordinate.Z, X, Z);

            DebugParameters.Loc = _landing.IndicatorLoc;
            DebugParameters.Gs = _landing.IndicatorGs;

            return ConvertHelper.ObjectToByte(_landing);
        }

        private void ChangeAircraftCoordinate(AircraftPosition aircraft)
        {
            _aircraft = aircraft;
        }
    }
}
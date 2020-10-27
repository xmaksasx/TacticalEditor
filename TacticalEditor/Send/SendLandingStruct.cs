using System;
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

        public SendLandingStruct()
        {
            _landingHelper = new LandingHelper();
            _aircraft = new AircraftPosition();
            _airBase = new AirBasePoint();
            _landing = new Landing();
            _landing.Head = _landing.GetHead("TacticalEditor_Landing");
            EventsHelper.ChangeAircraftCoordinateEvent += ChangeAircraftCoordinate;
            EventsHelper.ChangeAirportEvent += ChangeAirportEvent;
        }

        private void ChangeAirportEvent(AirBasePoint airBase)
        {
            _airBase = airBase;
        }

        private void ChangeAircraftCoordinate(AircraftPosition aircraft)
        {
            _aircraft = aircraft;
        }

        public byte[] GetByte()
        {
            _landing.DistanceToRwy = _landingHelper.GetDistance(_aircraft.GeoCoordinate.X, _aircraft.GeoCoordinate.Z,
                _airBase.AirportInfo.Runway.Threshold.X, _airBase.AirportInfo.Runway.Threshold.X);

            _landing.PassedLocatorOuter = _landingHelper.PassedLocatorOuter(_aircraft.GeoCoordinate.X,
                _aircraft.GeoCoordinate.Z, _aircraft.GeoCoordinate.H,
                _airBase.AirportInfo.Runway.LocatorOuter.X, _airBase.AirportInfo.Runway.LocatorOuter.Z) ? 1 : 0;

            _landing.PassedLocatorMiddle = _landingHelper.PassedLocatorMiddle(_aircraft.GeoCoordinate.X,
                _aircraft.GeoCoordinate.Z, _aircraft.GeoCoordinate.H,
                _airBase.AirportInfo.Runway.LocatorMiddle.X, _airBase.AirportInfo.Runway.LocatorMiddle.Z) ? 1 : 0;

            _landing.IndicatorLoc = _landingHelper.IndicatorLoc(_aircraft.GeoCoordinate.X,
                _aircraft.GeoCoordinate.Z, _airBase.AirportInfo.Runway.Localizer.X,
                _airBase.AirportInfo.Runway.Localizer.Z, _airBase.AirportInfo.Runway.Heading);

            var distanceGs = _landingHelper.GetDistance(_aircraft.GeoCoordinate.X, _aircraft.GeoCoordinate.Z,
                _airBase.AirportInfo.Runway.GlideSlope.X, _airBase.AirportInfo.Runway.GlideSlope.Z);
            _landing.IndicatorGs =
                _landingHelper.IndicatorGs(_aircraft.GeoCoordinate.H, _airBase.NavigationPoint.GeoCoordinate.H, distanceGs);

            var distanceLoc = _landingHelper.GetDistance(_aircraft.GeoCoordinate.X, _aircraft.GeoCoordinate.Z,
                _airBase.AirportInfo.Runway.Localizer.X, _airBase.AirportInfo.Runway.Localizer.Z);

            _landing.IndicatorLocIsVisible = _landingHelper.IndicatorLocIsVisible(distanceLoc, _aircraft.GeoCoordinate.H,
                _airBase.NavigationPoint.GeoCoordinate.H, _landing.IndicatorLoc) ? 1 : 0;


            _landing.IndicatorGsIsVisible = _landingHelper.IndicatorGsIsVisible(distanceGs, _aircraft.GeoCoordinate.H,
                _airBase.NavigationPoint.GeoCoordinate.H, _landing.IndicatorLoc) ? 1 : 0;


            _landing.CourseOM = _landingHelper.CourseToLocator(_aircraft.GeoCoordinate.X,
                _aircraft.GeoCoordinate.Z, _aircraft.Risk,_airBase.AirportInfo.Runway.LocatorOuter.X,
                _airBase.AirportInfo.Runway.LocatorOuter.Z);

            _landing.DistanceToOM = _landingHelper.GetDistance(_aircraft.GeoCoordinate.X, _aircraft.GeoCoordinate.Z,
                _airBase.AirportInfo.Runway.LocatorOuter.X, _airBase.AirportInfo.Runway.LocatorOuter.Z);

            EventsHelper.OnDebugNumberEvent(_landing.IndicatorLoc);

            return ConvertHelper.ObjectToByte(_landing);

        }

      

    


    }

  
}

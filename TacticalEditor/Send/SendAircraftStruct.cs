using TacticalEditor.Helpers;
using TacticalEditor.Models;
using TacticalEditor.VisualObject.VisAirport;

namespace TacticalEditor.Send
{
    class SendAircraftStruct
    {

        private AirBasePoint _airBase;
        private CoordinateHelper _coordinateHelper;
        private AircraftPosition _aircraft;

        public SendAircraftStruct()
        {
            _airBase = new AirBasePoint();
            _coordinateHelper = new CoordinateHelper();
            _aircraft= new AircraftPosition();
            _aircraft.Head = _aircraft.GetHead("Aircraft_Position");
            EventsHelper.ChangeAirportEvent += ChangeAirportEvent;
        }

        private void ChangeAirportEvent(AirBasePoint airBase)
        {
            _airBase = airBase;
        }

        public byte[] GetByte(AircraftPosition aircraft)
        {
            _coordinateHelper.LocalCordToLatLon(_airBase.AirportInfo.Runway.Threshold.Latitude,_airBase.AirportInfo.Runway.Threshold.Longitude, aircraft.GeoCoordinate.X, aircraft.GeoCoordinate.Z,
                out var lat, out var lon
            );
            
            _aircraft.Kren = aircraft.Kren;
            _aircraft.Risk = aircraft.Risk;
            _aircraft.Tang = aircraft.Tang;
            _aircraft.HLand = _coordinateHelper.GetElevation(lat, lon, _airBase.NavigationPoint.GeoCoordinate.H);
            _aircraft.GeoCoordinate.Latitude = lat;
            _aircraft.GeoCoordinate.Longitude = lon;
            _aircraft.GeoCoordinate.X = aircraft.GeoCoordinate.X;
            _aircraft.GeoCoordinate.Z = aircraft.GeoCoordinate.Z;
            _aircraft.GeoCoordinate.H = aircraft.GeoCoordinate.H;


            DebugParameters.LatLA = lat;
            DebugParameters.LonLA = lon;
            DebugParameters.HLA = _aircraft.HLand;
            DebugParameters.PsiLA = _aircraft.Risk;
            DebugParameters.HbarLA = _aircraft.GeoCoordinate.H- _aircraft.HLand;

            EventsHelper.OnChangeAircraftCoordinateEvent(_aircraft);

            return ConvertHelper.ObjectToByte(_aircraft);

        }
    }
}
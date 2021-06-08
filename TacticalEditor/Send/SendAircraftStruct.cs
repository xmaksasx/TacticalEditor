using TacticalEditor.Helpers;
using TacticalEditor.Models;
using TacticalEditor.VisualObject.VisAerodrome;

namespace TacticalEditor.Send
{
    class SendAircraftStruct
    {

        private AerodromePoint _aerodromePoint;
        private CoordinateHelper _coordinateHelper;
        private AircraftPosition _aircraft;

        public SendAircraftStruct()
        {
            _aerodromePoint = new AerodromePoint();
            _coordinateHelper = new CoordinateHelper();
            _aircraft= new AircraftPosition();
            _aircraft.Head = _aircraft.GetHead("Aircraft_Position");
            EventsHelper.ChangeAerodromeEvent += ChangeAerodrome;
        }

        private void ChangeAerodrome(AerodromePoint aerodromePoint)
        {
            _aerodromePoint = aerodromePoint;
        }

        public byte[] GetByte(AircraftPosition aircraft)
        {
            _coordinateHelper.LocalCordToLatLon(_aerodromePoint.AerodromeInfo.Runway.Threshold.Latitude,_aerodromePoint.AerodromeInfo.Runway.Threshold.Longitude, aircraft.GeoCoordinate.X, aircraft.GeoCoordinate.Z,
                out var lat, out var lon
            );
            
            _aircraft.Kren = aircraft.Kren;
            _aircraft.Risk = aircraft.Risk;
            _aircraft.Tang = aircraft.Tang;
            _aircraft.HLand = _coordinateHelper.GetElevation(lat, lon, _aerodromePoint.NavigationPoint.GeoCoordinate.H);
            _aircraft.GeoCoordinate.Latitude = lat;
            _aircraft.GeoCoordinate.Longitude = lon;
            _aircraft.GeoCoordinate.X = aircraft.GeoCoordinate.X;
            _aircraft.GeoCoordinate.Z = aircraft.GeoCoordinate.Z;
            _aircraft.GeoCoordinate.H = aircraft.GeoCoordinate.H;
            _aircraft.V= aircraft.V;


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
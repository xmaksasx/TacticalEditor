using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TacticalEditor.Helpers;
using TacticalEditor.Models;
using TacticalEditor.Models.NavPoint;
using TacticalEditor.VisualObject.VisAirport;

namespace TacticalEditor.Send
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class ListOfAirBases : Header
    {
        private byte[] _head;
        private int _countAirBases;
        private int _activeAirbase;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        AirBasePoint[] AirBases = new AirBasePoint[20];
        AircraftPosition _aircraft = new AircraftPosition();
        MeasureHelper _measureHelper = new MeasureHelper();

        public ListOfAirBases()
        {
            _head = GetHead("TacticalEditor_AirBases");
            EventsHelper.AirBaseCollectionEvent += AirBaseCollectionEvent;
            EventsHelper.ChangeAircraftCoordinateEvent += ChangeAircraftCoordinate;
            EventsHelper.ChangeAirportEvent += ChangeAirportEvent;

        }

        private void ChangeAirportEvent(AirBasePoint e)
        {
            for (int i = 0; i < AirBases.Length; i++)
                if (AirBases[i] != null)
                    if (AirBases[i].Guid== e.Guid)
                        _activeAirbase = i;
        }

        private void ChangeAircraftCoordinate(AircraftPosition aircraft)
        {
            _aircraft = aircraft;
        }

        private void AirBaseCollectionEvent(AirBasePoint[] airbases)
        {
            _countAirBases = 0;
            AirBases = airbases;
            foreach (var airbase in AirBases)
                if (airbase != null)
                    _countAirBases++;
        }

        public byte[] GetByte()
        {
            Calc();
            List<byte> result = new List<byte>();
            result.AddRange(_head);
            result.AddRange(BitConverter.GetBytes(_countAirBases));
            result.AddRange(BitConverter.GetBytes(_activeAirbase));
            for(int i = 0; i < AirBases.Length; i++)
                result.AddRange(AirBases[i] == null
                    ? ConvertHelper.ObjectToByte(new AirBaseInfo())
                    : ConvertHelper.ObjectToByte(AirBases[i].AirportInfo));

            for(int i = 0; i < AirBases.Length; i++)
                result.AddRange(AirBases[i] == null
                    ? ConvertHelper.ObjectToByte(new NavigationPoint() { Type = -1 })
                    : ConvertHelper.ObjectToByte(AirBases[i].NavigationPoint));


            return result.ToArray();
        }

        private void Calc()
        {
            for(int i = 0; i < AirBases.Length; i++)
            {
                if(AirBases[i] == null) continue;

                AirBases[i].NavigationPoint.Measure.Distance =
                    _measureHelper.GetDistanceInMLatLon(AirBases[i].NavigationPoint.GeoCoordinate.Latitude,
                        AirBases[i].NavigationPoint.GeoCoordinate.Longitude,
                        _aircraft.GeoCoordinate.Latitude, _aircraft.GeoCoordinate.Longitude);
                AirBases[i].NavigationPoint.Measure.Psi =
                    _measureHelper.GetDegreesAzimuthLatLon(AirBases[i].NavigationPoint.GeoCoordinate.Latitude,
                        AirBases[i].NavigationPoint.GeoCoordinate.Longitude,
                        _aircraft.GeoCoordinate.Latitude, _aircraft.GeoCoordinate.Longitude);
                _measureHelper.GetDegreesAzimuthLatLon(AirBases[i].NavigationPoint.GeoCoordinate.Latitude,
                    AirBases[i].NavigationPoint.GeoCoordinate.Longitude,
                    _aircraft.GeoCoordinate.Latitude, _aircraft.GeoCoordinate.Longitude);
            }
        }
    }
}

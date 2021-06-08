using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TacticalEditor.Helpers;
using TacticalEditor.Models;
using TacticalEditor.Models.NavPoint;
using TacticalEditor.VisualObject.VisAerodrome;


namespace TacticalEditor.Send
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class ListOfAerodromes : Header
    {
        private byte[] _head;
        private int _countAerodromes;
        private int _activeAerodrome;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        AerodromePoint[] _aerodromes = new AerodromePoint[20];
        AircraftPosition _aircraft = new AircraftPosition();
        MeasureHelper _measureHelper = new MeasureHelper();

        public ListOfAerodromes()
        {
            _head = GetHead("TacticalEditor_Aerodromes");
            EventsHelper.AerodromeCollectionEvent += AerodromeCollectionEvent;
            EventsHelper.ChangeAircraftCoordinateEvent += ChangeAircraftCoordinate;
            EventsHelper.ChangeAerodromeEvent += ChangeAerodrome;

        }

        private void ChangeAerodrome(AerodromePoint e)
        {
            for (int i = 0; i < _aerodromes.Length; i++)
                if (_aerodromes[i] != null)
                    if (_aerodromes[i].Guid == e.Guid)
                        _activeAerodrome = i;
        }

        private void ChangeAircraftCoordinate(AircraftPosition aircraft)
        {
            _aircraft = aircraft;
        }

        private void AerodromeCollectionEvent(AerodromePoint[] aerodromePoints)
        {
            _countAerodromes = 0;
            _aerodromes = aerodromePoints;
            foreach (var aerodrome in _aerodromes)
                if (aerodrome != null)
                    _countAerodromes++;
        }

        public byte[] GetByte()
        {
            Calc();
            List<byte> result = new List<byte>();
            result.AddRange(_head);
            result.AddRange(BitConverter.GetBytes(_countAerodromes));
            result.AddRange(BitConverter.GetBytes(_activeAerodrome));
            for(int i = 0; i < _aerodromes.Length; i++)
                result.AddRange(_aerodromes[i] == null
                    ? ConvertHelper.ObjectToByte(new AerodromeInfo())
                    : ConvertHelper.ObjectToByte(_aerodromes[i].AerodromeInfo));

            for(int i = 0; i < _aerodromes.Length; i++)
                result.AddRange(_aerodromes[i] == null
                    ? ConvertHelper.ObjectToByte(new NavigationPoint() { Type = -1 })
                    : ConvertHelper.ObjectToByte(_aerodromes[i].NavigationPoint));


            return result.ToArray();
        }

        private void Calc()
        {
            for(int i = 0; i < _aerodromes.Length; i++)
            {
                if(_aerodromes[i] == null) continue;

                _aerodromes[i].NavigationPoint.Measure.Distance =
                    _measureHelper.GetDistanceInMLatLon(_aerodromes[i].NavigationPoint.GeoCoordinate.Latitude,
                        _aerodromes[i].NavigationPoint.GeoCoordinate.Longitude,
                        _aircraft.GeoCoordinate.Latitude, _aircraft.GeoCoordinate.Longitude);
                _aerodromes[i].NavigationPoint.Measure.Psi =
                    _measureHelper.GetDegreesAzimuthLatLon(_aerodromes[i].NavigationPoint.GeoCoordinate.Latitude,
                        _aerodromes[i].NavigationPoint.GeoCoordinate.Longitude,
                        _aircraft.GeoCoordinate.Latitude, _aircraft.GeoCoordinate.Longitude);
            }
        }
    }
}

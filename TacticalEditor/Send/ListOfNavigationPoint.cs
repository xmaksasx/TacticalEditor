using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Elevation;
using TacticalEditor.Helpers;
using TacticalEditor.Models;
using TacticalEditor.Models.NavPoint;
using TacticalEditor.VisualObject.VisAirport;
using TacticalEditor.VisualObject.VisPpm;

namespace TacticalEditor.Send
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class ListOfNavigationPoint: Header
    {
        public int CountPoints;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public PpmPoint[] Points = new PpmPoint[20];

        private byte[] _head;

        MeasureHelper _measureHelper = new MeasureHelper();
        CoordinateHelper _coordinateHelper = new CoordinateHelper();
        AircraftPosition _aircraft = new AircraftPosition();
        AirBasePoint _airBasePoint = new AirBasePoint();



        public ListOfNavigationPoint()
        {
            EventsHelper.PpmCollectionEvent += PpmCollection;
            EventsHelper.ChangeAirportEvent += ChangeAirportEvent;
            _head = GetHead("TacticalEditor_PpmPoints");
        }

        private void ChangeAirportEvent(AirBasePoint e)
        {
            _airBasePoint = e;
        }
      

        private void PpmCollection(PpmPoint[] ppmPoints)
        {
            CountPoints = 0;
            Points = ppmPoints;
            foreach (var ppmPoint in ppmPoints)
                if (ppmPoint != null)
                    CountPoints++;
        }

        public byte[] GetByte()
        {
            Calc();
            List<byte> result = new List<byte>();
          result.AddRange(_head);
            result.AddRange(BitConverter.GetBytes(CountPoints));
            for (int i = 0; i < Points.Length; i++)
                result.AddRange(Points[i] == null
                    ? ConvertHelper.ObjectToByte(new NavigationPoint() {Type = -1})
                    : ConvertHelper.ObjectToByte(Points[i].NavigationPoint));
            return result.ToArray();
        }

        public byte[] GetByte(bool reverse)
        {
            Calc();
            List<byte> result = new List<byte>();
            _head = new byte[]
            {
                82, 111, 117, 116, 101, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                82, 111, 117, 116, 101, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 16
            };
            result.AddRange(_head);
            result.AddRange(BitConverter.GetBytes((double)CountPoints));
            for (int i = 0; i < Points.Length; i++)
                if (Points[i] == null)
                {
                    result.AddRange(ConvertHelper.ObjectToByte(new NavigationPoint() {Type = -1}));
                }
                else
                {

                    var tt  = BitConverter.GetBytes(Points[i].NavigationPoint.Name[0]);

                    Encoding.BigEndianUnicode.GetChars(tt,0, tt.Length, Points[i].NavigationPoint.Name,0);
                    ConvertHelper.ObjectToByte(Points[i].NavigationPoint);
                }



            var bytes = result.ToArray();




            byte[] name = Encoding.BigEndianUnicode.GetBytes("ППМ");

            for (int i = 68; i < bytes.Length; i += 8)
                Array.Reverse(bytes, i, 8);
            return bytes;
        }



        private void Calc()
        {
            for (int i = 0; i < Points.Length; i++)
            {
                if (Points[i] == null) continue;

                Points[i].NavigationPoint.Measure.Distance =
                    _measureHelper.GetDistanceInMLatLon(Points[i].NavigationPoint.GeoCoordinate.Latitude,
                        Points[i].NavigationPoint.GeoCoordinate.Longitude,
                        _aircraft.GeoCoordinate.Latitude, _aircraft.GeoCoordinate.Longitude);
                Points[i].NavigationPoint.Measure.Psi =
                    _measureHelper.GetDegreesAzimuthLatLon(Points[i].NavigationPoint.GeoCoordinate.Latitude,
                        Points[i].NavigationPoint.GeoCoordinate.Longitude,
                        _aircraft.GeoCoordinate.Latitude, _aircraft.GeoCoordinate.Longitude);
                _measureHelper.GetDegreesAzimuthLatLon(Points[i].NavigationPoint.GeoCoordinate.Latitude,
                    Points[i].NavigationPoint.GeoCoordinate.Longitude,
                    _aircraft.GeoCoordinate.Latitude, _aircraft.GeoCoordinate.Longitude);
            }
        }
     
    }
}

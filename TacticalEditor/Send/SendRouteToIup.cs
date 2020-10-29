using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using TacticalEditor.Helpers;
using TacticalEditor.Models;
using TacticalEditor.Models.NavPoint;
using TacticalEditor.VisualObject.VisAirport;
using TacticalEditor.VisualObject.VisPpm;

namespace TacticalEditor.Send
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class SendRouteToIup
    {
        private RouteToIup _routeToIup = new RouteToIup();
        private RouteToIup1 _routeToIup1 = new RouteToIup1();
        MeasureHelper _measureHelper = new MeasureHelper();
        CoordinateHelper _coordinateHelper = new CoordinateHelper();
        AircraftPosition _aircraft = new AircraftPosition();
        AirBasePoint _airBasePoint = new AirBasePoint();



        public SendRouteToIup()
        {
            EventsHelper.PpmCollectionEvent += PpmCollection;
            EventsHelper.ChangeAirportEvent += ChangeAirportEvent;
            _routeToIup.Head = new byte[]
            {
                82, 111, 117, 116, 101, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                82, 111, 117, 116, 101, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 16
            };
        }

        private void ChangeAirportEvent(AirBasePoint e)
        {
            _airBasePoint = e;
            _routeToIup.DepartureAirportInfo = _airBasePoint.AirportInfo;
            _routeToIup.DepartureNavigationPoint = _airBasePoint.NavigationPoint;
            _routeToIup.ArrivalAirportInfo = _airBasePoint.AirportInfo;
            _routeToIup.ArrivalNavigationPoint = _airBasePoint.NavigationPoint;
            SetAirport(_routeToIup1.ArrivalAirport);
            SetAirport(_routeToIup1.DepartureAirport);
        }

        private void SetAirport(Airport airport)
        {
            Array.Copy(ReverseArray(_airBasePoint.AirportInfo.Name), airport.Name, airport.Name.Length);
            airport.Runway = _airBasePoint.AirportInfo.Runway;
        }


        private void PpmCollection(PpmPoint[] ppmPoints)
        {
            _routeToIup.CountPoints = 0;
            for (int i = 0; i < ppmPoints.Length; i++)
            {
                if (ppmPoints[i] == null) continue;
                {
                    _routeToIup.CountPoints++;
                    _routeToIup.NavigationPoints[i] = ppmPoints[i].NavigationPoint;
                }
            }
        }

        List<byte> result = new List<byte>();

        public byte[] GetByte()
        {
            Calc();
            result.Clear();
            result.AddRange(_routeToIup.Head);
            if (_routeToIup.DepartureAirportInfo != null)
                PrepareAirBase(result, _routeToIup.DepartureAirportInfo, _routeToIup.DepartureNavigationPoint);
            for (int i = 0; i < _routeToIup.NavigationPoints.Length; i++)
            {
                var rnp = _routeToIup.NavigationPoints[i];
                if (rnp == null)
                {
                    rnp = new NavigationPoint {Type = -1};
                    result.AddRange(ConvertHelper.ObjectToByte(rnp));
                }

                else
                {
                    var namePpm = new string(rnp.Name).Trim('\0');
                    var nameBytes = ReverseArray(rnp.Name);
                    Array.Copy(nameBytes, rnp.Name, nameBytes.Length);
                    result.AddRange(ConvertHelper.ObjectToByte(rnp));
                    Array.Clear(rnp.Name, 0, rnp.Name.Length);
                    Array.Copy(namePpm.ToCharArray(), rnp.Name, namePpm.ToCharArray().Length);
                }
            }

            if (_routeToIup.ArrivalAirportInfo != null)
                PrepareAirBase(result, _routeToIup.ArrivalAirportInfo, _routeToIup.ArrivalNavigationPoint);

            byte[] bytes = ConvertHelper.ObjectToByte(_routeToIup);

            for (int i = 68; i < bytes.Length; i += 8)
                Array.Reverse(bytes, i, 8);
            return bytes;
        }

        private byte[] ReverseArray(char[] str)
        {
            var nameBytes = Encoding.BigEndianUnicode.GetBytes(new string(str).Trim('\0'));
            var bytes = new byte[str.Length];
            nameBytes.CopyTo(bytes,0);
            for (int i = 0; i < bytes.Length; i = i + 8)
                Array.Reverse(bytes, i, 8);

            return bytes;
        }


        private void PrepareAirBase(List<byte> result, AirBaseInfo Airport, NavigationPoint rnp)
        {
            var nameAir = new string(Airport.Name).Trim('\0');
            var nameBytes = ReverseArray(Airport.Name);
            Array.Copy(nameBytes, Airport.Name, nameBytes.Length);

            var nameAirRus = new string(Airport.RusName).Trim('\0');
            nameBytes = ReverseArray(Airport.RusName);
            Array.Copy(nameBytes, Airport.RusName, nameBytes.Length);

            var nameAirCountry = new string(Airport.Country).Trim('\0');
            nameBytes = ReverseArray(Airport.Country);
            Array.Copy(nameBytes, Airport.Country, nameBytes.Length);

            result.AddRange(ConvertHelper.ObjectToByte(Airport));


            var namePpm = new string(rnp.Name).Trim('\0');
            nameBytes = ReverseArray(rnp.Name);
            Array.Copy(nameBytes, rnp.Name, nameBytes.Length);

            result.AddRange(ConvertHelper.ObjectToByte(rnp));

            Array.Clear(rnp.Name, 0, rnp.Name.Length);
            Array.Clear(Airport.Name, 0, Airport.Name.Length);
            Array.Clear(Airport.RusName, 0, Airport.RusName.Length);
            Array.Clear(Airport.Country, 0, Airport.Country.Length);

            Array.Copy(namePpm.ToCharArray(), rnp.Name, namePpm.ToCharArray().Length);
            Array.Copy(nameAir.ToCharArray(), Airport.Name, nameAir.ToCharArray().Length);
            Array.Copy(nameAirRus.ToCharArray(), Airport.RusName, nameAirRus.ToCharArray().Length);
            Array.Copy(nameAirCountry.ToCharArray(), Airport.Country, nameAirCountry.ToCharArray().Length);



        }

        private void Calc()
        {
            for (int i = 0; i < _routeToIup.NavigationPoints.Length; i++)
            {
                if (_routeToIup.NavigationPoints[i] == null) continue;

                _routeToIup.NavigationPoints[i].Measure.Distance =
                    _measureHelper.GetDistanceInKmLatLon(_routeToIup.NavigationPoints[i].GeoCoordinate.Latitude,
                        _routeToIup.NavigationPoints[i].GeoCoordinate.Longitude,
                        _aircraft.GeoCoordinate.Latitude, _aircraft.GeoCoordinate.Longitude);
                _routeToIup.NavigationPoints[i].Measure.Psi =
                    _measureHelper.GetDegreesAzimuthLatLon(_routeToIup.NavigationPoints[i].GeoCoordinate.Latitude,
                        _routeToIup.NavigationPoints[i].GeoCoordinate.Longitude,
                        _aircraft.GeoCoordinate.Latitude, _aircraft.GeoCoordinate.Longitude);
                _measureHelper.GetDegreesAzimuthLatLon(_routeToIup.NavigationPoints[i].GeoCoordinate.Latitude,
                    _routeToIup.NavigationPoints[i].GeoCoordinate.Longitude,
                    _aircraft.GeoCoordinate.Latitude, _aircraft.GeoCoordinate.Longitude);
            }
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class RouteToIup : Header
    {
        public double CountPoints;
        public AirBaseInfo DepartureAirportInfo;
        public NavigationPoint DepartureNavigationPoint;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public NavigationPoint[] NavigationPoints = new NavigationPoint[20];

        public AirBaseInfo ArrivalAirportInfo;
        public NavigationPoint ArrivalNavigationPoint;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class RouteToIup1 : Header
    {
        public double CountPoints;
        public Airport DepartureAirport = new Airport();
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public NavigationPoint[] NavigationPoints = new NavigationPoint[20];
        public Airport ArrivalAirport = new Airport();
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class Airport 
    {
        /// <summary>
        /// Название аэропорта на кириллице
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        public char[] Name = new char[40];

        /// <summary>
        /// Информация о взлетной полосе
        /// </summary>
        public RunwayInfo Runway;
        
    }


}

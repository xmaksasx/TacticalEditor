using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using TacticalEditor.Helpers;
using TacticalEditor.ModelsXml;
using TacticalEditor.VisualObject.VisAirport;

namespace TacticalEditor.WorkingPoints
{
    class AirBaseWorker
    {
        private int _countAirPortPoint;
        private AirBasePoint[] _airPortPoints;
        private uint _sizeMap;
        private readonly CoordinateHelper _coordinateHelper;


        public AirBaseWorker()
        {
            _countAirPortPoint = 0;
            _airPortPoints = new AirBasePoint[20];
            _coordinateHelper = new CoordinateHelper();
            EventsHelper.ChangeOfSizeEvent += EventsHelperOnChangeOfSizeEvent;
        }

        private void EventsHelperOnChangeOfSizeEvent(uint sizeMap)
        {
            _sizeMap = sizeMap;
        }


        #region Airport

        public List<Aerodrome> LoadAirBaseXml()
        {
            var airBaseXmls = new List<Aerodrome>();
            string[] fileEntries = Directory.GetFiles("Airports");
            foreach(string fileName in fileEntries)
            {
                Aerodrome air;
                XmlSerializer serializer = new XmlSerializer(typeof(Aerodrome));
                using(FileStream fileStream = new FileStream(fileName, FileMode.Open))
                    air = (Aerodrome)serializer.Deserialize(fileStream);
                airBaseXmls.Add(air);
            }
            return airBaseXmls;
        }

        public VisualAirBase CrateVisualAirBase(Aerodrome airbase)
        {
            var airportPoint = PreparePpmPoint(airbase);
            _airPortPoints[_countAirPortPoint] = airportPoint;
            _countAirPortPoint++;
            EventsHelper.OnAirBaseCollectionEvent(_airPortPoints);
             return new VisualAirBase(airportPoint);
        }

        private AirBasePoint PreparePpmPoint(Aerodrome airport)
        {

            _coordinateHelper.LatLonToPixel(airport.latitude, airport.longitude, _sizeMap, out var px, out var py);
            AirBasePoint airportPoint = new AirBasePoint();

            airportPoint.NavigationPoint.Type = 1;
            airportPoint.NavigationPoint.GeoCoordinate.H = airport.altitude;
            airportPoint.NavigationPoint.GeoCoordinate.Latitude = airport.latitude;
            airportPoint.NavigationPoint.GeoCoordinate.Longitude = airport.longitude;


            PrepareThreshold(airportPoint, airport);
            PrepareLocalizer(airportPoint, airport);
            PrepareGlideSlope(airportPoint, airport);
            PrepareLocatorMiddle(airportPoint, airport);
            PrepareLocatorOuter(airportPoint, airport);

            var name = airport.name.ToCharArray();
            name.CopyTo(airportPoint.AirportInfo.Name, 0);
            var country = airport.country.ToCharArray();
            country.CopyTo(airportPoint.AirportInfo.Country, 0);
            var rusname = airport.rusname.ToCharArray();
            rusname.CopyTo(airportPoint.AirportInfo.RusName, 0);
            airportPoint.AirportInfo.Runway.Width = airport.Runway[0].width;
            airportPoint.AirportInfo.Runway.Length = airport.Runway[0].length;
            airportPoint.AirportInfo.Runway.Heading = airport.Runway[0].DirectCourse.Threshold.heading;


            airportPoint.Screen.SizeMap = _sizeMap;
            airportPoint.Screen.RelativeX = px;
            airportPoint.Screen.RelativeY = py;

            if(airport.name == "Lipetsk")
            {
                airportPoint.AirportInfo.ActiveAirport = true;
                airportPoint.NavigationPoint.Measure.Psi = airport.Runway[0].DirectCourse.Threshold.heading;
            
                //  _navigationPoints[0] = airportPoint.NavigationPoint;
               // EventsHelper.OnPpmCollectionEvent(_navigationPoints);
                //AddVisualAircraft(airport);
            }
            return airportPoint;
        }

        private void PrepareThreshold(AirBasePoint airportPoint, Aerodrome airport)
        {
         
            airportPoint.AirportInfo.Runway.Threshold.H = airport.altitude;
            airportPoint.AirportInfo.Runway.Threshold.Latitude = airport.Runway[0].DirectCourse.Threshold.latitude;
            airportPoint.AirportInfo.Runway.Threshold.Longitude = airport.Runway[0].DirectCourse.Threshold.longitude;
            airportPoint.AirportInfo.Runway.Threshold.X = airport.Runway[0].DirectCourse.Threshold.x;
            airportPoint.AirportInfo.Runway.Threshold.Z = airport.Runway[0].DirectCourse.Threshold.z;
        }

        private void PrepareLocalizer(AirBasePoint airportPoint, Aerodrome airport)
        {
            airportPoint.AirportInfo.Runway.Localizer.H = airport.altitude;
            airportPoint.AirportInfo.Runway.Localizer.Latitude = airport.Runway[0].DirectCourse.ILS.LOC.latitude;
            airportPoint.AirportInfo.Runway.Localizer.Longitude = airport.Runway[0].DirectCourse.ILS.LOC.longitude;
            _coordinateHelper.LocalCordToXZ(airport.Runway[0].DirectCourse.Threshold.latitude, airport.Runway[0].DirectCourse.Threshold.longitude,
                airport.Runway[0].DirectCourse.ILS.LOC.latitude, airport.Runway[0].DirectCourse.ILS.LOC.longitude, out var x, out var z);
            airportPoint.AirportInfo.Runway.Localizer.X = x;
            airportPoint.AirportInfo.Runway.Localizer.Z = z;
        }

        private void PrepareGlideSlope(AirBasePoint airportPoint, Aerodrome airport)
        {
            airportPoint.AirportInfo.Runway.GlideSlope.H = airport.altitude;
            airportPoint.AirportInfo.Runway.GlideSlope.Latitude = airport.Runway[0].DirectCourse.ILS.GS.latitude;
            airportPoint.AirportInfo.Runway.GlideSlope.Longitude = airport.Runway[0].DirectCourse.ILS.GS.longitude;
            _coordinateHelper.LocalCordToXZ(airport.Runway[0].DirectCourse.Threshold.latitude, airport.Runway[0].DirectCourse.Threshold.longitude,
                airport.Runway[0].DirectCourse.ILS.GS.latitude, airport.Runway[0].DirectCourse.ILS.GS.longitude, out var x, out var z);
            airportPoint.AirportInfo.Runway.GlideSlope.X = x;
            airportPoint.AirportInfo.Runway.GlideSlope.Z = z;
        }

        private void PrepareLocatorMiddle(AirBasePoint airportPoint, Aerodrome airport)
        {
            airportPoint.AirportInfo.Runway.LocatorMiddle.H = airport.altitude;
            airportPoint.AirportInfo.Runway.LocatorMiddle.Latitude = airport.Runway[0].DirectCourse.LocatorMiddle.latitude;
            airportPoint.AirportInfo.Runway.LocatorMiddle.Longitude = airport.Runway[0].DirectCourse.LocatorMiddle.longitude;
            _coordinateHelper.LocalCordToXZ(airport.Runway[0].DirectCourse.Threshold.latitude, airport.Runway[0].DirectCourse.Threshold.longitude,
                airport.Runway[0].DirectCourse.LocatorMiddle.latitude, airport.Runway[0].DirectCourse.LocatorMiddle.longitude, out var x, out var z);
            airportPoint.AirportInfo.Runway.LocatorMiddle.X = x;
            airportPoint.AirportInfo.Runway.LocatorMiddle.Z = z;
        }

        private void PrepareLocatorOuter(AirBasePoint airportPoint, Aerodrome airport)
        {
            airportPoint.AirportInfo.Runway.LocatorOuter.H = airport.altitude;
            airportPoint.AirportInfo.Runway.LocatorOuter.Latitude = airport.Runway[0].DirectCourse.LocatorOuter.latitude;
            airportPoint.AirportInfo.Runway.LocatorOuter.Longitude = airport.Runway[0].DirectCourse.LocatorOuter.longitude;
            _coordinateHelper.LocalCordToXZ(airport.Runway[0].DirectCourse.Threshold.latitude, airport.Runway[0].DirectCourse.Threshold.longitude,
                airport.Runway[0].DirectCourse.LocatorOuter.latitude, airport.Runway[0].DirectCourse.LocatorOuter.longitude, out var x, out var z);
            airportPoint.AirportInfo.Runway.LocatorOuter.X = x;
            airportPoint.AirportInfo.Runway.LocatorOuter.Z = z;
        }



        #endregion

    }
}

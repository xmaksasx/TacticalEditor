using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using TacticalEditor.Helpers;
using TacticalEditor.ModelsXml;
using TacticalEditor.VisualObject.VisAerodrome;

namespace TacticalEditor.WorkingPoints
{
    class AerodromeWorker
    {
        private int _countaerodromePoint;
        private AerodromePoint[] _aerodromePoints;
        private readonly CoordinateHelper _coordinateHelper;


        public AerodromeWorker()
        {
            _countaerodromePoint = 0;
            _aerodromePoints = new AerodromePoint[20];
            _coordinateHelper = new CoordinateHelper();
           
        }

      


        #region Aerodrome

        public List<Aerodrome> LoadAerodromeXml()
        {
            var aerodromeXmls = new List<Aerodrome>();
            string[] fileEntries = Directory.GetFiles("Aerodromes");
            foreach(string fileName in fileEntries)
            {
                Aerodrome air;
                XmlSerializer serializer = new XmlSerializer(typeof(Aerodrome));
                using(FileStream fileStream = new FileStream(fileName, FileMode.Open))
                    air = (Aerodrome)serializer.Deserialize(fileStream);
                aerodromeXmls.Add(air);
            }
            return aerodromeXmls;
        }

        public VisualAerodrome CrateVisualAerodrome(Aerodrome aerodrome)
        {
            var aerodromePoint = PrepareAerodromePoint(aerodrome);
            _aerodromePoints[_countaerodromePoint] = aerodromePoint;
            _countaerodromePoint++;
            EventsHelper.OnAerodromeCollectionEvent(_aerodromePoints);
             return new VisualAerodrome(aerodromePoint);
        }

        private AerodromePoint PrepareAerodromePoint(Aerodrome aerodrome)
        {

            _coordinateHelper.LatLonToPixel(aerodrome.Latitude, aerodrome.Longitude, out var px, out var py);
            AerodromePoint aerodromePoint = new AerodromePoint();

            aerodromePoint.NavigationPoint.Type = 1;
            aerodromePoint.NavigationPoint.GeoCoordinate.H = aerodrome.Altitude;
            aerodromePoint.NavigationPoint.GeoCoordinate.Latitude = aerodrome.Latitude;
            aerodromePoint.NavigationPoint.GeoCoordinate.Longitude = aerodrome.Longitude;

            PrepareThreshold(aerodromePoint, aerodrome);
            PrepareLocalizer(aerodromePoint, aerodrome);
            PrepareGlideSlope(aerodromePoint, aerodrome);
            PrepareLocatorMiddle(aerodromePoint, aerodrome);
            PrepareLocatorOuter(aerodromePoint, aerodrome);

            var name = aerodrome.Name.ToCharArray();
            name.CopyTo(aerodromePoint.AerodromeInfo.Name, 0);
            var country = aerodrome.Country.ToCharArray();
            country.CopyTo(aerodromePoint.AerodromeInfo.Country, 0);
            var rusname = aerodrome.Rusname.ToCharArray();
            rusname.CopyTo(aerodromePoint.AerodromeInfo.RusName, 0);
            aerodromePoint.AerodromeInfo.Runway.Width = aerodrome.Runway[0].Width;
            aerodromePoint.AerodromeInfo.Runway.Length = aerodrome.Runway[0].Length;
            aerodromePoint.AerodromeInfo.Runway.Heading = aerodrome.Runway[0].DirectCourse.Threshold.Heading;
            aerodromePoint.Guid = new Guid(aerodrome.Guid);

            if (aerodrome.Name == "Adler")
            {
                aerodromePoint.AerodromeInfo.ActiveAerodrome = true;
                aerodromePoint.NavigationPoint.Measure.Psi = aerodrome.Runway[0].DirectCourse.Threshold.Heading;
            }
            return aerodromePoint;
        }

        private void PrepareThreshold(AerodromePoint aerodromePoint, Aerodrome aerodrome)
        {
            aerodromePoint.AerodromeInfo.Runway = new RunwayInfo();
            aerodromePoint.AerodromeInfo.Runway.Threshold.H = aerodrome.Altitude;
            aerodromePoint.AerodromeInfo.Runway.Threshold.Latitude = aerodrome.Runway[0].DirectCourse.Threshold.Latitude;
            aerodromePoint.AerodromeInfo.Runway.Threshold.Longitude = aerodrome.Runway[0].DirectCourse.Threshold.Longitude;
            aerodromePoint.AerodromeInfo.Runway.Threshold.X = aerodrome.Runway[0].DirectCourse.Threshold.X;
            aerodromePoint.AerodromeInfo.Runway.Threshold.Z = aerodrome.Runway[0].DirectCourse.Threshold.Z;
        }

        private void PrepareLocalizer(AerodromePoint aerodromePoint, Aerodrome aerodrome)
        {
            aerodromePoint.AerodromeInfo.Runway.Localizer.H = aerodrome.Altitude;
            aerodromePoint.AerodromeInfo.Runway.Localizer.Latitude = aerodrome.Runway[0].DirectCourse.ILS.LOC.Latitude;
            aerodromePoint.AerodromeInfo.Runway.Localizer.Longitude = aerodrome.Runway[0].DirectCourse.ILS.LOC.Longitude;
            _coordinateHelper.LocalCordToXZ(aerodrome.Runway[0].DirectCourse.Threshold.Latitude, aerodrome.Runway[0].DirectCourse.Threshold.Longitude,
                aerodrome.Runway[0].DirectCourse.ILS.LOC.Latitude, aerodrome.Runway[0].DirectCourse.ILS.LOC.Longitude, out var x, out var z);
            aerodromePoint.AerodromeInfo.Runway.Localizer.X = x;
            aerodromePoint.AerodromeInfo.Runway.Localizer.Z = z;
        }

        private void PrepareGlideSlope(AerodromePoint aerodromePoint, Aerodrome aerodrome)
        {
            aerodromePoint.AerodromeInfo.Runway.GlideSlope.H = aerodrome.Altitude;
            aerodromePoint.AerodromeInfo.Runway.GlideSlope.Latitude = aerodrome.Runway[0].DirectCourse.ILS.GS.Latitude;
            aerodromePoint.AerodromeInfo.Runway.GlideSlope.Longitude = aerodrome.Runway[0].DirectCourse.ILS.GS.Longitude;
            _coordinateHelper.LocalCordToXZ(aerodrome.Runway[0].DirectCourse.Threshold.Latitude, aerodrome.Runway[0].DirectCourse.Threshold.Longitude,
                aerodrome.Runway[0].DirectCourse.ILS.GS.Latitude, aerodrome.Runway[0].DirectCourse.ILS.GS.Longitude, out var x, out var z);
            aerodromePoint.AerodromeInfo.Runway.GlideSlope.X = x;
            aerodromePoint.AerodromeInfo.Runway.GlideSlope.Z = z;
        }

        private void PrepareLocatorMiddle(AerodromePoint aerodromePoint, Aerodrome aerodrome)
        {
            aerodromePoint.AerodromeInfo.Runway.LocatorMiddle.H = aerodrome.Altitude;
            aerodromePoint.AerodromeInfo.Runway.LocatorMiddle.Latitude = aerodrome.Runway[0].DirectCourse.LocatorMiddle.Latitude;
            aerodromePoint.AerodromeInfo.Runway.LocatorMiddle.Longitude = aerodrome.Runway[0].DirectCourse.LocatorMiddle.Longitude;
            _coordinateHelper.LocalCordToXZ(aerodrome.Runway[0].DirectCourse.Threshold.Latitude, aerodrome.Runway[0].DirectCourse.Threshold.Longitude,
                aerodrome.Runway[0].DirectCourse.LocatorMiddle.Latitude, aerodrome.Runway[0].DirectCourse.LocatorMiddle.Longitude, out var x, out var z);
            aerodromePoint.AerodromeInfo.Runway.LocatorMiddle.X = x;
            aerodromePoint.AerodromeInfo.Runway.LocatorMiddle.Z = z;
        }

        private void PrepareLocatorOuter(AerodromePoint aerodromePoint, Aerodrome aerodrome)
        {
            aerodromePoint.AerodromeInfo.Runway.LocatorOuter.H = aerodrome.Altitude;
            aerodromePoint.AerodromeInfo.Runway.LocatorOuter.Latitude = aerodrome.Runway[0].DirectCourse.LocatorOuter.Latitude;
            aerodromePoint.AerodromeInfo.Runway.LocatorOuter.Longitude = aerodrome.Runway[0].DirectCourse.LocatorOuter.Longitude;
            _coordinateHelper.LocalCordToXZ(aerodrome.Runway[0].DirectCourse.Threshold.Latitude, aerodrome.Runway[0].DirectCourse.Threshold.Longitude,
                aerodrome.Runway[0].DirectCourse.LocatorOuter.Latitude, aerodrome.Runway[0].DirectCourse.LocatorOuter.Longitude, out var x, out var z);
            aerodromePoint.AerodromeInfo.Runway.LocatorOuter.X = x;
            aerodromePoint.AerodromeInfo.Runway.LocatorOuter.Z = z;
        }



        #endregion

    }
}

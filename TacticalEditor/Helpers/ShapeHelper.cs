using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TacticalEditor.ModelsXml;

namespace TacticalEditor.Helpers
{
	class ShapeHelper
	{

        public void BuildBox()
        {
            //Point point;

            //GetLatLonOfPoint(_currentAirport.NavigationPoint.GeoCoordinate.Latitude, _currentAirport.NavigationPoint.GeoCoordinate.Longitude, 6000, _currentAirport.AerodromeInfo.Runway.Heading, out double latLocator, out double lonLocator);
            //_coordinateHelper.LatLonToPixel(latLocator, lonLocator, out var PX, out var PY);
            //point = new Point(PX, PY);
            //routePoints.PPM.Add(new Ppm { RelativeX = point.X / sizeMap, RelativeY = point.Y / sizeMap });
            //AddVisualToPlotter(_ppmWorker.CrateVisualPpm(point), point);


            //GetLatLonOfPoint(latLocator, lonLocator, 12500, _currentAirport.AerodromeInfo.Runway.Heading - 90, out latLocator, out lonLocator);
            //_coordinateHelper.LatLonToPixel(latLocator, lonLocator, out PX, out PY);
            //point = new Point(PX, PY);
            //routePoints.PPM.Add(new Ppm { RelativeX = point.X / sizeMap, RelativeY = point.Y / sizeMap });
            //AddVisualToPlotter(_ppmWorker.CrateVisualPpm(point), point);

            //GetLatLonOfPoint(latLocator, lonLocator, 28000, _currentAirport.AerodromeInfo.Runway.Heading - 180, out latLocator, out lonLocator);
            //_coordinateHelper.LatLonToPixel(latLocator, lonLocator, out PX, out PY);
            //point = new Point(PX, PY);
            //routePoints.PPM.Add(new Ppm { RelativeX = point.X / sizeMap, RelativeY = point.Y / sizeMap });
            //AddVisualToPlotter(_ppmWorker.CrateVisualPpm(point), point);

            //GetLatLonOfPoint(latLocator, lonLocator, 12500, _currentAirport.AerodromeInfo.Runway.Heading - 270, out latLocator, out lonLocator);
            //_coordinateHelper.LocalCordToXZ(_currentAirport.NavigationPoint.GeoCoordinate.Latitude,
            //    _currentAirport.NavigationPoint.GeoCoordinate.Longitude,
            //    latLocator,
            //    lonLocator,
            //    out var x1,
            //    out var x2
            //    );
            //_coordinateHelper.LatLonToPixel(latLocator, lonLocator, out PX, out PY);
            //point = new Point(PX, PY);
            //routePoints.PPM.Add(new Ppm { RelativeX = point.X / sizeMap, RelativeY = point.Y / sizeMap });
            //AddVisualToPlotter(_ppmWorker.CrateVisualPpm(point), point);
        }


        private void GetLatLonOfPoint(double latThreshold, double lonThreshold, double distance, double courseThreshold, out double latLocator, out double lonLocator)
        {
            latLocator = (latThreshold + distance * Math.Cos(courseThreshold * Math.PI / 180) / (6371000 * Math.PI / 180));
            lonLocator = (lonThreshold + distance * Math.Sin(courseThreshold * Math.PI / 180) / Math.Cos(latThreshold * Math.PI / 180) / (6371000 * Math.PI / 180));
        }

        public void AddDebugPm()
        {

            //_coordinateHelper.LocalCordToLatLon(43.439489, 39.925886, -7100 * 1, 7100 * 1, out var LatT, out var LonT);
            //_coordinateHelper.LatLonToPixel(LatT, LonT, out var PX, out var PY);
            //point = new Point(PX, PY);
            //_routePoints.PPM.Add(new Ppm { RelativeX = point.X / sizeMap, RelativeY = point.Y / sizeMap });
            //AddVisualToPlotter(_ppmWorker.CrateVisualPpm(point), point);

            //_coordinateHelper.LocalCordToLatLon(43.439489, 39.925886, -7100 * 2, 7100 * 2, out LatT, out LonT);
            //_coordinateHelper.LatLonToPixel(LatT, LonT, out PX, out PY);
            //point = new Point(PX, PY);
            //_routePoints.PPM.Add(new Ppm { RelativeX = point.X / sizeMap, RelativeY = point.Y / sizeMap });
            //AddVisualToPlotter(_ppmWorker.CrateVisualPpm(point), point);

            //_coordinateHelper.LocalCordToLatLon(43.439489, 39.925886, -7100 * 4, 7100 * 4, out LatT, out LonT);
            //_coordinateHelper.LatLonToPixel(LatT, LonT, out PX, out PY);
            //point = new Point(PX, PY);
            //_routePoints.PPM.Add(new Ppm { RelativeX = point.X / sizeMap, RelativeY = point.Y / sizeMap });
            //AddVisualToPlotter(_ppmWorker.CrateVisualPpm(point), point);

            //_coordinateHelper.LocalCordToLatLon(43.439489, 39.925886, -7100 * 8, 7100 * 8, out LatT, out LonT);
            //_coordinateHelper.LatLonToPixel(LatT, LonT, out PX, out PY);
            //point = new Point(PX, PY);
            //_routePoints.PPM.Add(new Ppm { RelativeX = point.X / sizeMap, RelativeY = point.Y / sizeMap });
            //AddVisualToPlotter(_ppmWorker.CrateVisualPpm(point), point);




            //_coordinateHelper.LocalCordToLatLon(43.439489, 39.925886, 7100 * 8, 7100 * 8, out LatT, out LonT);
            //_coordinateHelper.LatLonToPixel(LatT, LonT, out PX, out PY);
            //point = new Point(PX, PY);
            //_routePoints.PPM.Add(new Ppm { RelativeX = point.X / sizeMap, RelativeY = point.Y / sizeMap });
            //AddVisualToPlotter(_ppmWorker.CrateVisualPpm(point), point);

            //_coordinateHelper.LocalCordToLatLon(43.439489, 39.925886, 7100 * 4, 7100 * 4, out LatT, out LonT);
            //_coordinateHelper.LatLonToPixel(LatT, LonT, out PX, out PY);
            //point = new Point(PX, PY);
            //_routePoints.PPM.Add(new Ppm { RelativeX = point.X / sizeMap, RelativeY = point.Y / sizeMap });
            //AddVisualToPlotter(_ppmWorker.CrateVisualPpm(point), point);

            //_coordinateHelper.LocalCordToLatLon(43.439489, 39.925886, 7100 * 2, 7100 * 2, out LatT, out LonT);
            //_coordinateHelper.LatLonToPixel(LatT, LonT, out PX, out PY);
            //point = new Point(PX, PY);
            //_routePoints.PPM.Add(new Ppm { RelativeX = point.X / sizeMap, RelativeY = point.Y / sizeMap });
            //AddVisualToPlotter(_ppmWorker.CrateVisualPpm(point), point);

            //_coordinateHelper.LocalCordToLatLon(43.439489, 39.925886, 7100 * 1, 7100 * 1, out LatT, out LonT);
            //_coordinateHelper.LatLonToPixel(LatT, LonT, out PX, out PY);
            //point = new Point(PX, PY);
            //_routePoints.PPM.Add(new Ppm { RelativeX = point.X / sizeMap, RelativeY = point.Y / sizeMap });
            //AddVisualToPlotter(_ppmWorker.CrateVisualPpm(point), point);
        }
    }

}

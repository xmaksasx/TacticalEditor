using System;

namespace TacticalEditor.Helpers
{
    class LandingHelper
    {
        private MeasureHelper _measureHelper;
        public LandingHelper()
        {
            _measureHelper = new MeasureHelper();
        }

        public double GetDistance(double x1, double z1, double x2, double z2)
        {
            return Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((z1 - z2), 2));
        }

        public bool PassedLocatorOuter(double xLa, double zLa, double hLa, double xLocator, double zLocator)
        {
            var d = Math.Sqrt(Math.Pow((xLa - xLocator), 2) + Math.Pow((zLa - zLocator), 2));
            if (d <= hLa * 0.75)
                return true;
            return false;
        }

        public bool PassedLocatorMiddle(double xLa, double zLa, double hLa, double xLocator, double zLocator)
        {
            var d = Math.Sqrt(Math.Pow((xLa - xLocator), 2) + Math.Pow((zLa - zLocator), 2));
            if (d <= hLa * 0.75)
                return true;
            return false;
        }

        public double CourseToLocator(double xLa, double zLa, double yawLa, double xLocator, double zLocator)
        {
            return _measureHelper.GetDegreesAzimuthXZ(xLa, zLa, xLocator, zLocator);
          //  var a = Math.Atan((zLocator - zLa) / (xLocator - xLa));
          //  var b = 90 * Math.Sign(zLocator - zLa) * (1 - Math.Sign(zLocator - zLa));
          // return a + yawLa + b;
        }

        public double IndicatorLoc(double xLa, double zLa, double xLoc, double zLoc, double courseRwy)
        {
            var courseR = courseRwy * Math.PI / 180 ;
            var cos = Math.Cos(courseR);
            var a = ((zLoc - zLa) * Math.Cos(courseR)) - ((xLoc - xLa) * Math.Sin(courseR));
            var b = ((zLoc - zLa) * Math.Sin(courseR)) + ((xLoc - xLa) * Math.Cos(courseR));
            return  (Math.Atan(a / b) * 180 / Math.PI)*-1;
        }

        public double IndicatorGs(double hBarLa, double hRwy, double distanceGs)
        {
            int hGs = 15; //Высота маяка 15м
            var a = (hBarLa - hRwy - hGs) / distanceGs;
            return (3 - Math.Atan(a) * 180 / Math.PI) * -1;
        }

        public bool IndicatorLocIsVisible(double distanceLoc, double yLa, double hRwy, double indicatorLoc)
        {
            int hGs = 15; //Высота маяка 15м
            var a = distanceLoc * Math.Tan(1.75 * 2.7) > yLa - hRwy + hGs;
            var b = distanceLoc * Math.Tan(0.3 * 2.7) < yLa - hRwy + hGs;
            var c = distanceLoc < 20000;
            var d = indicatorLoc < 8;
            if (a && b && c && d)
                return true;
            return false;
        }

        public bool IndicatorGsIsVisible(double distanceGs, double yLa, double hRwy, double indicatorGs)
        {
            var a = distanceGs * Math.Tan(7) > yLa - hRwy;
            var b = distanceGs * Math.Tan(0.85) < yLa - hRwy;
            var c = distanceGs < 20000;
            var d = indicatorGs < 15;
            if (a && b && c && d)
                return true;
            return false;
        }
    }
}

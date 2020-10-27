using System;

namespace TacticalEditor.Helpers
{
    class MeasureHelper
    {

        private const double EquatorialEarthRadius = 6378.1370D;
        private const double D2R = Math.PI / 180D;
        private const double R2D = 180 / Math.PI;

        #region Дистанция между двумя точками

        /// <summary>
        /// Возвращает дистанцию между заданными координатами
        /// </summary>
        /// <param name="xF">Текущая координата</param>
        /// <param name="zF">Текущая координата</param>
        /// <param name="xT">Координата точки искомой дистанции</param>
        /// <param name="zT">Координата точки искомой дистанции</param>
        /// <returns></returns>
        public double GetDistanceXZ(double xF, double zF, double xT, double zT)
            => Math.Sqrt(Math.Pow(xF - xT, 2) + Math.Pow(zF - zT, 2));

        /// <summary>
        /// Возвращает дистанцию в метрах между заданными координатами
        /// </summary>
        /// <param name="latF">Текущая координата</param>
        /// <param name="lonF">Текущая координата</param>
        /// <param name="latT">Координата точки искомой дистанции</param>
        /// <param name="lonT">Координата точки искомой дистанции</param>
        /// <returns></returns>
        public int GetDistanceInMLatLon(double latF, double lonF, double latT, double lonT)
        {
            return (int)(1000D * GetDistanceInKmLatLon(latF, lonF, latT, lonT));
        }

        /// <summary>
        /// Возвращает дистанцию в километрах между заданными координатами
        /// </summary>
        /// <param name="latF">Текущая координата</param>
        /// <param name="lonF">Текущая координата</param>
        /// <param name="latT">Координата точки искомой дистанции</param>
        /// <param name="lonT">Координата точки искомой дистанции</param>
        /// <returns></returns>
        public double GetDistanceInKmLatLon(double latF, double lonF, double latT, double lonT)
        {
            double dlong = (lonT - lonF) * D2R;
            double dlat = (latT - latF) * D2R;
            double a = Math.Pow(Math.Sin(dlat / 2D), 2D) +
                       Math.Cos(latF * D2R) * Math.Cos(latT * D2R) * Math.Pow(Math.Sin(dlong / 2D), 2D);
            double c = 2D * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1D - a));
            double d = EquatorialEarthRadius * c;
            return d;
        }

        #endregion

        #region Азимут

        /// <summary>
        /// Возвращает угол между направлением на север и направлением на объект.
        /// </summary>
        /// <param name="xF">Текущая координата</param>
        /// <param name="zF">Текущая координата</param>
        /// <param name="xT">Координата точки искомого угла</param>
        /// <param name="zT">Координата точки искомого угла</param>
        /// <returns></returns>
        public double GetDegreesAzimuthXZ(double xF, double zF, double xT, double zT)
        {
            var dx = xT - xF;
            var dz = zT - zF;
            var psi = (Math.Atan2(dz, dx)) * 180 / Math.PI;
            if(zT < zF)
                psi = psi + 360;
            return psi;
        }

        /// <summary>
        /// Возвращает угол в радианах между направлением на север и направлением на объект.
        /// </summary>
        /// <param name="latF">Текущая координата</param>
        /// <param name="lonF">Текущая координата</param>
        /// <param name="latT">Координата точки искомого угла</param>
        /// <param name="lonT">Координата точки искомого угла</param>
        /// <returns></returns>
        public double GetRadiansAzimuthLatLon(double latF, double lonF, double latT, double lonT)
        {
            return GetDegreesAzimuthLatLon(latF, lonF, latT, lonT) * D2R;
        }

        /// <summary>
        /// Возвращает угол в градусах между направлением на север и направлением на объект.
        /// </summary>
        /// <param name="latF">Текущая координата</param>
        /// <param name="lonF">Текущая координата</param>
        /// <param name="latT">Координата точки искомого угла</param>
        /// <param name="lonT">Координата точки искомого угла</param>
        /// <returns></returns>
        public double GetDegreesAzimuthLatLon(double latF, double lonF, double latT, double lonT)
        {
            var dLon = (lonT - lonF) * D2R;
            var dPhi = Math.Log(
                Math.Tan((latT * D2R) / 2 + Math.PI / 4) / Math.Tan((latF * D2R) / 2 + Math.PI / 4));
            if(Math.Abs(dLon) > Math.PI)
                dLon = dLon > 0 ? -(2 * Math.PI - dLon) : (2 * Math.PI + dLon);
            return ToBearing(Math.Atan2(dLon, dPhi));
        }

        private double ToBearing(double radians)
        {

            return ((radians * R2D) + 360) % 360;
        }

        #endregion


        public   double  initialBearingTo(double latF, double lonF, double latT, double lonT)
        {
            // tanθ = sinΔλ⋅cosφ2 / cosφ1⋅sinφ2 − sinφ1⋅cosφ2⋅cosΔλ
            // see mathforum.org/library/drmath/view/55417.html for derivation

            double l1 = latF*D2R;
            double l2 = latT*D2R;
            double lg = (lonT - lonF)*D2R;

            double x = Math.Cos(l1) * Math.Sin(l2) - Math.Sin(l1) * Math.Cos(l2) * Math.Cos(lg);
            double y = Math.Sin(lg) * Math.Cos(l2);
            double w = Math.Atan2(y, x);

            double bearing = w*R2D;

            return wrap360(bearing);
        }


        public double finalBearingTo(double latF, double lonF, double latT, double lonT)
        {
           double bearing = initialBearingTo(latF, lonF, latT, lonT) + 180;
           return wrap360(bearing);
        }

        double wrap360(double degrees)
        {
            if (0 <= degrees && degrees < 360) return degrees; // avoid rounding due to arithmetic ops if within range
            return (degrees % 360 + 360) % 360; // sawtooth wave p:360, a:360
        }

        public void GetPass(double xPpm, double zPpm, double xAir, double zAir)
        {
            double r = 1000;
            int active = 0;

            if (r > Math.Abs(xPpm - xAir) && r > Math.Abs(zPpm - zAir))
                active++;
        }

        void FindXY(double Xa, double Ya, double Xb, double Yb, double Rac)
        {
            var Rab = Math.Sqrt(Math.Pow((Xb - Xa), 2) + Math.Pow((Yb - Ya), 2));
            var k = Rac / Rab;
            var Xc = Xa + (Xb - Xa) * k;
            var Yc = Ya + (Yb - Ya) * k;
            //Ellipse el = new Ellipse();
            //el.Stroke = System.Windows.Media.Brushes.Black;
            //el.Fill = System.Windows.Media.Brushes.DarkBlue;
            //el.HorizontalAlignment = HorizontalAlignment.Left;
            //el.VerticalAlignment = VerticalAlignment.Center;
            //el.Width = 5;
            //el.Height = 5;

        }

     
    }
}

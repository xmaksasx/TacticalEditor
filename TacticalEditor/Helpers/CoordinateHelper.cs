﻿using System;
using System.Windows;
using Elevation;

namespace TacticalEditor.Helpers
{
    class CoordinateHelper
    {
        private const double MinLatitude = -85.05112878;
        private const double MaxLatitude = 85.05112878;
        private const double MinLongitude = -180;
        private const double MaxLongitude = 180;

        public int GetElevation(double latitude, double longitude)
        {
            ISrtmData srtmData = new SrtmData(@"Elevation");
            var elevation = srtmData.GetElevation(new GeographicalCoordinates(latitude, longitude));
            return elevation ?? 0;
        }

        public double Clip(double n, double minValue, double maxValue)
        {
            return Math.Min(Math.Max(n, minValue), maxValue);
        }

        #region пиксели в LatLon

        public void PixelToLatLon(Point point, uint mapSize, out double latitude, out double longitude)
        {
            var pixelX = point.X;
            var pixelY = point.Y;

            double x = (Clip(pixelX, 0, mapSize - 1) / mapSize) - 0.5;
            double y = 0.5 - (Clip(pixelY, 0, mapSize - 1) / mapSize);
            latitude = 90 - 360 * Math.Atan(Math.Exp(-y * 2 * Math.PI)) / Math.PI;
            longitude = 360 * x;
        }

        #endregion

        #region LatLon в пиксели

        public void LatLonToPixel(double latitude, double longitude, uint mapSize, out int pixelX, out int pixelY)
        {
            latitude = Clip(latitude, MinLatitude, MaxLatitude);
            longitude = Clip(longitude, MinLongitude, MaxLongitude);

            double x = (longitude + 180) / 360;
            double sinLatitude = Math.Sin(latitude * Math.PI / 180);
            double y = 0.5 - Math.Log((1 + sinLatitude) / (1 - sinLatitude)) / (4 * Math.PI);

            pixelX = (int) Clip(x * mapSize + 0.5, 0, mapSize - 1);
            pixelY = (int) Clip(y * mapSize + 0.5, 0, mapSize - 1);
        }

        #endregion

        private const double bb2 = 8376121;
        private const double bb4 = 590.42;
        private const double bb6 = 1.68;
        private const double R = 6367558.497;
        private const double AA2 = 5333.5419;
        private const double AA4 = 4.84339;
        private const double AA6 = 0.007622;
        private const double P57 = 57.2957795130823208767;
        private const double P157 = 0.01745329251994329576;

        void Linear2Gp(double FL0, double FEast, double xx, double yy, out double lat, out double lon)
        {
            double x = yy;

            double y = xx - FEast;

            double u = x / R;

            double v = y / R;

            double tmp = 2.0 * u;

            double a1 = Math.Sin(tmp);

            double b1 = Math.Cos(tmp);

            double a2 = 2.0 * a1 * b1;

            double b2 = 1.0 - 2.0 * a1 * a1;

            double a3 = a1 * b2 + a2 * b1;

            double b3 = b1 * b2 - a1 * a2;

            tmp = 2.0 * v;

            double c1 = (Math.Exp(tmp) - Math.Exp(-tmp)) * 0.5;

            double c12 = c1 * c1;

            double d1 = Math.Sqrt(1.0 + c12);

            double c2 = 2.0 * c1 * d1;

            double d2 = 1.0 + 2.0 * c12;

            double c3 = c1 * d2 + c2 * d1;

            double d3 = c1 * c2 + d1 * d2;

            double psi = u - (bb2 * a1 * d1 + bb4 * a2 * d2 + bb6 * a3 * d3) * 1.0E-10;

            double p = v - (bb2 * b1 * c1 + bb4 * b2 * c2 + bb6 * b3 * c3) * 1.0E-10;

            double sfi = Math.Sin(psi) / ((Math.Exp(p) + Math.Exp(-p)) * 0.5);

            double sfi2 = sfi * sfi;

            double fi = Math.Atan(sfi / (Math.Sqrt(1.0 - sfi2) + 1.0E-20));

            double tgl = ((Math.Exp(p) - Math.Exp(-p)) * 0.5) / (Math.Cos(psi) + 1.0E-20);

            lon = (Math.Atan(tgl) + FL0) * P57;

            lat = ((fi + ((5645.0 * sfi2 - 531245.0) * sfi2 + 67385254.0) * sfi * Math.Cos(fi) * 1.0E-10) * 180) /
                  Math.PI;
        }

        void Gp2Linear(double FL0, double FEast, double lat, double lon, out double x, out double y)
        {

            double b = lat * P157;

            double l = lon * P157;

            l = l - FL0;

            double sinb = Math.Sin(b);

            double sin2b = sinb * sinb;

            double fi = b - ((2624.0 * sin2b + 372834.0) * sin2b + 66934216.0) * sinb * Math.Cos(b) * 1.0E-10;

            double cosfi = Math.Cos(fi);

            double thp = cosfi * Math.Sin(l);

            double psi = Math.Atan(Math.Sin(fi) / (cosfi + 1.0E-20) / (Math.Cos(l) + 1.0E-20));

            double p = 0.5 * Math.Log((1.0 + thp) / (1.0 - thp));



            double a1 = Math.Sin(2.0 * psi);

            double b1 = Math.Cos(2.0 * psi);

            double tmp = 1.0 / (1.0 - thp * thp);

            double c1 = 2.0 * thp * tmp;

            double d1 = (1.0 + thp * thp) * tmp;

            double a2 = 2.0 * a1 * b1;

            double b2 = 1.0 - 2.0 * a1 * a1;

            double c2 = 2.0 * c1 * d1;

            double d2 = 1.0 + 2.0 * c1 * c1;

            double a3 = a1 * b2 + a2 * b1;

            double b3 = b1 * b2 - a1 * a2;

            double c3 = c1 * d2 + c2 * d1;

            double d3 = d1 * d2 + c1 * c2;



            y = R * psi + AA2 * a1 * d1 + AA4 * a2 * d2 + AA6 * a3 * d3;

            x = R * p + AA2 * b1 * c1 + AA4 * b2 * c2 + AA6 * b3 * c3 + FEast;

        }

        public void Calc_LocalCord_toLatLon(double Lat, double Lon, double X, double Z, out double LatT,
            out double LonT)

        {

            double Z0, X0;

            double FL0 = Lon * P157;

            double FEast = ((int) (Lon / 6) + 1) * 1000000.0 + 500000.0;

            Gp2Linear(FL0, FEast, Lat, Lon, out Z0, out X0);

            Linear2Gp(FL0, FEast, Z0 + Z, X0 + X, out LatT, out LonT);

        }

        public void Calc_LocalCord_toXZ(double Lat, double Lon, double LatT, double LonT, out double X, out double Z)

        {

            double Z0, X0;

            double FL0 = Lon * P157;

            double FEast = ((int) (Lon / 6) + 1) * 1000000.0 + 5000000.0;

            Gp2Linear(FL0, FEast, Lat, Lon, out Z0, out X0);

            Gp2Linear(FL0, FEast, LatT, LonT, out Z, out X);

            X -= X0;

            Z -= Z0;

        }

        void Main()
        {

            //  Calc_LocalCord_toXZ(m->CenterCord.Lat, m->CenterCord.Lon, m->Cord.Lat, m->Cord.Lon, &m->Cord.x, &m->Cord.z);

        }

        public string Grad2GradMinSec(double degree)
        {
            var deg = (int) degree;
            var min = (degree - deg) * 60;
            var sec = ((degree - deg) * 60 - (int)min) * 60;
            return $"{deg}°{min:0}'{sec:0.##}\"";
        }
    }
}
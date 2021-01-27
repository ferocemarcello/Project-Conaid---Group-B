using System;
using System.Globalization;

namespace LibraryModels
{
    public class Point
    {
        public Point(double angle, double distance)
        {
            Angle = angle;
            Distance = distance;
        }

        public Point(double angle, double distance, double timeIndex) : this(angle, distance)
        {
            Xangle = timeIndex*10;
        }

        // Polar Coordinates
        public double Angle { get; set; }
        public double Distance { get; set; }
        public double Xangle { get; set; }
        // Cartesian Coordinates
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Point ToCartesian()
        {
            X = Distance*Math.Cos(_degToRad(Angle));
            Y = Distance*Math.Sin(_degToRad(Angle));
            //implement Z
            return this;
        }

        private double _degToRad(double deg)
        {
            double angleRa = deg*Math.PI/180;
            return angleRa;
        }

        public string ToCartesianString()
        {
            string result = $"{X.ToString(CultureInfo.InvariantCulture)} {Y.ToString(CultureInfo.InvariantCulture)}";
            return result;
        }

        public string ToPolarString()
        {
            string result = $"{Angle} {Distance}";
            return result;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matan
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public Point(double x, double y)
        {
            X = x;
            Y = y;
            Z = 0;
        }
        public Point(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Point RoundToIsometric(Point center) => new Vector(center, this).RoundToIsometric().E;
        public Point RoundToLength(Point center, double length)
        {
            double oldLength = Math.Sqrt(Math.Pow(X - center.X, 2) + Math.Pow(Y - center.Y, 2));
            double x = center.X + (X - center.X) * (length / oldLength);
            double y = center.Y + (Y - center.Y) * (length / oldLength);
            double z = center.Z + (Z - center.Z) * (length / oldLength);
            return new Point(x, y, z);
        }
    }
}

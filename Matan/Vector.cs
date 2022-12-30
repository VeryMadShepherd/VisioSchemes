using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matan
{
    public class Vector
    {
        public Point E { get; set; }
        public Point C { get; set; }
        private double dX { get; set; }
        private double dY { get; set; }
        private double dZ { get; set; }
        private double length = 1;
        public double Length
        {
            get
            {
                return length;
            }
            set
            {
                E = E.RoundToLength(C, value);
                CalculateD();
                length = GetLength();
            }
        }
        public Vector(double x, double y) : this(x, y, 0)
        {

        }
        public Vector(double x, double y, double z) : this(new Point(0, 0), new Point(x, y, z))
        {

        }
        public Vector(Point point) : this(new Point(0, 0), point)
        {

        }
        public Vector(Point center, Point end)
        {
            C = center;
            E = end;
            CalculateD();
            length = GetLength();
        }
        private void CalculateD()
        {
            dX = E.X - C.X;
            dY = E.Y - C.Y;
            dZ = E.Z - C.Z;
        }
        public static double operator *(Vector A, Vector B) => A.dX * B.dX + A.dY * B.dY + A.dZ * B.dZ;
        public static Vector operator *(Vector A, double B)
        {
            var begin = new Point(A.C.X, A.C.Y);
            var end = new Point(A.C.X + A.dX * B, A.C.Y + A.dY * B);
            return new Vector(begin, end);
        }
        public static Vector operator *(double B, Vector A) => A * B;
        public static Vector operator ^(Vector A, Vector B) => new Vector(A.dY * B.dZ - B.dY * A.dZ, B.dX * A.dZ - A.dX * B.dZ, A.dX * B.dY - B.dX * A.dY);
        public static Vector operator +(Vector A, Vector B)
        {
            var begin = new Point(A.C.X, A.C.Y);
            var end = new Point(A.C.X + A.dX + B.dX, A.C.Y + A.dY + B.dY);
            return new Vector(begin, end);
        }
        public static Vector operator -(Vector A, Vector B)
        {
            var begin = new Point(A.C.X, A.C.Y);
            var end = new Point(A.C.X + A.dX - B.dX, A.C.Y + A.dY - B.dY);
            return new Vector(begin, end);
        }
        public double GetAngleDeg() => Math.Atan2(dY, dX) * 180.0 / Math.PI;
        public double GetAngleRad() => Math.Atan2(dY, dX);
        public double GetAngleDeg(Vector B) => Length == 0 || B.Length == 0 ? 0 : Math.Acos((this * B) / (Length * B.Length)) * 180.0 / Math.PI;
        public double GetAngleRad(Vector B) => Length == 0 || B.Length == 0 ? 0 : Math.Acos((this * B) / (Length * B.Length));
        public Vector Reverse()
        {
            var end = new Point(C.X - (E.X - C.X), C.Y - (E.Y - C.Y), C.Z - (E.Z - C.Z));
            return new Vector(C, end);
        }
        public Vector Normalize()
        {
            if (Length == 0)
                return null;
            var end = new Point(C.X + dX / Length, C.Y + dY / Length, C.Z + dZ / Length);
            return new Vector(C, end);
        }
        public Vector RoundToIsometric()
        {
            double angle = Func.RoundToIsometric(GetAngleDeg());
            angle = Converting.D2R(angle);
            Point center = C;
            Point end = new Point(center.X + Length * Math.Cos(angle), center.Y + Length * Math.Sin(angle));
            return new Vector(center, end);
        }
        public Vector RoundToOrtho()
        {
            double angle = Func.RoundToOrtho(GetAngleDeg());
            angle = Converting.D2R(angle);
            Point center = C;
            Point end = new Point(center.X + Length * Math.Cos(angle), center.Y + Length * Math.Sin(angle));
            return new Vector(center, end);
        }
        public bool IsPositive() => E.Z - C.Z >= 0 ? true : false;
        private double GetLength() => Math.Sqrt(Math.Pow(E.X - C.X, 2) + Math.Pow(E.Y - C.Y, 2));
    }
}

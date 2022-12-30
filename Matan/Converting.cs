using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matan
{
    public struct Converting
    {
        public const double MM2Inch = 1 / 25.4;
        public const double Inch2MM = 25.4;
        public static double D2R(double angle)
        {
            return (Math.PI * angle) / 180.0;
        }
        public static double R2D(double angle)
        {
            return angle * (180.0 / Math.PI);
        }
        public static Vector DegToVector(double angle) => new Vector(1 * Math.Cos(D2R(angle)), 1 * Math.Sin(D2R(angle)));
        public static Vector RadToVector(double angle) => new Vector(1 * Math.Cos(angle), 1 * Math.Sin(angle));
    }
}

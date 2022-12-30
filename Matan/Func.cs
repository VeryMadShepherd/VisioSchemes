using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matan
{
    public static class Func
    {
        public static double ReverseAngleDeg(double Angle) => AngleShift(Angle, 180);
        public static double GetAngleDeg(double AngleA, double AngleB)
        {
            Vector A = Converting.DegToVector(AngleA);
            Vector B = Converting.DegToVector(AngleB);
            return A.GetAngleDeg(B);
        }
        public static double GetAngleRad(double AngleA, double AngleB)
        {
            Vector A = Converting.RadToVector(AngleA);
            Vector B = Converting.RadToVector(AngleB);
            return A.GetAngleRad(B);
        }
        public static double GetAngleDeg(Point center, Point end) => new Vector(center, end).GetAngleDeg();
        public static double GetAngleRad(Point center, Point end) => new Vector(center, end).GetAngleRad();

        public static double RoundToIsometric(double Angle)
        {
            double value = 0;
            switch (Angle)
            {
                case double n when n >= 0 && n < 60: //вверх-вправо
                    value = 30;
                    break;
                case double n when n >= 60 && n < 120: //вверх
                    value = 90;
                    break;
                case double n when n >= 120 && n <= 180: //вверх-влево
                    value = 150;
                    break;
                case double n when n > -180 && n < -120: //вниз-влево
                    value = -150;
                    break;
                case double n when n < -60 && n >= -120: //вниз
                    value = -90;
                    break;
                case double n when n < 0 && n >= -60: //вниз-вправо
                    value = -30;
                    break;
                default:
                    break;
            }
            return value;
        }
        public static double RoundToOrtho(double Angle)
        {
            double value = 0;
            Debug.Print(Angle.ToString());
            switch (Angle)
            {
                case double n when n >= -22.5 && n < 22.5: //0
                    value = 0;
                    break;
                case double n when n >= 22.5 && n < 67.5: //45
                    value = 45;
                    break;
                case double n when n >= 67.5 && n < 112.5: //90
                    value = 90;
                    break;
                case double n when n >= 112.5 && n < 157.5: //135
                    value = 135;
                    break;
                case double n when n >= 157.5 || n < -157.5: //180
                    value = 180;
                    break;
                case double n when n >= -157.5 && n < -112.5: //-135
                    value = -135;
                    break;
                case double n when n >= -112.5 && n < -67.5: //-90
                    value = -90;
                    break;
                case double n when n >= -67.5 && n < -22.5: //-45
                    value = -45;
                    break;
                default:
                    break;
            }
            return value;
        }
        public static double AngleShift(double angle1, double angle2)
        {
            double sum = angle1 + angle2;
            double buffer;
            if (sum > 180)
            {
                buffer = sum - 180;
                sum = -180 + buffer;
            }
            else if (sum < -180)
            {
                buffer = Math.Abs(sum) - 180;
                sum = 180 - buffer;
            }
            return sum;
        }
        public static void FindOrthoCoordinates(double x, double y, double Cx, double Cy, out double Ox, out double Oy)
        {
            double Radius = Math.Sqrt(Math.Pow(x - Cx, 2) + Math.Pow(y - Cy, 2));
            double Direction = FindOrthoDirection(x, y, Cx, Cy);
            Ox = Cx + Radius * Math.Cos(Direction * Math.PI / 180);
            Oy = Cy + Radius * Math.Sin(Direction * Math.PI / 180);
        }

        public static double FindOrthoDirection(double x, double y, double Cx, double Cy)
        {
            double Angle = Math.Atan2(y - Cy, x - Cx) * 180.0 / Math.PI;
            double value = 0;
            switch (Angle)
            {
                case double n when n >= 0 && n < 60: //вверх-вправо
                    value = 30;
                    break;
                case double n when n >= 60 && n < 120: //вверх
                    value = 90;
                    break;
                case double n when n >= 120 && n <= 180: //вверх-влево
                    value = 150;
                    break;
                case double n when n > -180 && n < -120: //вниз-влево
                    value = -150;
                    break;
                case double n when n < -60 && n >= -120: //вниз
                    value = -90;
                    break;
                case double n when n < 0 && n >= -60: //вниз-вправо
                    value = -30;
                    break;
                default:
                    break;
            }
            return value;
        }
        public static double RoundDirectionToOrtho(double direction)
        {
            double value = 0;
            switch (direction)
            {
                case double n when n >= 0 && n < 60: //вверх-вправо
                    value = 30;
                    break;
                case double n when n >= 60 && n < 120: //вверх
                    value = 90;
                    break;
                case double n when n >= 120 && n <= 180: //вверх-влево
                    value = 150;
                    break;
                case double n when n > -180 && n < -120: //вниз-влево
                    value = -150;
                    break;
                case double n when n < -60 && n >= -120: //вниз
                    value = -90;
                    break;
                case double n when n < 0 && n >= -60: //вниз-вправо
                    value = -30;
                    break;
                default:
                    break;
            }
            return value;
        }
        public static double PolarDirection(double x, double y, double Cx, double Cy)
        {
            double Angle = Math.Atan2(y - Cy, x - Cx) * 180.0 / Math.PI;
            return Angle;
        }
    }
}

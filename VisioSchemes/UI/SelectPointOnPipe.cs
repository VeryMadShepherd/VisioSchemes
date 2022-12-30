using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Extension;
using Matan;
using Microsoft.Office.Interop.Visio;

namespace VisioSchemes.UI
{
    internal abstract class SelectPointOnPipe : InterfaceCommand
    {
        protected Shape CurrentSignGreen; //"Allowed" sign
        protected Shape CurrentSignRed; //"Not allowed" sign
        protected Vector Pipe;
        protected static bool Prepare(Shape pipe)
        {
            Stop();
            double length = Math.Sqrt
                (
                Math.Pow(Math.Abs(pipe.GetBeginX()) - Math.Abs(pipe.GetEndX()), 2)
                +
                Math.Pow(Math.Abs(pipe.GetBeginY()) - Math.Abs(pipe.GetEndY()), 2)
                );
            if (length < PConst.MinimumPipeGlueLength)
            {
                MessageBox.Show(PConst.PipeIsTooShort);
                return false;
            }
            return true;
        }
        protected SelectPointOnPipe(Shape pipe)
        {
            var B = new Point(pipe.GetBeginX(), pipe.GetBeginY());
            var E = new Point(pipe.GetEndX(), pipe.GetEndY());
            Pipe = new Vector(B, E);
            CurrentMouseMoveEvent = MouseMove;
            Center = new Matan.Point(0, 0);
            CurrentSignGreen = CreateSign(CurrentPage, PConst.PointSelect);
        }
        internal override void MouseMove(double x, double y)
        {
            //Catching bug
            if (CurrentSignGreen == null || CurrentSignRed == null)
            {
                MessageBox.Show(PConst.UI_SignIsNull);
                Stop();
            }
            //Main
            var AB = new Vector(Pipe.E.X - Pipe.C.X, Pipe.E.Y - Pipe.C.Y);
            var AC = new Vector(x - Pipe.C.X, y - Pipe.C.Y);
            var projection = ((AB * AC) / (Math.Pow(Pipe.Length, 2) / 2)) / 2;
            switch (true)
            {
                case true when (projection >= 1):
                    {
                        Center.X = Pipe.E.X;
                        Center.Y = Pipe.E.Y;
                        break;
                    }
                case true when (projection <= 0):
                    {
                        Center.X = Pipe.C.X;
                        Center.Y = Pipe.C.Y;
                        break;
                    }
                default:
                    {
                        Center.X = Pipe.C.X + Pipe.Length * Math.Cos(Pipe.GetAngleRad()) * projection;
                        Center.Y = Pipe.C.Y + Pipe.Length * Math.Sin(Pipe.GetAngleRad()) * projection;
                        break;
                    }
            }
            SetMarks(projection);
        }
        protected abstract void SetMarks(double projection);
    }
}

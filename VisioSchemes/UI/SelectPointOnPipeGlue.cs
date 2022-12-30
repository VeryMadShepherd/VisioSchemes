using Extension;
using Microsoft.Office.Interop.Visio;

namespace VisioSchemes.UI
{
    internal class SelectPointOnPipeGlue : SelectPointOnPipe
    {
        internal Place GetPlace;
        internal static InterfaceCommand Start(Shape pipe)
        {
            if (Prepare(pipe) == false)
            {
                return null;
            }
            CurrentInstance = new SelectPointOnPipeGlue(pipe);
            return CurrentInstance;
        }
        private SelectPointOnPipeGlue(Shape pipe) : base(pipe)
        {
            CurrentSignRed = CreateSign(CurrentPage, PConst.PointSelectPlace);
        }

        protected override void SetMarks(double projection)
        {
            var distance = Pipe.Length * projection;
            switch (true)
            {
                case true when distance >= Pipe.Length - 0.5:
                    GetPlace = Place.End;
                    CurrentSignRed.SetCenter(Pipe.E.X, Pipe.E.Y);
                    CurrentSignRed.SetAngle(Pipe.GetAngleDeg());
                    CurrentSignGreen.SetCenter(Nah, Nah);
                    break;
                case true when distance <= 0.5:
                    GetPlace = Place.Begin;
                    CurrentSignRed.SetCenter(Pipe.C.X, Pipe.C.Y);
                    CurrentSignRed.SetAngle(Matan.Func.ReverseAngleDeg(Pipe.GetAngleDeg()));
                    CurrentSignGreen.SetCenter(Nah, Nah);
                    break;
                default:
                    GetPlace = Place.Middle;
                    CurrentSignGreen.SetCenter(Center.X, Center.Y);
                    CurrentSignGreen.SetAngle(Pipe.GetAngleDeg());
                    CurrentSignRed.SetCenter(Nah, Nah);
                    break;
            }
        }

        protected override void Dispose()
        {
            CurrentSignGreen?.Delete();
            CurrentSignRed?.Delete();
        }
    }

}

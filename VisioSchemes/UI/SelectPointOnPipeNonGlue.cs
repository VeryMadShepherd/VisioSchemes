using Extension;
using Microsoft.Office.Interop.Visio;

namespace VisioSchemes.UI
{
    internal class SelectPointOnPipeNonGlue : SelectPointOnPipe
    {
        internal static InterfaceCommand Start(Shape pipe)
        {
            if (Prepare(pipe) == false)
            {
                return null;
            }
            CurrentInstance = new SelectPointOnPipeNonGlue(pipe);
            return CurrentInstance;
        }
        private SelectPointOnPipeNonGlue(Shape pipe) : base(pipe)
        {
            CurrentSignRed = CreateSign(CurrentPage, PConst.PointSelectForbidden);
        }

        protected override void SetMarks(double projection)
        {
            if (IsClose(projection))
            {
                IsAllowed = false;
                SetPlace(CurrentSignRed, CurrentSignGreen);
            }
            else
            {
                IsAllowed = true;
                SetPlace(CurrentSignGreen, CurrentSignRed);
            }
        }
        protected void SetPlace(Shape toCenter, Shape away)
        {
            toCenter.SetCenter(Center.X, Center.Y);
            toCenter.SetAngle(Pipe.GetAngleDeg());
            away.SetCenter(Nah, Nah);
        }
        protected bool IsClose(double projection)
        {
            if (projection >= 0.92 || projection <= 0.08)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected override void Dispose()
        {
            CurrentSignGreen?.Delete();
            CurrentSignRed?.Delete();
        }
    }
}

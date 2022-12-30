using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extension;
using Matan;
using Microsoft.Office.Interop.Visio;

namespace VisioSchemes.UI
{
    internal class UserDirectionArrow : InterfaceCommand
    {
        private Shape CurrentSignGreen; //разрешающий символ
        private Shape CurrentSignRed; //запрещающий символ
        private double Cx;
        private double Cy;
        private bool IsOrthoIsometrics = true;
        internal List<double> ForbiddenDirections { get; set; }
        internal static InterfaceCommand Start(double x, double y, List<double> forbiddenDirections = null, bool isOrthoIsometrics = true)
        {
            Stop();
            CurrentInstance = new UserDirectionArrow(x, y, isOrthoIsometrics, forbiddenDirections);
            return CurrentInstance;
        }
        private UserDirectionArrow(double x, double y, bool isOrthoIsometrics, List<double> ForbiddenDirections = null)
        {
            IsOrthoIsometrics = isOrthoIsometrics;
            Cx = x;
            Cy = y;
            this.ForbiddenDirections = ForbiddenDirections ?? new List<double>();
            CurrentSignGreen = CreateSign(CurrentPage, "Arrow");
            CurrentSignRed = CreateSign(CurrentPage, "Cross");
            CurrentMouseMoveEvent = MouseMove;
        }
        internal override void MouseMove(double x, double y)
        {
            //блок отлова исключения
            if (CurrentSignRed == null || CurrentSignGreen == null)
            {
                System.Windows.Forms.MessageBox.Show("Обнаружена ошибка: CurrentSign = null в UserDirectionArrow. Перезапустите текущую команду.");
                Stop();
            }
            //основной блок
            var vDirection = new Vector(new Point(Cx, Cy), new Point(x, y));
            double direction = 0;
            if (IsOrthoIsometrics)
            {
                direction = System.Math.Round(vDirection.RoundToIsometric().GetAngleDeg());
            }
            else
            {
                direction = System.Math.Round(vDirection.RoundToOrtho().GetAngleDeg());
            }
            if (ForbiddenDirections.Contains(direction))
            {
                CurrentSignGreen.SetCenter(Nah, Nah);
                CurrentSignRed.SetAngle(direction);
                CurrentSignRed.SetCenter(Cx, Cy);
                IsAllowed = false;
            }
            else
            {
                CurrentSignRed.SetCenter(Nah, Nah);
                CurrentSignGreen.SetAngle(direction);
                CurrentSignGreen.SetCenter(Cx, Cy);
                IsAllowed = true;
            }
        }

        protected override void Dispose()
        {
            CurrentSignGreen?.Delete();
            CurrentSignRed?.Delete();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Extension;
using Matan;
using Microsoft.Office.Interop.Visio;
using VisioSchemes.UI;
using VisioSchemes.Commands.Misc;

namespace VisioSchemes.Commands.DrawingCommands
{
    internal class DrawPipe : DrawingCommand
    {
        private readonly string CommandName = PConst.PipePrompt;
        private double PreviousDirection { get; set; }
        private Shape CurrentPipe { get; set; }
        private Shape PreviousPipe { get; set; }
        private bool IsConnected { get; set; }
        internal static void Start()
        {
            OnStart();
            CurrentInstance = new DrawPipe();
        }
        private DrawPipe()
        {
            ThisAddIn.Ribbon.SetTip(CommandName);
            LastCommand = Start;
            CurrentMouseUpEvent = FirstClick;
            CurrentInterface = SelectShape.Start(SelectShape.Flags.NonFlange, SelectShape.SignType.Cross);
        }
        internal override void Dispose()
        {
            CurrentPipe?.Delete();
            if (Center != null)
            {
                CreateConnector(Center.X, Center.Y, CurrentDirection, ConnectorType.Weld);
            }
            ThisAddIn.MainWindow.DeselectAll();
        }
        private void FirstClick(double x, double y)
        {
            System.Diagnostics.Debug.Print("HUI");
            if (CurrentInterface.SelectedElement != null)
            {
                IsConnected = true;
                Center = new Point(CurrentInterface.SelectedElement.GetX(), CurrentInterface.SelectedElement.GetY());
            }
            else
            {
                Center = new Point(x, y);
            }
            InterfaceCommand.Stop();
            CurrentPipe = NewPipe();
            CurrentMouseMoveEvent = MouseMove;
            CurrentMouseUpEvent = SecondClick;
            UserDirectionArrow.Start(Center.X, Center.Y);
        }
        private void SecondClick(double x, double y)
        {
            var dVector = new Vector(Center, new Point(x, y));
            dVector = IsOrtho() ? dVector.RoundToIsometric() : dVector;
            CurrentDirection = dVector.GetAngleDeg();
            if (IsConnected == false)
            {
                CreateConnector(Center.X, Center.Y, Func.ReverseAngleDeg(CurrentDirection), ConnectorType.Weld);
                CurrentPage.Drop(ThisAddIn.VisioDoc.Masters.ItemU[PConst.Weld], Center.X, Center.Y);
            }
            CurrentPipe.SetEnd(dVector.E.X, dVector.E.Y);
            Center.X = dVector.E.X;
            Center.Y = dVector.E.Y;
            PreviousPipe = CurrentPipe;
            CurrentPipe = NewPipe();
            UserDirectionArrow.Start(Center.X, Center.Y, new List<double> { Func.ReverseAngleDeg(CurrentDirection) });
            CurrentMouseUpEvent = MainClick;
        }
        /// <summary>
        /// Repeating after the first two until the user stops the command
        /// </summary>
        private void MainClick(double x, double y)
        {
            ActionAllowed = false;
            Intermediate();
        }
        /// <summary>
        /// This functions remains due to preserve logic of the extended project. In that case it should find needed element in database.
        /// </summary>
        protected override void Find()
        {
            Intermediate();
        }
        /// <summary>
        /// Creating an elbow of the pipeline
        /// </summary>
        private void Intermediate()
        {
            //inner branch
            double size = DrawElbow.BranchLength;
            double angle = Converting.D2R(PreviousPipe.GetAngle());
            var center = new Point(PreviousPipe.GetEndX(), PreviousPipe.GetEndY());
            var end = new Point(PreviousPipe.GetEndX() - size * Math.Cos(angle), PreviousPipe.GetEndY() - size * Math.Sin(angle));
            PreviousPipe.SetEnd(end.X, end.Y);
            var inner = new Branch(center, end, size);
            //outer branch
            angle = Converting.D2R(CurrentPipe.GetAngle());
            end = new Point(CurrentPipe.GetBeginX() + size * Math.Cos(angle), CurrentPipe.GetBeginY() + size * Math.Sin(angle));
            var outer = new Branch(center, end, size);
            CurrentPipe.SetBegin(end.X, end.Y);
            //final
            DrawElbow.Create(inner, outer, CurrentPage, false);
            PreviousDirection = CurrentDirection;
            CurrentDirection = Func.RoundDirectionToOrtho(CurrentPipe.GetAngle());
            Center.X = CurrentPipe.GetEndX();
            Center.Y = CurrentPipe.GetEndY();
            PreviousPipe = CurrentPipe;
            CurrentPipe = NewPipe();
            UserDirectionArrow.Start(Center.X, Center.Y, new List<double> { Func.ReverseAngleDeg(CurrentDirection) });
            ActionAllowed = true;
        }
        private void MouseMove(double x, double y)
        {
            if (CurrentPipe == null)
            {
                Stop();
                MessageBox.Show("Error: CurrentPipe is null. Please restart command.");
                return;
            }
            var dVector = new Vector(Center, new Point(x, y));
            dVector = IsOrtho() ? dVector.RoundToIsometric() : dVector;
            CurrentPipe.SetEnd(dVector.E.X, dVector.E.Y);
        }
        /// <summary>
        /// Creating a simple line if the pipe
        /// </summary>
        private Shape NewPipe()
        {
            Shape result = CurrentPage.DrawLine(Center.X, Center.Y, Center.X, Center.Y);
            ThisAddIn.VisioApp.ActiveWindow.DeselectAll();
            return result;
        }
        internal static Shape NewPipe(Page page, double diameter, Point begin, Point end)
        {
            Shape result = page.DrawLine(begin.X, begin.Y, end.X, end.Y);
            ThisAddIn.VisioApp.ActiveWindow.DeselectAll();
            return result;
        }
        private bool IsStraightElbow() =>
            Math.Round(Math.Abs(Func.GetAngleDeg(CurrentDirection, PreviousDirection))) == 120
            ||
            Math.Round(Math.Abs(Func.GetAngleDeg(CurrentDirection, PreviousDirection))) == 60;
    }
}

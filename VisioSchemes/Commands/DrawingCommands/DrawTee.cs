using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Extension;
using Matan;
using Microsoft.Office.Interop.Visio;
using VisioSchemes.Commands.Misc;
using VisioSchemes.UI;

namespace VisioSchemes.Commands.DrawingCommands
{
    internal class DrawTee : DrawingCommand
    {
        private string CommandName = PConst.TeePrompt;
        internal static double BranchLength = Converting.MM2Inch * Properties.Settings.Default.TeeLength;
        private readonly Branch[] Branch = new Branch[3];
        private int BranchNumber = 0;
        internal static void Start()
        {
            OnStart();
            CurrentInstance = new DrawTee();
        }
        private DrawTee()
        {
            ThisAddIn.Ribbon.SetTip(CommandName);
            LastCommand = Start;
            CurrentMouseUpEvent = ChoosePosition;
            CurrentInterface = SelectShape.Start(SelectShape.Flags.NonFlange, SelectShape.SignType.Arrow);
        }
        internal override void Dispose()
        {
            foreach (var branch in Branch)
            {
                if (branch != null)
                {
                    branch.Dispose();
                }
            }
            ThisAddIn.MainWindow.DeselectAll();
        }
        protected override void ChoosePosition(double x, double y)
        {
            if (CurrentInterface.SelectedElement != null)
            {
                SourceConnector = CurrentInterface.SelectedElement;
                Center = new Point(
                    SourceConnector.GetX() + BranchLength * Math.Cos(Converting.D2R(SourceConnector.GetAngle())),
                    SourceConnector.GetY() + BranchLength * Math.Sin(Converting.D2R(SourceConnector.GetAngle()))
                    );
            }
            else
            {
                Center = new Point(x, y);
            }
            ActionAllowed = false;
            Find();
        }
        /// <summary>
        /// This functions remains due to preserve logic of the extended project. In that case it should find needed element in database.
        /// </summary>
        protected override void Find()
        {
            Intermediate();
        }
        /// <summary>
        /// Sets local UI data
        /// </summary>
        private void Intermediate()
        {
            var forbidden = new List<double>();
            if (SourceConnector != null)
            {
                Branch[BranchNumber] = new Branch(Center,
                    new Point(SourceConnector.GetX(), SourceConnector.GetY()),
                    BranchLength);
                Branch[BranchNumber].Connector = SourceConnector;
                BranchNumber++;
                forbidden.Add(Branch[0].Vector.GetAngleDeg());
            }
            CurrentInterface = UserDirectionArrow.Start(Center.X, Center.Y, forbidden);
            ActionAllowed = true;
            CurrentMouseUpEvent = ChooseBranch;
        }
        private void ChooseBranch(double x, double y)
        {
            Branch[BranchNumber] = new Branch(Center, new Point(x, y), BranchLength);
            BranchNumber++;
            var forbidden = new List<double>
            {
                Math.Round(Branch[0].Vector.GetAngleDeg())
            };
            switch (BranchNumber)
            {
                case 1:
                    {
                        CurrentInterface = UserDirectionArrow.Start(Center.X, Center.Y, forbidden);
                        break;
                    }
                case 2:
                    {
                        forbidden = FindForbiddenDirection();
                        CurrentInterface = UserDirectionArrow.Start(Center.X, Center.Y, forbidden);
                        break;
                    }
                case 3:
                    {
                        Create(CurrentPage, Branch);
                        break;
                    }
                default:
                    {
                        Stop();
                        MessageBox.Show("Error!");
                        break;
                    }
            }
        }
        internal static void Create(Page page, Branch[] branches)
        {
            Selection selection = ThisAddIn.VisioApp.ActiveWindow.Selection;
            selection.DeselectAll();
            branches[0].CreateEnding(page, "Weld");
            branches[1].CreateEnding(page, "Weld");
            branches[2].CreateEnding(page, "Weld");
            for (int i = 0; i < 3; i++)
            {
                branches[i].CreateConnector();
                selection.Select(branches[i].Line, 2);
                selection.Select(branches[i].Ending, 2);
            }
            selection.Group();
            selection = ThisAddIn.VisioApp.ActiveWindow.Selection;
            foreach (Shape shape in selection)
            {
                shape.SendToBack();
            }
            for (int i = 0; i < 3; i++)
            {
                branches[i] = null;
            }
            Stop();
        }
        /// <summary>
        /// Tee can only has T-form, so after every step of drawing some of branch directions became forbidden. 
        /// This also takes into account the reducing diameter if there is one, but not in this case, 
        /// since the tee is not linked to the database ("Find" function is about it)
        /// </summary>
        /// <returns></returns>
        private List<double> FindForbiddenDirection()
        {
            var result = new List<double>
            {
                Math.Round(Branch[0].Vector.GetAngleDeg(), 0),
                Math.Round(Branch[1].Vector.GetAngleDeg(), 0)
            };
            double angle = Math.Round(Branch[0].Vector.GetAngleDeg(Branch[1].Vector), 0);
            double sign = Math.Sign((Branch[0].Vector ^ Branch[1].Vector).E.Z);
            double toShift = Math.Round(Branch[1].Vector.GetAngleDeg(), 0);
            switch (angle)
            {
                case 60.0:
                    {
                        result.Add(Func.AngleShift(toShift, (60 * sign)));
                        result.Add(Func.AngleShift(toShift, (-120 * sign)));
                        break;
                    }
                case 120.0:
                    {
                        result.Add(Func.AngleShift(toShift, (-60 * sign)));
                        result.Add(Func.AngleShift(toShift, (120 * sign)));
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            return result;
        }
    }
}

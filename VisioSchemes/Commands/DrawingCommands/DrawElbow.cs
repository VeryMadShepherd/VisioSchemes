using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extension;
using Matan;
using Microsoft.Office.Interop.Visio;
using VisioSchemes.Commands.Misc;
using VisioSchemes.UI;

namespace VisioSchemes.Commands.DrawingCommands
{
    internal class DrawElbow : DrawingCommand
    {
        private Branch Inner;
        private Branch Outer;
        private string CommandName = PConst.ElbowPrompt;
        internal static double BranchLength = Converting.MM2Inch * Properties.Settings.Default.ElbowRadius;
        internal static void Start()
        {
            OnStart();
            CurrentInstance = new DrawElbow();
        }
        private DrawElbow()
        {
            ThisAddIn.Ribbon.SetTip(CommandName);
            LastCommand = Start;
            CurrentMouseUpEvent = ChoosePosition;
            CurrentInterface = SelectShape.Start(SelectShape.Flags.All, SelectShape.SignType.Arrow);
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
                var point = new Point(SourceConnector.GetX(), SourceConnector.GetY());
                Inner = new Branch(Center, point, BranchLength);
                Inner.Connector = SourceConnector;
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
            if (Inner != null)
            {
                forbidden.Add(Inner.Vector.GetAngleDeg());
            }
            ActionAllowed = true;
            CurrentInterface = UserDirectionArrow.Start(Center.X, Center.Y, forbidden);
            CurrentMouseUpEvent = ChooseBranch;
        }
        private void ChooseBranch(double x, double y)
        {
            if (Inner == null)
            {
                var point = new Point(x, y);
                Inner = new Branch(Center, point, BranchLength);
                var forbidden = new List<double>
                {
                    Inner.Vector.GetAngleDeg()
                };
                CurrentInterface = UserDirectionArrow.Start(Center.X, Center.Y, forbidden);
                return;
            }
            if (Outer == null)
            {
                var point = new Point(x, y);
                Outer = new Branch(Center, point, BranchLength);
                Outer.CreateConnector();
                Create(Inner, Outer, CurrentPage);
                return;
            }
            Stop();//Anti-bug
        }
        internal static void Create(Branch inner, Branch outer, Page page, bool IsStop = true)
        {
            //Ends of the elbow
            GetConnectors(inner.Vector, outer.Vector, out string mInner, out string mOuter);
            inner.CreateEnding(page, mInner);
            outer.CreateEnding(page, mOuter);
            inner.CreateConnector();
            outer.CreateConnector();
            //The shape of the elbow
            Selection elbowSelection = ThisAddIn.MainWindow.Selection;
            elbowSelection.DeselectAll();
            elbowSelection.Select(inner.Line, 2);
            elbowSelection.Select(outer.Line, 2);
            elbowSelection.Join();
            elbowSelection = ThisAddIn.MainWindow.Selection;
            foreach (Shape shape in elbowSelection)
            {
                shape.SetLineRound(5);
            }
            elbowSelection = ThisAddIn.MainWindow.Selection;
            elbowSelection.Select(inner.Ending, 2);
            elbowSelection.Select(outer.Ending, 2);
            elbowSelection.Group();
            elbowSelection = ThisAddIn.MainWindow.Selection;
            //Remains of the extended project. Fill the shape of the elbow by data
            //double direction = (inner.Vector + outer.Vector).Reverse().GetAngleDeg();
            foreach (Shape shape in elbowSelection)
            {
                //shape.SetElementData(page, direction);
            }
            //Final
            CreateConnector(inner.Vector.C.X, inner.Vector.C.Y, 0, ConnectorType.Dimension);
            ThisAddIn.MainWindow.Selection.DeselectAll();
            if (IsStop)
                Stop();
        }
        /// <summary>
        /// This function remains due to preserve logic of the extended project. In that case it should find correct connector type due to 
        /// type of an elbow (socket, thread, weld, etc).
        /// </summary>
        private static void GetConnectors(Vector inner, Vector outer, out string innerJoint, out string outerJoint)
        {
            innerJoint = PConst.Weld;
            outerJoint = PConst.Weld;
        }
    }
}

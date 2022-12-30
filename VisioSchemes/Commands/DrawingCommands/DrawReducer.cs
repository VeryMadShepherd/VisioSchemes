using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matan;
using Microsoft.Office.Interop.Visio;
using VisioSchemes.UI;

namespace VisioSchemes.Commands.DrawingCommands
{
    internal class DrawReducer : DrawingCommand
    {
        private string CommandName = PConst.ReducerPrompt;
        internal static void Start()
        {
            OnStart();
            CurrentInstance = new DrawReducer();
        }
        private DrawReducer()
        {
            ThisAddIn.Ribbon.SetTip(CommandName);
            LastCommand = Start;
            CurrentMouseUpEvent = ChoosePosition;
            CurrentInterface = SelectShape.Start(SelectShape.Flags.NonFlange, SelectShape.SignType.Arrow);
        }
        internal override void Dispose()
        {
            ThisAddIn.MainWindow.DeselectAll();
        }

        protected override void Find()
        {
            Intermediate();
        }
        private void Intermediate()
        {
            bool isConnected = SourceConnector == null ? false : true;
            CreateReducer(CurrentPage, Center, CurrentDirection, true, isConnected);
            ActionAllowed = true;
        }
        private static void CreateReducer(Page page, Point source, double direction, bool isStraight, bool isConnected)
        {
            double orthoDirection = Func.RoundToIsometric(direction);
            string masterName = PConst.ReducerConcentric;
            string masterOrientation = isStraight ? "Str" : "Rev";
            masterName = masterName + masterOrientation; 
            masterName = GetMasterName(direction, masterName);
            Shape image = ThisAddIn.VisioApp.ActivePage.Drop(ThisAddIn.VisioDoc.Masters.ItemU[masterName], source.X, source.Y);
            Selection vsoSelection = ThisAddIn.VisioApp.ActiveWindow.Selection;
            vsoSelection.DeselectAll();
            vsoSelection.Select(image, 2);
            if (ShouldFlipVertical(direction))
                vsoSelection.FlipVertical();
            if (ShouldFlipHorizontal(direction))
                vsoSelection.FlipHorizontal();
            double angle;
            if (direction != orthoDirection)
            {
                angle = Func.GetAngleDeg(direction, orthoDirection);
                vsoSelection.Rotate(angle, VisUnitCodes.visDegrees);
            }
            double distanceX = image.GetLength() * Math.Cos(Converting.D2R(direction));
            double distanceY = image.GetLength() * Math.Sin(Converting.D2R(direction));
            var connector = CreateConnector(source.X + distanceX, source.Y + distanceY, direction, ConnectorType.Weld);
            connector.BringToFront();
            if (isConnected == false)
            {
                connector = CreateConnector(source.X, source.Y, Func.ReverseAngleDeg(direction), ConnectorType.Weld);
                connector.BringToFront();
            }
            Stop();
        }
        private static bool ShouldFlipVertical(double direction)
        {
            bool FlipVertical = false;
            double orthoDirection = Func.RoundToIsometric(direction);
            switch (orthoDirection)
            {
                case -150: 
                case -90: 
                case -30: 
                    FlipVertical = true;
                    break;
                default:
                    break;
            }
            return FlipVertical;
        }
        private static bool ShouldFlipHorizontal(double direction)
        {
            bool flipHorizontal = false;
            double orthoDirection = Func.RoundToIsometric(direction);
            switch (orthoDirection)
            {
                case 150: 
                case -150: 
                    flipHorizontal = true;
                    break;
                default:
                    break;
            }
            return flipHorizontal;
        }
    }
}

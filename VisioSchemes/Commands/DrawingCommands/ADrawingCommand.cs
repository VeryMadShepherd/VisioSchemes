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
    internal abstract class DrawingCommand : Command
    {
        protected Shape SourceConnector;
        protected double CurrentDirection = 0;

        protected DrawingCommand()
        {

        }
        protected virtual void ChoosePosition(double x, double y)
        {
            if (CurrentInterface.SelectedElement != null)
            {
                ActionAllowed = false;
                SourceConnector = CurrentInterface.SelectedElement;
                Center = new Point(SourceConnector.GetX(), SourceConnector.GetY());
                CurrentDirection = SourceConnector.GetAngle();
                InterfaceCommand.Stop();
                Find();
            }
            else
            {
                Center = new Point(x, y);
                CurrentInterface = UserDirectionArrow.Start(Center.X, Center.Y);
                CurrentMouseUpEvent = ChooseDirection;
            }
        }
        protected virtual void ChooseDirection(double x, double y)
        {
            ActionAllowed = false;
            CurrentDirection = GetDirection(Center, x, y);
            InterfaceCommand.Stop();
            Find();
        }
        protected static string GetMasterName(double direction, string MasterName)
        {
            double orthoDirection = Func.RoundToIsometric(direction);
            switch (orthoDirection)
            {
                case 30:
                case 150:
                case -150:
                case -30:
                    MasterName += PConst.Diagonal;
                    break;
                case 90:
                case -90:
                    MasterName += PConst.Vertical;
                    break;
                default:
                    MasterName += PConst.Vertical;
                    break;
            }
            return MasterName;
        }
        protected static Shape GetImage(string name, double direction, Point source, Page page)
        {
            string drawingName = GetMasterName(direction, name);
            return page.Drop(ThisAddIn.VisioDoc.Masters.ItemU[drawingName], source.X, source.Y);
        }
        protected static Point GetEndPoint(Shape image, double direction)
        {
            var source = new Point(image.GetX(), image.GetY());
            var end = new Point(
                source.X + image.GetLength() * Math.Cos(Converting.D2R(direction)),
                source.Y + image.GetLength() * Math.Sin(Converting.D2R(direction))
                );
            return end;
        }


        /// <summary>
        /// This functions remains due to preserve logic of the extended project. In that case it should find needed element in database.
        /// </summary>
        protected abstract void Find();

        protected static void FinalizeCreationObject(Shape image, Page page, double direction, ref ShapeBlock block)
        {
            //block.ElementMark = image.SetElementData(page, Func.AngleShift(direction, 45));
        }

    }
}

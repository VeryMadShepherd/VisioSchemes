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
    internal class DrawFlange : DrawingCommand
    {
        private string CommandName = PConst.FlangePrompt;
        internal static void Start()
        {
            OnStart();
            CurrentInstance = new DrawFlange();
        }
        private DrawFlange()
        {
            ThisAddIn.Ribbon.SetTip(CommandName);
            LastCommand = Start;
            CurrentMouseUpEvent = ChoosePosition;
            CurrentInterface = SelectShape.Start(SelectShape.Flags.All, SelectShape.SignType.Arrow);
        }
        /// <summary>
        /// This functions remains due to preserve logic of the extended project. In that case it should find needed element in database.
        /// </summary>
        protected override void Find()
        {
            Create(CurrentPage, Center, CurrentDirection, SourceConnector);
            Stop();
        }
        internal static ShapeBlock Create(Page page, Point source, double direction, Shape connector = null)
        {
            bool isResponding = connector.GetConnectorType() == ConnectorType.Flange ? true : false;
            var image = GetImage(PConst.FlangeWN, direction, source, page);
            var end = GetEndPoint(image, direction);
            var block = new ShapeBlock(image);
            switch (true)
            {
                case true when connector == null:
                    image.SetAngle(direction);
                    connector = CreateConnector(source.X, source.Y, Func.ReverseAngleDeg(direction), ConnectorType.Weld);
                    block.OuterConnector = CreateConnector(end.X, end.Y, direction, ConnectorType.Flange);
                    break;
                case true when isResponding:
                    image.FlipVertical();
                    image.FlipHorizontal();
                    image.SetAngle(connector.GetAngle());
                    image.SetCenter(end.X, end.Y);
                    block.OuterConnector = CreateConnector(end.X, end.Y, direction, ConnectorType.Weld);
                    break;
                //case true when isResponding && element.InnerJoint == Joint.Flange:
                //    image.SetAngle(connector.GetAngle());
                //    image.SetCenter(end.X, end.Y);
                //    break;
                default:
                    image.SetAngle(connector.GetAngle());
                    block.OuterConnector = CreateConnector(end.X, end.Y, direction, ConnectorType.Flange);
                    break;
            }
            block.InnerConnector = connector;
            FinalizeCreationObject(image, page, direction, ref block);
            return block;
        }
    }
}

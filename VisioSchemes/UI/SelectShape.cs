using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extension;
using Microsoft.Office.Interop.Visio;

namespace VisioSchemes.UI
{
    internal class SelectShape : InterfaceCommand
    {
        internal enum SignType
        {
            Arrow,
            Cross,
            Line
        }
        internal enum Flags
        {
            NonFlange,
            All,
            Dimension,
            Pipe,
            PipeAndKIP
        }

        private Dictionary<ShapeType, Shape> CurrentSignGreen = new Dictionary<ShapeType, Shape>();
        private Shape CurrentCircleLeft;
        private Shape CurrentCircleRight;
        private bool IsConnector;
        private bool ShouldDrawCircles;
        private List<ConnectorType> ConnectorTypes;
        private List<ShapeType> ShapeTypes = new List<ShapeType>();
        internal static InterfaceCommand Start(List<ConnectorType> connectorTypes, SignType type)
        {
            Stop();
            CurrentInstance = new SelectShape(connectorTypes, type);
            return CurrentInstance;
        }
        internal static InterfaceCommand Start(Flags flag, SignType type)
        {
            var list = new List<ConnectorType>();
            var shapes = new Dictionary<ShapeType, SignType>();
            switch (flag)
            {
                case Flags.NonFlange:
                    list.Add(ConnectorType.Socket);
                    list.Add(ConnectorType.Weld);
                    list.Add(ConnectorType.Thread);
                    break;
                case Flags.All:
                    list.Add(ConnectorType.Flange);
                    list.Add(ConnectorType.Socket);
                    list.Add(ConnectorType.Weld);
                    list.Add(ConnectorType.Thread);
                    break;
                case Flags.Dimension:
                    list.Add(ConnectorType.Flange);
                    list.Add(ConnectorType.Socket);
                    list.Add(ConnectorType.Weld);
                    list.Add(ConnectorType.Thread);
                    list.Add(ConnectorType.Dimension);
                    break;
                case Flags.Pipe:
                    shapes.Add(ShapeType.Pipe, SignType.Line);
                    CurrentInstance = new SelectShape(shapes);
                    return CurrentInstance;
                case Flags.PipeAndKIP:
                    shapes.Add(ShapeType.Pipe, SignType.Line);
                    shapes.Add(ShapeType.Sign, SignType.Cross);
                    CurrentInstance = new SelectShape(shapes);
                    return CurrentInstance;
                default:
                    break;
            }
            CurrentInstance = new SelectShape(list, type);
            return Start(list, type);
        }
        private SelectShape(List<ConnectorType> connectorTypes, SignType type)
        {
            ConnectorTypes = connectorTypes;
            IsConnector = true;
            CurrentSignGreen.Add(ShapeType.Connector, CreateSign(CurrentPage, GetMasterName(type)));
            CurrentMouseMoveEvent = MouseMove;
        }

        public SelectShape(Dictionary<ShapeType, SignType> shapes)
        {
            IsConnector = false;
            foreach (var element in shapes)
            {
                ShapeTypes.Add(element.Key);
                CurrentSignGreen.Add(element.Key, CreateSign(CurrentPage, GetMasterName(element.Value)));
                if (element.Key == ShapeType.Pipe)
                {
                    CurrentCircleLeft = CreateSign(CurrentPage, "Circle");
                    CurrentCircleRight = CreateSign(CurrentPage, "Circle");
                    ShouldDrawCircles = true;
                }
            }
            CurrentMouseMoveEvent = MouseMove;
        }

        internal override void MouseMove(double x, double y)
        {
            if (IsConnector)
            {
                MouseMoveConnector(x, y);
            }
            else
            {
                MouseMoveShape(x, y);
            }
        }
        internal void MouseMoveConnector(double x, double y)
        {
            Shape found = FindClosest(CurrentPage, x, y, ConnectorTypes);
            if (found == null)
                return;
            double X = found.GetX();
            double Y = found.GetY();
            Shape sign = null;
            if (CurrentSignGreen.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("Обнаружена ошибка: CurrentSignGreen = null в SelectShape. Перезапустите текущую команду.");
                Stop();
            }
            else
            {
                sign = CurrentSignGreen.First().Value;
            }
            if (Math.Sqrt(Math.Pow(x - X, 2) + Math.Pow(y - Y, 2)) < Properties.Settings.Default.Sensitivity * 5)
            {
                sign.SetCenter(X, Y);
                SelectedElement = found;
            }
            else
            {
                sign.SetCenter(Nah, Nah);
                SelectedElement = null;
            }
            sign = null;
        }
        internal void MouseMoveShape(double x, double y)
        {
            Shape found = FindClosest(CurrentPage, x, y, ShapeTypes);
            if (found == null)
                return;
            double X = found.GetX();
            double Y = found.GetY();
            if (ShouldDrawCircles && (CurrentCircleRight == null || CurrentCircleLeft == null))
            {
                System.Windows.Forms.MessageBox.Show("Обнаружена ошибка: CurrentCircle = null в SelectShape. Перезапустите текущую команду.");
                Stop();
            }
            Shape sign = null;
            var foundShapeType = found.GetShapeType();
            if (CurrentSignGreen.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("Обнаружена ошибка: CurrentSignGreen = null в SelectShape. Перезапустите текущую команду.");
                Stop();
            }
            else
            {
                sign = CurrentSignGreen.FirstOrDefault(z => z.Key == foundShapeType).Value;
            }

            if (Math.Sqrt(Math.Pow(x - X, 2) + Math.Pow(y - Y, 2)) < Properties.Settings.Default.Sensitivity * 5)
            {
                sign.SetCenter(X, Y);
                var aaa = found.GetAngleByAtan();
                sign.SetAngle(found.GetAngleByAtan());
                if (ShouldDrawCircles)
                {
                    CurrentCircleRight.SetCenter(found.GetBeginX(), found.GetBeginY());
                    CurrentCircleLeft.SetCenter(found.GetEndX(), found.GetEndY());
                }
                SelectedElement = found;
            }
            else
            {
                foreach (var oldSign in CurrentSignGreen)
                {
                    oldSign.Value.SetCenter(Nah, Nah);
                }
                if (ShouldDrawCircles)
                {
                    CurrentCircleRight.SetCenter(Nah, Nah);
                    CurrentCircleLeft.SetCenter(Nah, Nah);
                }
                SelectedElement = null;
            }
        }

        private string GetMasterName(SignType type)
        {
            switch (type)
            {
                case SignType.Arrow:
                    return PConst.SignArrow;
                case SignType.Cross:
                    return PConst.SignWeldSelectCursorGreen;
                case SignType.Line:
                    return PConst.SignLineSelect;
                default:
                    return PConst.SignWeldSelectCursorGreen;
            }
        }
        protected override void Dispose()
        {
            foreach (var shape in CurrentSignGreen)
            {
                shape.Value.Delete();
            }
            CurrentCircleLeft?.Delete();
            CurrentCircleRight?.Delete();
        }
    }
}

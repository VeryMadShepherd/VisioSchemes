using Matan;
using Microsoft.Office.Interop.Visio;
using Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisioSchemes.UI;

namespace VisioSchemes.Commands
{
    internal abstract class Command
    {
        protected Point Center;
        protected Page CurrentPage;
        protected bool ActionAllowed = true;
        protected delegate void Void();
        protected delegate void MouseEvent(double x, double y);
        protected static Void LastCommand;
        protected static Command CurrentInstance;
        protected static InterfaceCommand CurrentInterface;
        protected static MouseEvent CurrentMouseUpEvent;
        protected static MouseEvent CurrentMouseMoveEvent;
        protected static System.Windows.Forms.Form CurrentForm;

        protected Command()
        {
            CurrentPage = ThisAddIn.VisioApp.ActivePage;
        }
        internal static void OnStart()
        {
            if (CurrentInstance != null)
            {
                Stop();
            }
            CurrentForm?.Close();
        }
        internal static void Stop()
        {
            InterfaceCommand.Stop();
            CurrentInterface = null;
            CurrentInstance?.Dispose();
            CurrentInstance = null;
            CurrentMouseMoveEvent = null;
            CurrentMouseUpEvent = null;
            ThisAddIn.Ribbon.DisposeTip();
        }
        internal static void MouseUp(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        {
            bool allowed = CurrentInterface == null ? true : CurrentInterface.IsAllowed && CurrentInstance.ActionAllowed;
            if (Button == 1 && allowed)
                CurrentMouseUpEvent?.Invoke(x, y);
        }
        internal static void MouseMove(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        {
            if (CurrentInstance != null && CurrentInstance.ActionAllowed)
            {
                CurrentMouseMoveEvent?.Invoke(x, y);
                InterfaceCommand.CurrentMouseMoveEvent?.Invoke(x, y);
            }
        }

        /// <summary>
        /// Stops current command or repeats last
        /// </summary>
        internal static void OnSpacePressed(int KeyAscii, ref bool CancelDefault)
        {
            if (KeyAscii == 32 || KeyAscii == 13)
            {
                if (CurrentInstance != null)
                {
                    Stop();
                }
                else
                {
                    LastCommand?.Invoke();
                }
            }
        }
        protected static void ChangeForm(Form form)
        {
            CurrentForm?.Close();
            CurrentForm = form;
            CurrentForm.Show();
        }
        internal static Shape CreateConnector(double x, double y, double direction, ConnectorType type)
        {
            string imageName = type == ConnectorType.Dimension ? "ConDimension" : "ConArrow";
            Master image = ThisAddIn.VisioDoc.Masters.ItemU[imageName];
            Shape connector = ThisAddIn.VisioApp.ActivePage.Drop(image, x, y);
            connector.SetShapeType(ShapeType.Connector);
            connector.SetConnectorType(type);
            connector.SetAngle(direction);
            connector.SetLayer(ThisAddIn.VisioApp.ActivePage, "Вспомогательные элементы");
            connector.BringToFront();
            ThisAddIn.VisioApp.ActiveWindow.Selection.DeselectAll();
            return connector;
        }

        protected double GetDirection(Point center, double x, double y)
        {
            var dVector = new Vector(center, new Point(x, y));
            dVector = IsOrtho() ? dVector.RoundToIsometric() : dVector;
            return dVector.GetAngleDeg();
        }
        internal static bool IsOrtho() => ThisAddIn.Ribbon.IsOrtho.Checked;
        internal virtual void Dispose()
        {
            ThisAddIn.MainWindow.DeselectAll();
        }
        protected Shape CreateShape(string name, Point point, double angle, ShapeType shapeType = ShapeType.RedMark)
        {
            Shape image = CurrentPage.Drop(ThisAddIn.VisioDoc.Masters.ItemU[name], point.X, point.Y);
            image.SetAngle(angle);
            image.SetShapeType(shapeType);
            return image;
        }

    }
}

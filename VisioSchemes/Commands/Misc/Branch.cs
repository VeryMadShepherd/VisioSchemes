using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extension;
using Matan;
using Microsoft.Office.Interop.Visio;

namespace VisioSchemes.Commands.Misc
{
    internal class Branch
    {
        internal Shape Line;
        internal Shape Ending;
        internal Shape Connector;
        internal Vector Vector;

        internal Branch(Point center, Point end, double length)
        {
            end = Command.IsOrtho() ? end.RoundToIsometric(center) : end;
            end = end.RoundToLength(center, length);
            double centerX = center.X;
            double centerY = center.Y;
            double endX = end.X;
            double endY = end.Y;
            Vector = new Vector(center, end);
            Line = ThisAddIn.VisioApp.ActivePage.DrawLine(center.X, center.Y, end.X, end.Y);
            //Line.SetLineWeight(DocumentSettings.Drawing.MainLine);
            ThisAddIn.MainWindow.DeselectAll();
        }
        internal void CreateEnding(Page page, string masterName)
        {
            Ending = page.Drop(ThisAddIn.VisioDoc.Masters.ItemU[masterName], Vector.E.X, Vector.E.Y);
            double angle = Vector.GetAngleDeg();
            Ending.SetAngle(angle);
            ThisAddIn.MainWindow.DeselectAll();
        }
        internal void Dispose()
        {
            Line?.Delete();
            Ending?.Delete();
            Connector?.Delete();
        }
        internal void CreateConnector()
        {
            if (Connector == null)
                Connector = Command.CreateConnector
                    (Vector.E.X, Vector.E.Y, Vector.GetAngleDeg(), ConnectorType.Weld);
            Connector.BringToFront();
        }
    }
}

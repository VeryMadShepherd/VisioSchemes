using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Visio;
using Extension;

namespace VisioSchemes
{
    internal static class ProjectVisioExtension
    {
        /// <summary>
        /// Returns user-defined cell "Length" value. Shape properties table must consist this cell, otherwise it will return 0.
        /// </summary>
        internal static double GetLength(this Shape shape)
        {
            double.TryParse(shape.GetCellValue("User.Length").Replace(".", ","), out double value);
            return value;
        }

        /// <summary>
        /// Returns ShapeType enum, which stored in the "Type" user-defined cell. If there is no such cell, returns ShapeType.Error
        /// </summary>
        internal static ShapeType GetShapeType(this Shape shape)
        {
            double.TryParse(shape.GetCellValue("User.Type"), out double parsed);
            return (ShapeType)parsed;
        }

        /// <summary>
        /// Returns ConnectorType enum, which stored in the "ConnectorType" user-defined cell. If there is no such cell, returns ConnectorType.Error
        /// </summary>
        internal static ConnectorType GetConnectorType(this Shape shape)
        {
            double.TryParse(shape.GetCellValue("User.ConnectorType"), out double parsed);
            return (ConnectorType)parsed;
        }

        /// <summary>
        /// Sets ShapeType enum of the shape. Creates user-defined cell if needed. 
        /// </summary>
        internal static void SetShapeType(this Shape shape, ShapeType type) => shape.SetUserRow("Type", type.ToString("d"));

        /// <summary>
        /// Sets ConnectorType enum of the shape. Creates user-defined cell if needed. 
        /// </summary>
        internal static void SetConnectorType(this Shape shape, ConnectorType type) => shape.SetUserRow("ConnectorType", type.ToString("d"));

        /// <summary>
        /// Creates position marker and connect it to chosen shape. 
        /// </summary>
        internal static Shape MarkShape(this Shape shape, double direction, Page page, string layerName, Shape target = null)
        {
            if (target == null)
            {
                target = shape;
            }
            else
            {
                target.SetLayer(page, PConst.MiscLayer);
            }
            Shape marker = shape.CreateMarker(page, 0, direction);
            marker.SetLayer(page, layerName);
            target.ConnectTo(marker, page, layerName);
            ThisAddIn.VisioApp.ActiveWindow.Selection.DeselectAll();
            return marker;
        }

        /// <summary>
        /// Creates position marker and sets its properties. Returns created marker.
        /// </summary>
        private static Shape CreateMarker(this Shape shape, Page page, int multiplier, double direction)
        {
            double markerDistance = Properties.Settings.Default.MarkerDistance;
            double rDirection = Matan.Converting.D2R(direction);
            double size = Properties.Settings.Default.MarkerSize;
            Shape marker = ThisAddIn.VisioApp.ActivePage.DrawOval(0, 0, size, size);
            marker.SetCenter(
                shape.GetX() + markerDistance * Math.Cos(rDirection) + Properties.Settings.Default.MarkerSize * multiplier,
                shape.GetY() + markerDistance * Math.Sin(rDirection));
            marker.SetText("", VConst.ISOCPEUR, Properties.Settings.Default.TextSize.ToString() + " pt", VConst.Italic, false);
            marker.SetLineWeight(Properties.Settings.Default.DimensionLine);
            marker.Characters.AddCustomFieldU(shape.NameU.Replace("\"", string.Empty) + "!Prop.Position", 0);
            marker.SetZeroIndent();
            return marker;
        }
    }
}

using System;
using Microsoft.Office.Interop.Visio;

namespace Extension
{
    public static class VisioExtension
    {
        #region Low-level extensions

        /// <summary>
        /// Returns target cell value. To get user-defined cell it should be "User.*CellName*", to get property cell value - "Prop.*CellName*"
        /// </summary>
        public static string GetCellValue(this Shape shape, string cellName)
        {
            if (shape == null)
                return "ERROR";
            if (shape.CellExists[cellName, 0] == 0)
                return "ERROR";
            return shape.Cells[cellName + ".Value"].FormulaU.Replace("\"", string.Empty);
        }

        /// <summary>
        /// Set value of the desired property. If one does not exist, create it.
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="name">In-core cell name without "Prop" suffix.</param>
        /// <param name="value">Value of the cell.</param>
        /// <param name="prompt">Description that will be diplayed to the user in the Visio app.</param>
        public static void SetPropRow(this Shape shape, string name, string value, string prompt)
        {
            if (shape.CellExists["Prop." + name, 0] == 0)
            {
                short PropRow = shape.AddRow(243, -2, 0);
                shape.CellsSRC[243, PropRow, PropRow].RowNameU = name;
                shape.CellsSRC[243, PropRow, 0].FormulaU = "\"" + value + "\"";
                shape.CellsSRC[243, PropRow, 2].FormulaU = "\"" + prompt + "\"";
                shape.CellsSRC[243, PropRow, 5].FormulaU = "0";
                shape.CellsSRC[243, PropRow, 3].FormulaU = "";
                shape.CellsSRC[243, PropRow, 14].FormulaU = "1033";
                shape.CellsSRC[243, PropRow, 15].FormulaU = "";
                shape.CellsSRC[243, PropRow, 1].FormulaU = "";
                shape.CellsSRC[243, PropRow, 4].FormulaU = "\"" + "" + "\"";
            }
            else
            {
                shape.Cells["Prop." + name + ".Value"].FormulaU = "\"" + value + "\"";
            }
        }

        /// <summary>
        /// Set value of the user-defined row. If one does not exist, create it.
        /// </summary>
        /// <param name="name">In-core cell name without suffix "User".</param>
        /// <param name="value">Value of the cell.</param>
        public static void SetUserRow(this Shape shape, string name, string value)
        {
            if (shape.CellExists["User." + name, 0] == 0)
            {
                short PropRow = shape.AddRow(242, -2, 0);
                shape.CellsSRC[242, PropRow, 0].FormulaU = "\"" + value + "\"";
                shape.CellsSRC[242, PropRow, 1].RowNameU = name;
            }
            else
            {
                shape.Cells["User." + name + ".Value"].FormulaU = "\"" + value + "\"";
            }
        }

        #endregion

        #region SET shape parameters

        /// <summary>
        /// String format: "1 pt", with dot as decimal separator if needed;
        /// </summary>
        public static void SetLineWeight(this Shape shape, string weight) => shape.CellsSRC[1, 2, 0].FormulaU = weight;

        /// <summary>
        /// String format: integer as Visio font, "21" for example as Arial
        /// </summary>
        public static void SetFont(this Shape shape, string font) => shape.CellsSRC[3, 0, 0].FormulaU = font;

        /// <summary>
        /// String format: "6 pt", with dot as decimal separator if needed;
        /// </summary>
        public static void SetFontSize(this Shape shape, string size) => shape.CellsSRC[3, 0, 7].FormulaU = size;

        /// <summary>
        /// String format: "THEMEGUARD(RGB(0,0,0))" where 0, 0, 0 - RGB color code
        /// </summary>
        public static void SetLineColor(this Shape shape, string color) => shape.CellsSRC[1, 2, 1].FormulaU = color;

        /// <summary>
        /// String format: THEMEGUARD(RGB(0,0,0)) where 0, 0, 0 - RGB color code
        /// </summary>
        public static void SetFillColor(this Shape shape, string color) => shape.CellsSRC[1, 3, 3].FormulaU = color;

        /// <summary>
        /// Protects shape from selection. You must allow this action in Drawing Explorer in Visio app.
        /// </summary>
        public static void SetShapeLocked(this Shape shape) => shape.CellsSRC[1, 15, 15].FormulaU = "1";

        /// <summary>
        /// Sets the tilt angle of the shape in degrees. Angle should be from 0 to 360.
        /// </summary>
        public static void SetAngle(this Shape shape, double angle) => shape.CellsSRC[1, 1, 6].FormulaU = System.Math.Round(angle).ToString() + " deg";

        /// <summary>
        /// Sets the angle of the shape in radians.
        /// </summary>
        public static void SetAngleRadian(this Shape shape, double angle) => shape.CellsSRC[1, 1, 6].FormulaU =
            System.Math.Round(Matan.Converting.R2D(angle)).ToString() + " deg";

        /// <summary>
        /// Sets the line style of the shape as "dotted".
        /// </summary>
        public static void SetDotted(this Shape shape) => shape.CellsSRC[1, 2, 2].FormulaU = "2";

        /// <summary>
        /// Rounds corners of the shape to selected angle (default is 0).
        /// </summary>
        public static void SetLineRound(this Shape shape, int radius) =>
            shape.CellsSRC[1, 2, 3].FormulaU = radius.ToString() + " mm";

        /// <summary>
        /// Sets the indent of the text as 0.
        /// </summary>
        public static void SetZeroIndent(this Shape shape)
        {
            shape.CellsSRC[1, 11, 0].FormulaU = "0 pt";
            shape.CellsSRC[1, 11, 1].FormulaU = "0 pt";
        }

        /// <summary>
        /// Sets both endline styles as "arrow". 
        /// </summary>
        public static void SetLineArrow(this Shape shape)
        {
            SetLineArrowForward(shape);
            SetLineArrowBackward(shape);
        }

        /// <summary>
        /// Sets the endline style of the end of the line as "arrow".
        /// </summary>
        public static void SetLineArrowForward(this Shape shape)
        {
            shape.CellsSRC[1, 2, 6].FormulaU = "13";
        }

        /// <summary>
        /// Sets the endline style of the beginning of the line as "arrow".
        /// </summary>
        public static void SetLineArrowBackward(this Shape shape)
        {
            shape.CellsSRC[1, 2, 5].FormulaU = "13";
        }

        /// <summary>
        /// Sets the shape text. 
        /// </summary>
        /// <param name="text">Text value.</param>
        /// <param name="font">Integer as Visio font. "21" for Arial as an example.</param>
        /// <param name="size">Size should be in "6 pt" format.</param>
        /// <param name="style">Integer as Visio font style. "32" for "normal"</param>
        /// <param name="IsOnlyText">If false, leaves border around text. If true, there will be only text without border.</param>
        public static void SetText(this Shape shape, string text, string font, string size, string style = VConst.Normal, bool IsOnlyText = false)
        {
            if (IsOnlyText)
            {
                //fill
                shape.CellsSRC[1, 3, 2].FormulaU = "0";
                shape.CellsSRC[1, 26, 5].FormulaU = "FALSE";
                shape.CellsSRC[1, 26, 6].FormulaU = "FALSE";
                shape.CellsSRC[1, 26, 7].FormulaU = "FALSE";
                //line
                shape.CellsSRC[1, 2, 2].FormulaU = "0";
                shape.CellsSRC[1, 26, 4].FormulaU = "FALSE";
            }
            if (text != null)
            {
                shape.Characters.Begin = 0;
                shape.Characters.End = 0;
                shape.SetFont(font);
                shape.SetFontSize(size);
                shape.Characters.Text = text;
            }
        }

        /// <summary>
        /// Sets the shape text and set its parameters as Arial and size of 6 pt.
        /// </summary>
        /// <param name="text">Text value.</param>
        /// <param name="IsOnlyText">If false, leaves border around text. If true, there will be only text without border.</param>
        public static void SetText(this Shape shape, string text, bool IsOnlyText = false)
        {
            shape.SetText(text, VConst.Arial, VConst.SIZE, VConst.Normal, IsOnlyText);
        }

        /// <summary>
        /// Sets shape layer. Returns success.
        /// </summary>
        public static bool SetLayer(this Shape shape, Page page, string layerName)
        {
            Layer TempLayer = page.Layers[layerName];
            if (TempLayer != null)
            {
                TempLayer.Add(shape, 0);
            }
            else
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Create a connector between target shape and marker, then put it into selected layer.
        /// </summary>
        public static void ConnectTo(this Shape target, Shape marker, Page page, string layer, string weight = "0.24 pt")
        {
            Shape connector = page.DropEx("Connector", target.GetX(), target.GetY());
            connector.CellsSRC[1, 23, 19].FormulaU = "1";
            connector.CellsSRC[1, 23, 10].FormulaU = "16";
            connector.SetLineWeight(weight);
            connector.SetLayer(page, layer);
            connector.Cells["BeginX"].GlueTo(marker.Cells["PinX"]);
            connector.Cells["EndX"].GlueTo(target.Cells["PinX"]);
            connector.Cells["BeginY"].GlueTo(marker.Cells["PinY"]);
            connector.Cells["EndY"].GlueTo(target.Cells["PinY"]);
        }

        #endregion

        #region GET shape parameters

        /// <summary>
        /// Returns shape properties table cell value "PinX", position on x-axis 
        /// </summary>
        public static double GetX(this Shape shape)
        {
            try
            {
                return shape.Cells["PinX"].Result[65];
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Returns shape properties table cell value "PinX": position on y-axis 
        /// </summary>
        public static double GetY(this Shape shape)
        {
            try
            {
                return shape.Cells["PinY"].Result[65];
            }
            catch
            {
                return 0;
            }

        }

        /// <summary>
        /// Returns shape properties table cell value "BeginX": position on the x-axis of the beginning point of the line. Shape must be line-type.
        /// </summary>
        public static double GetBeginX(this Shape shape) => shape.Cells["BeginX"].Result[65];

        /// <summary>
        /// Returns shape properties table cell value "BeginX": position on the y-axis of the beginning point of the line. Shape must be line-type.
        /// </summary>
        public static double GetBeginY(this Shape shape) => shape.Cells["BeginY"].Result[65];

        /// <summary>
        /// Returns shape properties table cell value "EndX": position on the x-axis of the end point of the line. Shape must be line-type.
        /// </summary>
        public static double GetEndX(this Shape shape) => shape.Cells["EndX"].Result[65];

        /// <summary>
        /// Returns shape properties table cell value "EndX": position on the y-axis of the end point of the line. Shape must be line-type.
        /// </summary>
        public static double GetEndY(this Shape shape) => shape.Cells["EndY"].Result[65];

        /// <summary>
        /// Returns shape properties table cell value "Angle": rotation angle of the shape.
        /// </summary>
        public static double GetAngle(this Shape shape) => System.Math.Round(shape.Cells["Angle"].Result[81]);

        /// <summary>
        /// Returns calculated angle by the line points. Shape must be line-type.
        /// </summary>
        public static double GetAngleByAtan(this Shape shape)
        {
            double beginX = shape.GetBeginX();
            double beginY = shape.GetBeginY();
            double endX = shape.GetEndX();
            double endY = shape.GetEndY();
            double angle = Math.Atan2(endY - beginY, endX - beginX);
            return Math.Round(Matan.Converting.R2D(angle));
        }

        /// <summary>
        /// Create and place on the page shape from the document's stencil. Returns created shape.
        /// </summary>
        public static Shape DropEx(this Page page, string name, double x = 0, double y = 0)
        {
            return page.Drop(page.Document.Masters.ItemU[name], x, y);
        }

        #endregion

        public static void CreateLayer(Page vPage, string name, string LayerColor = null)
        {
            Layer CreatingLayer = vPage.Layers.Add(name);
            CreatingLayer.NameU = name;
            LayerColor = LayerColor ?? "255";
            CreatingLayer.CellsC[2].FormulaU = LayerColor; //цвет
            CreatingLayer.CellsC[3].FormulaU = "0"; //статус
            CreatingLayer.CellsC[4].FormulaU = "1"; //видимость
            CreatingLayer.CellsC[5].FormulaU = "1"; //печатается ли
            CreatingLayer.CellsC[6].FormulaU = "0"; //активен ли
            CreatingLayer.CellsC[7].FormulaU = "0"; //заблокирован ли
            CreatingLayer.CellsC[8].FormulaU = "1"; //snap - привязка?
            CreatingLayer.CellsC[9].FormulaU = "1"; //соединяется ли (glue)
            CreatingLayer.CellsC[11].FormulaU = "0%"; //прозрачность
        }
    }
}

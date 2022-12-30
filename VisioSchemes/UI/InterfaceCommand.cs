using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extension;
using Matan;
using Microsoft.Office.Interop.Visio;

namespace VisioSchemes.UI
{
    public abstract class InterfaceCommand
    {
        internal Shape SelectedElement;
        internal Point Center;
        internal static MouseEvent CurrentMouseMoveEvent;
        internal delegate void MouseEvent(double x, double y);
        internal bool IsAllowed = true;
        protected const double Nah = -10;
        protected Page CurrentPage;
        protected static InterfaceCommand CurrentInstance;
        protected InterfaceCommand()
        {
            CurrentPage = ThisAddIn.VisioApp.ActivePage;
            Clear(CurrentPage);
        }
        internal static void Stop()
        {
            CurrentMouseMoveEvent = null;
            if (CurrentInstance != null)
            {
                CurrentInstance.Dispose();
                Clear(CurrentInstance.CurrentPage);
            }
            CurrentInstance = null;
        }
        internal abstract void MouseMove(double x, double y);

        #region FindClosest

        /// <summary>
        /// Find connector closest to point (x, y). Shape must have cell "User.Type" with one of the listed values
        /// </summary>
        internal static Shape FindClosest(Page page, double x, double y, List<ConnectorType> types)
        {
            var shapesPool = new List<Shape>();
            foreach (Shape shape in page.Shapes)
            {
                foreach (var type in types)
                {
                    if (shape.GetConnectorType() == type)
                    {
                        shapesPool.Add(shape);
                        break;
                    }
                }
            }
            if (shapesPool.Count == 0)
            {
                return null;
            }
            return FindClosest(x, y, shapesPool);
        }

        /// <summary>
        /// Find shape of the selected type closest to point (x, y). Shape must have cell "User.Type" with selected type
        /// </summary>
        internal static Shape FindClosest(Page page, double x, double y, ShapeType type)
        {
            var shapesPool = new List<Shape>();
            foreach (Shape shape in page.Shapes)
            {
                if (shape.GetShapeType() == type)
                {
                    shapesPool.Add(shape);
                }
            }
            if (shapesPool.Count == 0)
            {
                return null;
            }
            return FindClosest(x, y, shapesPool);
        }

        /// <summary>
        /// Find shape of the selected types closest to point (x, y). Shape must have cell "User.Type" with one of the selected types
        /// </summary>
        internal static Shape FindClosest(Page page, double x, double y, List<ShapeType> types)
        {
            var shapesPool = new List<Shape>();
            foreach (Shape shape in page.Shapes)
            {
                foreach (var type in types)
                {
                    if (shape.GetShapeType() == type)
                    {
                        shapesPool.Add(shape);
                    }
                }
            }
            if (shapesPool.Count == 0)
            {
                return null;
            }
            return FindClosest(x, y, shapesPool);
        }

        /// <summary>
        /// Subfunction to other FindClosest to increase performance
        /// </summary>
        private static Shape FindClosest(double x, double y, List<Shape> shapesPool)
        {
            double minimalDistance = GetCurrentDistance(shapesPool[0]);
            Shape element = shapesPool[0];
            foreach (Shape shape in shapesPool)
            {
                double currentDistance = GetCurrentDistance(shape);
                if (currentDistance < minimalDistance)
                {
                    minimalDistance = currentDistance;
                    element = shape;
                }
            }
            return element;

            double GetCurrentDistance(Shape shape) => Math.Sqrt(Math.Pow(x - shape.GetX(), 2) + Math.Pow(y - shape.GetY(), 2));
        }
        #endregion

        /// <summary>
        /// Creates UI shape. Allow "close" action in Drawing explorer to prevent shape from accidentaly selection by user
        /// </summary>
        protected static Shape CreateSign(Page page, string masterName)
        {
            Shape result = page.Drop(ThisAddIn.VisioDoc.Masters.ItemU[masterName], 0, 0);
            result.SetShapeType(ShapeType.RedMark);
            result.SetShapeLocked();
            result.SetCenter(Nah, Nah);
            return result;
        }
        protected abstract void Dispose();

        /// <summary>
        /// Delete UI shapes, if any present (support de-bug function)
        /// </summary>
        internal static void Clear(Page page)
        {
            foreach (Shape shape in page.Shapes)
            {
                if (shape.GetShapeType() == ShapeType.RedMark)
                {
                    shape.Delete();
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Visio = Microsoft.Office.Interop.Visio;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Interop.Visio;
using System.Windows.Forms;
using Extension;
using VisioSchemes.Commands;

namespace VisioSchemes
{
    public partial class ThisAddIn
    {
        public static ToolsRibbon Ribbon;
        public static Visio.Application VisioApp;
        public static Window MainWindow;
        public static Document VisioDoc;
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            //ThisInstance = this;
            VisioApp = (Visio.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Visio.Application");
            if (VisioApp.ActiveDocument == null)
            {
                VisioApp.DocumentOpened += PrepareAddIn;
            }
            else
            {
                VisioDoc = VisioApp.ActiveDocument;
            }
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        private void PrepareAddIn(Document Doc)
        {
            if (IsDocumentValid(Doc))
            {
                VisioDoc = Doc;
                PrepareDocument(VisioDoc);
            }
        }
        private static void PrepareDocument(Document Doc)
        {
            MainWindow = VisioApp.ActiveWindow;
            Doc.PageAdded += CreateLayers;
            MainWindow.KeyPress += Command.OnSpacePressed;
            MainWindow.MouseUp += Command.MouseUp;
            MainWindow.MouseMove += Command.MouseMove;
            //ProjectSettings.ImageSource.FillImageSource();
            Ribbon.EnableButtons();
        }
        private static bool IsDocumentValid(Document Doc)
        {
            var existingLayers = new List<string>();
            foreach(Layer layer in VisioApp.ActivePage.Layers)
            {
                existingLayers.Add(layer.Name);
            }
            var neededLayers = new List<string>()
            {
                PConst.PipesLayer,
                PConst.DimensionsLayer,
                PConst.PipesPosLayer,
                PConst.ElementsPosLayer,
                PConst.ConventionsLayer,
                PConst.AuxiliaryElementsLayer,
                PConst.RedLayer
            };
            foreach (var layer in neededLayers)
            {
                if (existingLayers.Contains(layer) == false)
                {
                    return false;
                }
            }
            return true;
        }

        private static void CreateLayers(Page vPage)
        {
            vPage.Name = vPage.Name.Replace("Page-", string.Empty);
            VisioExtension.CreateLayer(vPage, PConst.PipesLayer);
            VisioExtension.CreateLayer(vPage, PConst.DimensionsLayer);
            VisioExtension.CreateLayer(vPage, PConst.PipesPosLayer);
            VisioExtension.CreateLayer(vPage, PConst.ElementsPosLayer);
            VisioExtension.CreateLayer(vPage, PConst.ConventionsLayer);
            VisioExtension.CreateLayer(vPage, PConst.AuxiliaryElementsLayer);
            VisioExtension.CreateLayer(vPage, PConst.RedLayer);
            //vPage.Drop(VisioDoc.Masters.ItemU[PConst.TitleBlock], 0, 0);
        }

        /// <summary>
        /// Creates needed layer if needed
        /// </summary>
        internal static void Activate()
        {
            VisioDoc = VisioApp.ActiveDocument;
            CreateLayers(VisioApp.ActivePage);
            PrepareDocument(VisioDoc);
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}

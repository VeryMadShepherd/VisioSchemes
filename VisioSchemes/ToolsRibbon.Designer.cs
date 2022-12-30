
using System;
using Microsoft.Office.Tools.Ribbon;

namespace VisioSchemes
{
    partial class ToolsRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public ToolsRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
            ThisAddIn.Ribbon = this;
        }

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.VisioSchemes = this.Factory.CreateRibbonTab();
            this.PassportData = this.Factory.CreateRibbonGroup();
            this.separator1 = this.Factory.CreateRibbonSeparator();
            this.Draw = this.Factory.CreateRibbonGroup();
            this.Legend = this.Factory.CreateRibbonGroup();
            this.box4 = this.Factory.CreateRibbonBox();
            this.Modify = this.Factory.CreateRibbonGroup();
            this.box2 = this.Factory.CreateRibbonBox();
            this.separator2 = this.Factory.CreateRibbonSeparator();
            this.box1 = this.Factory.CreateRibbonBox();
            this.Workflow = this.Factory.CreateRibbonGroup();
            this.CurrentCommandValue = this.Factory.CreateRibbonLabel();
            this.Other = this.Factory.CreateRibbonGroup();
            this.ActivateFormular = this.Factory.CreateRibbonButton();
            this.ExportToWord = this.Factory.CreateRibbonButton();
            this.Merge = this.Factory.CreateRibbonButton();
            this.LinesList = this.Factory.CreateRibbonButton();
            this.PipesList = this.Factory.CreateRibbonButton();
            this.FittingsList = this.Factory.CreateRibbonButton();
            this.DrawPipeline = this.Factory.CreateRibbonButton();
            this.Reducer = this.Factory.CreateRibbonButton();
            this.Tee = this.Factory.CreateRibbonButton();
            this.Valve = this.Factory.CreateRibbonButton();
            this.Flange = this.Factory.CreateRibbonButton();
            this.Dimension = this.Factory.CreateRibbonButton();
            this.Link = this.Factory.CreateRibbonButton();
            this.MarkElement = this.Factory.CreateRibbonButton();
            this.InsertElement = this.Factory.CreateRibbonButton();
            this.InsertOlet = this.Factory.CreateRibbonButton();
            this.InsertSeam = this.Factory.CreateRibbonButton();
            this.ChangeFigure = this.Factory.CreateRibbonButton();
            this.FindElement = this.Factory.CreateRibbonButton();
            this.IsOrtho = this.Factory.CreateRibbonToggleButton();
            this.CurrentDiameterValue = this.Factory.CreateRibbonButton();
            this.WeldNumber = this.Factory.CreateRibbonButton();
            this.ChangeSettings = this.Factory.CreateRibbonButton();
            this.Help = this.Factory.CreateRibbonButton();
            this.About = this.Factory.CreateRibbonButton();
            this.btnPreferences = this.Factory.CreateRibbonButton();
            this.VisioSchemes.SuspendLayout();
            this.PassportData.SuspendLayout();
            this.Draw.SuspendLayout();
            this.Legend.SuspendLayout();
            this.box4.SuspendLayout();
            this.Modify.SuspendLayout();
            this.box2.SuspendLayout();
            this.box1.SuspendLayout();
            this.Workflow.SuspendLayout();
            this.Other.SuspendLayout();
            this.SuspendLayout();
            // 
            // VisioSchemes
            // 
            this.VisioSchemes.Groups.Add(this.PassportData);
            this.VisioSchemes.Groups.Add(this.Draw);
            this.VisioSchemes.Groups.Add(this.Legend);
            this.VisioSchemes.Groups.Add(this.Modify);
            this.VisioSchemes.Groups.Add(this.Workflow);
            this.VisioSchemes.Groups.Add(this.Other);
            this.VisioSchemes.Label = "VisioSchemes";
            this.VisioSchemes.Name = "VisioSchemes";
            // 
            // PassportData
            // 
            this.PassportData.Items.Add(this.ActivateFormular);
            this.PassportData.Items.Add(this.ExportToWord);
            this.PassportData.Items.Add(this.Merge);
            this.PassportData.Items.Add(this.separator1);
            this.PassportData.Items.Add(this.LinesList);
            this.PassportData.Items.Add(this.PipesList);
            this.PassportData.Items.Add(this.FittingsList);
            this.PassportData.Label = "Database";
            this.PassportData.Name = "PassportData";
            // 
            // separator1
            // 
            this.separator1.Name = "separator1";
            // 
            // Draw
            // 
            this.Draw.Items.Add(this.DrawPipeline);
            this.Draw.Items.Add(this.Reducer);
            this.Draw.Items.Add(this.Tee);
            this.Draw.Items.Add(this.Valve);
            this.Draw.Items.Add(this.Flange);
            this.Draw.Label = "Draw";
            this.Draw.Name = "Draw";
            // 
            // Legend
            // 
            this.Legend.Items.Add(this.box4);
            this.Legend.Label = "Conventions";
            this.Legend.Name = "Legend";
            // 
            // box4
            // 
            this.box4.BoxStyle = Microsoft.Office.Tools.Ribbon.RibbonBoxStyle.Vertical;
            this.box4.Items.Add(this.Dimension);
            this.box4.Items.Add(this.Link);
            this.box4.Items.Add(this.MarkElement);
            this.box4.Name = "box4";
            // 
            // Modify
            // 
            this.Modify.Items.Add(this.box2);
            this.Modify.Items.Add(this.separator2);
            this.Modify.Items.Add(this.box1);
            this.Modify.Label = "Modify";
            this.Modify.Name = "Modify";
            // 
            // box2
            // 
            this.box2.BoxStyle = Microsoft.Office.Tools.Ribbon.RibbonBoxStyle.Vertical;
            this.box2.Items.Add(this.InsertElement);
            this.box2.Items.Add(this.InsertOlet);
            this.box2.Items.Add(this.InsertSeam);
            this.box2.Name = "box2";
            // 
            // separator2
            // 
            this.separator2.Name = "separator2";
            // 
            // box1
            // 
            this.box1.BoxStyle = Microsoft.Office.Tools.Ribbon.RibbonBoxStyle.Vertical;
            this.box1.Items.Add(this.ChangeFigure);
            this.box1.Items.Add(this.FindElement);
            this.box1.Items.Add(this.IsOrtho);
            this.box1.Name = "box1";
            // 
            // Workflow
            // 
            this.Workflow.Items.Add(this.CurrentDiameterValue);
            this.Workflow.Items.Add(this.WeldNumber);
            this.Workflow.Items.Add(this.CurrentCommandValue);
            this.Workflow.KeyTip = "D";
            this.Workflow.Label = "Status";
            this.Workflow.Name = "Workflow";
            // 
            // CurrentCommandValue
            // 
            this.CurrentCommandValue.Label = "No command";
            this.CurrentCommandValue.Name = "CurrentCommandValue";
            // 
            // Other
            // 
            this.Other.Items.Add(this.ChangeSettings);
            this.Other.Items.Add(this.Help);
            this.Other.Items.Add(this.About);
            this.Other.Items.Add(this.btnPreferences);
            this.Other.Label = "Other";
            this.Other.Name = "Other";
            // 
            // ActivateFormular
            // 
            this.ActivateFormular.KeyTip = "Y";
            this.ActivateFormular.Label = "Activate";
            this.ActivateFormular.Name = "ActivateFormular";
            this.ActivateFormular.OfficeImageId = "AcceptInvitation";
            this.ActivateFormular.ShowImage = true;
            this.ActivateFormular.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.Activate_Click);
            // 
            // ExportToWord
            // 
            this.ExportToWord.Enabled = false;
            this.ExportToWord.Label = "Export to Word";
            this.ExportToWord.Name = "ExportToWord";
            this.ExportToWord.OfficeImageId = "MergeToWord";
            this.ExportToWord.ShowImage = true;
            this.ExportToWord.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ExportToWord_Click);
            // 
            // Merge
            // 
            this.Merge.Enabled = false;
            this.Merge.ImageName = "XmlImport";
            this.Merge.Label = "Passport info";
            this.Merge.Name = "Merge";
            this.Merge.ShowImage = true;
            this.Merge.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.Merge_Click);
            // 
            // LinesList
            // 
            this.LinesList.Enabled = false;
            this.LinesList.Label = "Lines";
            this.LinesList.Name = "LinesList";
            this.LinesList.OfficeImageId = "ChangeCase";
            this.LinesList.ShowImage = true;
            this.LinesList.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.LinesList_Click);
            // 
            // PipesList
            // 
            this.PipesList.Enabled = false;
            this.PipesList.Label = "Pipes";
            this.PipesList.Name = "PipesList";
            this.PipesList.OfficeImageId = "PivotAutoCalcMenu";
            this.PipesList.ShowImage = true;
            this.PipesList.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.PipesList_Click);
            // 
            // FittingsList
            // 
            this.FittingsList.Enabled = false;
            this.FittingsList.Label = "Fittings";
            this.FittingsList.Name = "FittingsList";
            this.FittingsList.OfficeImageId = "SymbolInsertGallery";
            this.FittingsList.ShowImage = true;
            this.FittingsList.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.FittingsList_Click);
            // 
            // DrawPipeline
            // 
            this.DrawPipeline.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.DrawPipeline.Enabled = false;
            this.DrawPipeline.Image = global::VisioSchemes.Properties.Resources.Pipe;
            this.DrawPipeline.KeyTip = "Q";
            this.DrawPipeline.Label = "Pipe";
            this.DrawPipeline.Name = "DrawPipeline";
            this.DrawPipeline.ShowImage = true;
            this.DrawPipeline.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.StartDrawPipeLine);
            // 
            // Reducer
            // 
            this.Reducer.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.Reducer.Enabled = false;
            this.Reducer.Image = global::VisioSchemes.Properties.Resources.Reducer;
            this.Reducer.KeyTip = "W";
            this.Reducer.Label = "Reducer";
            this.Reducer.Name = "Reducer";
            this.Reducer.ShowImage = true;
            this.Reducer.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.StartDrawReducer);
            // 
            // Tee
            // 
            this.Tee.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.Tee.Enabled = false;
            this.Tee.Image = global::VisioSchemes.Properties.Resources.Tee;
            this.Tee.KeyTip = "E";
            this.Tee.Label = "Tee";
            this.Tee.Name = "Tee";
            this.Tee.ShowImage = true;
            this.Tee.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.StartDrawTee);
            // 
            // Valve
            // 
            this.Valve.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.Valve.Enabled = false;
            this.Valve.Image = global::VisioSchemes.Properties.Resources.Valve;
            this.Valve.KeyTip = "R";
            this.Valve.Label = "Valve";
            this.Valve.Name = "Valve";
            this.Valve.ShowImage = true;
            // 
            // Flange
            // 
            this.Flange.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.Flange.Enabled = false;
            this.Flange.Image = global::VisioSchemes.Properties.Resources.FlangeRibbon;
            this.Flange.Label = "Flange";
            this.Flange.Name = "Flange";
            this.Flange.ShowImage = true;
            this.Flange.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.StartDrawFlange);
            // 
            // Dimension
            // 
            this.Dimension.Enabled = false;
            this.Dimension.KeyTip = "A";
            this.Dimension.Label = "Dimension";
            this.Dimension.Name = "Dimension";
            this.Dimension.OfficeImageId = "ColumnWidth";
            this.Dimension.ShowImage = true;
            // 
            // Link
            // 
            this.Link.Enabled = false;
            this.Link.Label = "Link";
            this.Link.Name = "Link";
            this.Link.OfficeImageId = "BookmarkInsert";
            this.Link.ShowImage = true;
            // 
            // MarkElement
            // 
            this.MarkElement.Enabled = false;
            this.MarkElement.Label = "Mark";
            this.MarkElement.Name = "MarkElement";
            this.MarkElement.OfficeImageId = "ViewFormulaBar";
            this.MarkElement.ShowImage = true;
            // 
            // InsertElement
            // 
            this.InsertElement.Enabled = false;
            this.InsertElement.Label = "Insert element";
            this.InsertElement.Name = "InsertElement";
            this.InsertElement.OfficeImageId = "TraceDependents";
            this.InsertElement.ShowImage = true;
            // 
            // InsertOlet
            // 
            this.InsertOlet.Enabled = false;
            this.InsertOlet.KeyTip = "F";
            this.InsertOlet.Label = "Insert olet";
            this.InsertOlet.Name = "InsertOlet";
            this.InsertOlet.OfficeImageId = "ControlActiveX";
            this.InsertOlet.ShowImage = true;
            // 
            // InsertSeam
            // 
            this.InsertSeam.Enabled = false;
            this.InsertSeam.Label = "Insert tie-in";
            this.InsertSeam.Name = "InsertSeam";
            this.InsertSeam.OfficeImageId = "DataGraphicIconSet";
            this.InsertSeam.ShowImage = true;
            // 
            // ChangeFigure
            // 
            this.ChangeFigure.Enabled = false;
            this.ChangeFigure.Label = "Shape...";
            this.ChangeFigure.Name = "ChangeFigure";
            this.ChangeFigure.OfficeImageId = "AccessRefreshAllLists";
            this.ChangeFigure.ShowImage = true;
            // 
            // FindElement
            // 
            this.FindElement.Enabled = false;
            this.FindElement.Label = "Find elements";
            this.FindElement.Name = "FindElement";
            this.FindElement.OfficeImageId = "ZoomPrintPreviewExcel";
            this.FindElement.ShowImage = true;
            // 
            // IsOrtho
            // 
            this.IsOrtho.Checked = true;
            this.IsOrtho.Enabled = false;
            this.IsOrtho.Label = "Ortho";
            this.IsOrtho.Name = "IsOrtho";
            this.IsOrtho.OfficeImageId = "EquationInsertGallery";
            this.IsOrtho.ShowImage = true;
            // 
            // CurrentDiameterValue
            // 
            this.CurrentDiameterValue.Enabled = false;
            this.CurrentDiameterValue.Label = "Current diameter: 0000";
            this.CurrentDiameterValue.Name = "CurrentDiameterValue";
            this.CurrentDiameterValue.OfficeImageId = "D";
            this.CurrentDiameterValue.ShowImage = true;
            this.CurrentDiameterValue.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.DiametersChoose_Click);
            // 
            // WeldNumber
            // 
            this.WeldNumber.Enabled = false;
            this.WeldNumber.KeyTip = "S";
            this.WeldNumber.Label = "Current seam: 1";
            this.WeldNumber.Name = "WeldNumber";
            this.WeldNumber.OfficeImageId = "S";
            this.WeldNumber.ShowImage = true;
            // 
            // ChangeSettings
            // 
            this.ChangeSettings.Enabled = false;
            this.ChangeSettings.Image = global::VisioSchemes.Properties.Resources.global_settings;
            this.ChangeSettings.Label = "Preferences";
            this.ChangeSettings.Name = "ChangeSettings";
            this.ChangeSettings.ShowImage = true;
            this.ChangeSettings.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ChangeSettings_Click);
            // 
            // Help
            // 
            this.Help.Enabled = false;
            this.Help.Label = "Help";
            this.Help.Name = "Help";
            this.Help.OfficeImageId = "Help";
            this.Help.ShowImage = true;
            this.Help.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.Help_Click);
            // 
            // About
            // 
            this.About.Label = "About";
            this.About.Name = "About";
            this.About.OfficeImageId = "RefreshStatus";
            this.About.ShowImage = true;
            this.About.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.About_Click);
            // 
            // btnPreferences
            // 
            this.btnPreferences.Label = "Settings";
            this.btnPreferences.Name = "btnPreferences";
            this.btnPreferences.OfficeImageId = "RefreshStatus";
            this.btnPreferences.ShowImage = true;
            this.btnPreferences.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnPreferences_Click);
            // 
            // ToolsRibbon
            // 
            this.Name = "ToolsRibbon";
            this.RibbonType = "Microsoft.Visio.Drawing";
            this.Tabs.Add(this.VisioSchemes);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.ToolsRibbon_Load);
            this.VisioSchemes.ResumeLayout(false);
            this.VisioSchemes.PerformLayout();
            this.PassportData.ResumeLayout(false);
            this.PassportData.PerformLayout();
            this.Draw.ResumeLayout(false);
            this.Draw.PerformLayout();
            this.Legend.ResumeLayout(false);
            this.Legend.PerformLayout();
            this.box4.ResumeLayout(false);
            this.box4.PerformLayout();
            this.Modify.ResumeLayout(false);
            this.Modify.PerformLayout();
            this.box2.ResumeLayout(false);
            this.box2.PerformLayout();
            this.box1.ResumeLayout(false);
            this.box1.PerformLayout();
            this.Workflow.ResumeLayout(false);
            this.Workflow.PerformLayout();
            this.Other.ResumeLayout(false);
            this.Other.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab VisioSchemes;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup PassportData;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton ActivateFormular;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Legend;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton DrawPipeline;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton Help;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton Tee;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton Dimension;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Draw;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton Flange;
        internal Microsoft.Office.Tools.Ribbon.RibbonLabel CurrentCommandValue;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Workflow;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton IsOrtho;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Modify;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton InsertElement;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton InsertOlet;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton ChangeFigure;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton InsertSeam;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box4;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton Link;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton WeldNumber;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Other;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton CurrentDiameterValue;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton Valve;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton About;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton MarkElement;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton FindElement;
        internal RibbonButton ChangeSettings;
        internal RibbonButton ExportToWord;
        internal RibbonSeparator separator1;
        internal RibbonButton LinesList;
        internal RibbonButton PipesList;
        internal RibbonButton FittingsList;
        internal RibbonSeparator separator2;
        internal RibbonButton Merge;
        internal RibbonButton btnPreferences;
        internal RibbonButton Reducer;
    }

    partial class ThisRibbonCollection
    {
        internal ToolsRibbon ToolsRibbon
        {
            get { return this.GetRibbon<ToolsRibbon>(); }
        }
    }
}

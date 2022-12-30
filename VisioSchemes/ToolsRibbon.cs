using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Tools.Ribbon;
using VisioSchemes.Commands.DrawingCommands;

namespace VisioSchemes
{
    public partial class ToolsRibbon
    {
        private static Form CurrentForm;
        public void SetTip(string name) => CurrentCommandValue.Label = "Команда: " + name;
        public void DisposeTip() => CurrentCommandValue.Label = "Нет активной команды";
        public void EnableButtons()
        {
            ActivateFormular.Enabled = false;
            DrawPipeline.Enabled = true;
            Flange.Enabled = true;
            Reducer.Enabled = true;
            Tee.Enabled = true;
            //Dimension.Enabled = true;
            //IsOrtho.Enabled = true;
            //IsOrtho.Checked = true;
            //InsertElement.Enabled = true;
            //InsertOlet.Enabled = true;
            //ChangeFigure.Enabled = true;
            //InsertSeam.Enabled = true;
            //WeldNumber.Enabled = true;
            //CurrentDiameterValue.Enabled = true;
            //Valve.Enabled = true;
            //MarkElement.Enabled = true;
            //FindElement.Enabled = true;
            //LinesList.Enabled = true;
            //PipesList.Enabled = true;
            //FittingsList.Enabled = true;
            //Link.Enabled = true;
            //Merge.Enabled = true;
            //DrawPipeline.Click += new RibbonControlEventHandler(StartDrawPipeLine);
            //Flange.Click += new RibbonControlEventHandler(this.StartDrawFlange_Click);
            //MarkElement.Click += new RibbonControlEventHandler(MarkElement_Click);
        }
        public void EnableQuickModeButtons()
        {
            ActivateFormular.Enabled = false;
            DrawPipeline.Enabled = true;
            Flange.Enabled = true;
            Reducer.Enabled = true;
            Tee.Enabled = true;
            Dimension.Enabled = true;
            IsOrtho.Enabled = true;
            IsOrtho.Checked = true;
            InsertElement.Enabled = true;
            InsertOlet.Enabled = true;
            ChangeFigure.Enabled = true;
            InsertSeam.Enabled = true;
            WeldNumber.Enabled = true;
            CurrentDiameterValue.Enabled = true;
            Valve.Enabled = true;
            MarkElement.Enabled = true;
            FindElement.Enabled = true;
            //DrawPipeline.Click += new RibbonControlEventHandler(StartDrawQuickPipeLine);
            //Flange.Click += new RibbonControlEventHandler(this.StartDrawQuickFlange_Click);
            //MarkElement.Click += new RibbonControlEventHandler(ComplexMarkElement_Click);
        }
        #region TEST
        private void Test_Click(object sender, RibbonControlEventArgs e)
        {

        }
        private void Test_Addon()
        {

        }
        #endregion
        private void Activate_Click(object sender, RibbonControlEventArgs e) => ThisAddIn.Activate();
        private void StartDrawPipeLine(object sender, RibbonControlEventArgs e) => DrawPipe.Start();
        //private void StartDrawQuickPipeLine(object sender, RibbonControlEventArgs e) => DrawQuickPipe.Start();
        private void StartDrawReducer(object sender, RibbonControlEventArgs e) => DrawReducer.Start();
        private void StartDrawTee(object sender, RibbonControlEventArgs e) => DrawTee.Start();
        //private void StartDrawValve(object sender, RibbonControlEventArgs e) => DrawValve.Start();
        //private void StartDrawDimension(object sender, RibbonControlEventArgs e) => DrawDimension.Start();
        private void StartDrawFlange(object sender, RibbonControlEventArgs e) => DrawFlange.Start();
        //private void StartDrawQuickFlange(object sender, RibbonControlEventArgs e) => DrawQuickFlange.Start();
        //private void CountWeldSeam(object sender, RibbonControlEventArgs e) => MarkSeam.Start();
        //private void DiametersChoose(object sender, RibbonControlEventArgs e) => NewForm(new ChooseDiameter(StatusControl.SetDiameter));
        //private void MarkElement_Click(object sender, RibbonControlEventArgs e) => PassportCAD.MarkElement.Start();
        //private void ComplexMarkElement_Click(object sender, RibbonControlEventArgs e) => PassportCAD.ComplexMarkElement.Start();
        //private void ChangeFigure_Click(object sender, RibbonControlEventArgs e) => NewForm(new ChangeFigureDialog());
        //private void InsertSeam_Click(object sender, RibbonControlEventArgs e) => DrawSeam.Start();
        //private void FindElement_Click(object sender, RibbonControlEventArgs e) => NewForm(new PassportCAD.FindElement());
        //private void Insert_Click(object sender, RibbonControlEventArgs e) => NewForm(new ChooseInsertCommand());
        //private void InsertOlet_Click(object sender, RibbonControlEventArgs e) => DrawOlet.Start();
        //private void Link_Click(object sender, RibbonControlEventArgs e) => LinkToPage.Start();
        private void LinesList_Click(object sender, RibbonControlEventArgs e)
        {
            //DrawingControl.CollectPipeLineDataFromDrawing();
            //LineList.Start();
        }
        private void PipesList_Click(object sender, RibbonControlEventArgs e)
        {
            //DrawingControl.CollectElementDataFromDrawing();
            //PositionList.Start(PositionList.Flag.Pipes);
        }
        private void FittingsList_Click(object sender, RibbonControlEventArgs e)
        {
            //DrawingControl.CollectElementDataFromDrawing();
            //PositionList.Start(PositionList.Flag.Fittings);
        }
        private void About_Click(object sender, RibbonControlEventArgs e)
        {
            //AboutBox MyForm = new AboutBox();
            //MyForm.Show();
        }
        private void Help_Click(object sender, RibbonControlEventArgs e)
        {
            //var button = new Button();
            //System.Windows.Forms.Help.ShowHelp(
            //    button, 
            //    System.IO.Path.Combine(
            //        AppDomain.CurrentDomain.BaseDirectory, @"PassportCadHelp.chm"), 
            //        HelpNavigator.Topic,
            //        "Main.htm"
            //        );
        }
        private void ChangeSettings_Click(object sender, RibbonControlEventArgs e)
        {
            //CurrentForm?.Close();
            //if (ThisAddIn.VisioDoc == null)
            //{
            //    MessageBox.Show("Сначала откройте файл шаблона или паспорта!");
            //    return;
            //}
            //if (StatusControl.IsTemplate)
            //{
            //    CurrentForm = new SettingsForm(ThisAddIn.SaveTemplate, true);
            //}
            //else
            //{
            //    CurrentForm = new SettingsForm(ThisAddIn.ChangeSettings, false);
            //}
            //CurrentForm.Show();
        }
        private void NewForm(Form form)
        {
            CurrentForm?.Close();
            CurrentForm = form;
            CurrentForm.Show();
        }
        private void Merge_Click(object sender, RibbonControlEventArgs e)
        {
            //MergeFiles.CollectData();
            //NewForm(new Forms.PassportData());
        }
        private void Line_Click(object sender, RibbonControlEventArgs e)
        {
            //DrawMainLine.Start();
        }
        private void Sign_Click(object sender, RibbonControlEventArgs e)
        {
            //DrawCircleSign.Start();
        }
        private void button2_Click(object sender, RibbonControlEventArgs e)
        {
            //CreateTree.Create();
        }
        private void button3_Click(object sender, RibbonControlEventArgs e)
        {
            //ThisAddIn.Temporary();
        }

        private void TestButton_Click(object sender, RibbonControlEventArgs e)
        {

        }
        private void ExportToWord_Click(object sender, RibbonControlEventArgs e)
        {
            //PassportCAD.ExportToWord.Export(ThisAddIn.CurrentPassport);
        }

        private void btnPreferences_Click(object sender, RibbonControlEventArgs e)
        {
            //new Forms.Preferences().ShowDialog();
        }
        private void ToolsRibbon_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void DiametersChoose_Click(object sender, RibbonControlEventArgs e)
        {

        }
    }
}

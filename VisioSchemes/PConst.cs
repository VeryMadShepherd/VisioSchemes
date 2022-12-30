using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisioSchemes
{
    //Project consts
    internal static class PConst
    {
        #region Layers names
        internal const string MiscLayer = "Miscellaneous";
        internal const string PipesLayer = "Pipes";
        internal const string DimensionsLayer = "Dimensions";
        internal const string PipesPosLayer = "PipesPositions";
        internal const string ElementsPosLayer = "ElementsPositions";
        internal const string ConventionsLayer = "Conventions";
        internal const string AuxiliaryElementsLayer = "AuxiliaryElements";
        internal const string RedLayer = "RedLayer";
        #endregion

        #region Master shape names
        internal const string TitleBlock = "MainDraw";
        internal const string PointSelect = "PointSelect";
        internal const string PointSelectPlace = "PointSelectPlace";
        internal const string PointSelectForbidden = "PointSelectForbidden";
        internal const string SignArrow = "Arrow";
        internal const string SignWeldSelectCursorGreen = "WeldSelectCursorGreen";
        internal const string SignLineSelect = "LineSelect";
        internal const string Weld = "Weld";
        internal const string FlangeWN = "FlangeWeldingNeck";
        internal const string ReducerConcentric = "ReducerCon";

        internal const string Diagonal = "Diagonal";
        internal const string Vertical = "Vectical";
        #endregion

        #region UI
        internal const double MinimumPipeGlueLength = 0.3;

        #endregion

        #region Messages
        internal const string PipeIsTooShort = "Pipe is too short for break";
        internal const string UI_SignIsNull = "Обнаружена ошибка: CurrentSign = null в SelectPointOnPipe. Перезапустите текущую команду.";

        #endregion

        #region Prompt
        internal const string PipePrompt = "Draw pipe";
        internal const string ElbowPrompt = "Draw elbow";
        internal const string FlangePrompt = "Draw flange";
        internal const string TeePrompt = "Draw tee";
        internal const string ReducerPrompt = "Draw reducer";
        #endregion
    }
}

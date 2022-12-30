using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extension
{
    //Visio default string consts from application
    public struct VConst
    {
        //Fucking colours
        public const string Black = "THEMEGUARD(RGB(0,0,0))";
        public const string Red = "THEMEGUARD(RGB(255,0,0))";
        public const string Swamp = @"THEMEGUARD(MSOTINT(THEME(""AccentColor4""),-25))";

        //Other shit
        public const string Arial = "21";
        public const string ISOCPEUR = "21";
        public const string Normal = "32"; //нормальный стиль шрифта
        public const string Italic = "34"; //курсив
        public const string SIZE = "6 pt";

    }
}

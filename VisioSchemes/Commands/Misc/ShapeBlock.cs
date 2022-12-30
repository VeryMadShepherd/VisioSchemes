using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Visio;

namespace VisioSchemes.Commands.Misc
{
    internal class ShapeBlock
    {
        internal Shape Main;
        internal Shape InnerConnector;
        internal Shape OuterConnector;
        internal Shape ElementMark;
        internal Shape GasketMark;
        internal Shape FastenerMark;

        internal ShapeBlock(Shape main)
        {
            Main = main;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisioSchemes
{
    internal enum ShapeType
    {
        Error = 0,
        Connector = 1,
        Pipe = 2,
        Element = 3,
        Marker = 4,
        Position = 5,
        Temp = 6,
        RedMark = 7,
        Fastener = 8,
        Gasket = 9,
        Sign = 10
    }
    internal enum ConnectorType
    {
        Error = 0,
        Weld = 1,
        Socket = 2,
        Thread = 3,
        Flange = 4,
        Dimension = 5
    }
    internal enum Place
    {
        Begin,
        End,
        Middle
    }
}

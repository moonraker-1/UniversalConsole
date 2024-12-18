using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalConsole.InputProcessor
{
    internal interface IInputStandards
    {
        public enum Standards
        {
            Comm = 1,
            CommObj = 2,
            CommPropObj = 3,
            CommPropObj1Obj2 = 4
        }
    }
}

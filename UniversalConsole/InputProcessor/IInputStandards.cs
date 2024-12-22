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
            Invalid = 0,
            Key = 1,
            KeyObj = 2,
            KeyPropObj = 3,
            KeyPropObj1Obj2 = 4
        }
    }
}

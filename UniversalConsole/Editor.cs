using Hardware.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalConsole
{
    internal class Editor
    {
        private struct EditorCommnads
        {
            public readonly char save = 's';
            public readonly char close = 'c';
            public readonly char delete = 'd';

            public EditorCommnads()
            {
            }
        }
        public Editor() { }

        public void Open()
        {

        }

        private void close()
        {

        }

        private bool savePreState()
        {
            return true;
        }
    }
}

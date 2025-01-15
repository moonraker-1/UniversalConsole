using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using UniversalConsoleShared;

namespace UniversalConsoleAdvanced
{
    public static class AdvancedKeywordExecutable
    {
        public static bool Execute(IAdvanced.AdvancedKeyWords key)
        {
            switch (key)
            {
                case IAdvanced.AdvancedKeyWords.SETCOLORS:
                    return executeSetColors();
                case IAdvanced.AdvancedKeyWords.EDIT:
                    return executeEdit();
                default:
                    return false;
            }
        }

        private static bool executeSetColors()
        {
            return true;
        }

        private static bool executeEdit()
        {
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalConsoleShared;

namespace ConsoleMathLib
{

    public static class CalculusOperations
    {
        private readonly struct CalculusSymbols
        {
            public static readonly string pi = "π";
            public static readonly string Sigma = "Σ";
            public static readonly string alpha = "α";
            public static readonly string beta = "β";
            public static readonly string gamma = "γ";
        }
        

        private static readonly string accessType = CheckAccess.Check();

        private static readonly double pi = Math.PI;



        public static bool IsAccessible()
            => accessType == "Advanced" || accessType == "Premium" ? true : false;

    }
}

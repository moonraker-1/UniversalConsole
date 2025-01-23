using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalConsoleShared
{
    public static class ConsoleCursorPosition
    {
        private static int bottomLoc = Console.WindowTop + Console.WindowHeight - 1;
        private static int topLoc = Console.WindowTop + 1;
        public static int OriginalTopLoc {  get; set; }
        public static int OriginalLeftLoc { get; set; }

        public static void SetAtBottom()
        {
            Console.SetCursorPosition(0, bottomLoc);
        }
        public static void SetAtTop()
        {
            Console.SetCursorPosition(0, topLoc);
        }

        public static void SetAtOriginal()
        {
            Console.SetCursorPosition(OriginalLeftLoc, OriginalTopLoc);
        }

        public static void SeparateFromMainContent()
        {
            //Console.WriteLine(new string(' ', Console.WindowWidth));
        }
    }
}

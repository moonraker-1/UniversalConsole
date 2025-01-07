using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalConsoleShared
{
    public static class Globals
    {
        /// <summary>
        /// Is required to work with the file system of the machine.
        /// </summary>
        public static string location = Environment.CurrentDirectory;

        /// <summary>
        /// Is required to manage access to different functionalities.
        /// </summary>
        public static string accessType = UniversalConsoleShared.CheckAccess.Check();

        /// <summary>
        /// Is required to manage access to different functionalities.
        /// </summary>
        public static string accessEndDate = UniversalConsoleShared.CheckAccess.CheckEndDate();

        /// <summary>
        /// Is required for various console messages.
        /// </summary>
        public static readonly string contributionMessage = "Contribute to the project by accessing our GitHub:\n" +
            "https://github.com/moonraker-1/UniversalConsole";


        public static readonly Dictionary<int, ConsoleColor> consoleColors = new Dictionary<int, ConsoleColor>()
        {
            { 0, ConsoleColor.White },
            { 1, ConsoleColor.Red },
            { 2, ConsoleColor.Blue },
            { 3, ConsoleColor.Green },
            { 4, ConsoleColor.Yellow },
            { 5, ConsoleColor.Magenta },
            { 6, ConsoleColor.Cyan },
            { 7, ConsoleColor.Gray },
            { 8, ConsoleColor.Black },
            { 9, ConsoleColor.DarkRed },
            { 10, ConsoleColor.DarkBlue },
            { 11, ConsoleColor.DarkGreen },
            { 12, ConsoleColor.DarkYellow },
            { 13, ConsoleColor.DarkMagenta },
            { 14, ConsoleColor.DarkCyan },
            { 15, ConsoleColor.DarkGray },
        };

        /// <summary>
        /// Error log file.
        /// </summary>
        public static string errorLogFile = "errorLog.txt";

        /// <summary>
        /// Event log file.
        /// </summary>
        public static string eventLogFile = "eventLog.txt";
    }
}

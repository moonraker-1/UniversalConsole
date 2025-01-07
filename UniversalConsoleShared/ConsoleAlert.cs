using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UniversalConsoleShared
{
    public static class ConsoleError
    {
        public static void ErrorNotFound(object item)
        {
            _showMessage($"{item?.ToString()} cannot be checked/retrieved. " +
                $"Check if the parameters are correct.\n");
        }

        public static void ErrorKeyNotFound(object key)
        {
            _showMessage($"Key {key?.ToString()} was not found. " +
                $"Check the spelling.\n");
        }

        public static void ErrorIncorrectParameter(object item)
        {
            _showMessage($"Parameter {item?.ToString()} cannot be used. Check spelling.");
        }

        public static void ErrorUnkown()
        {
            _showMessage("Unkown error. Please, report\n" +
                $"{Globals.contributionMessage}");
        }

        public static void ErrorInternal()
        {
            _showMessage("Unkown error. Please, report\n" +
                $"{Globals.contributionMessage}");
        }

        public static void ErrorCustom(string message)
        {
            _showMessage(message);
        }

        private static void _showMessage(string? message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n" + message + "\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    
    public static class ConsoleInformation
    {
        public static void CurrentDirectoryChange(string directory)
        {
            _showMessage($"The directory has been changed to {directory}\n");
        }

        private static void _showMessage(string message)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(message);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}

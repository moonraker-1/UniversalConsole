using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalConsoleShared
{
    public static class ConsoleError
    {
        public static void ErrorNotFound(object item)
        {
            _showMessage($"{item?.ToString()} cannot be checked/retrieved. " +
                $"Check if the parameters are correct.\n",
              ConsoleColor.Red);
        }

        public static void ErrorKeyNotFound(object key)
        {
            _showMessage($"Key {key?.ToString()} was not found. " +
                $"Check the spelling.\n",
              ConsoleColor.Red);
        }

        public static void ErrorIncorrectParameter(object item)
        {
            _showMessage($"Parameter {item?.ToString()} cannot be used. Check spelling.",
              ConsoleColor.Red);
        }

        public static void ErrorUnkown()
        {
            _showMessage("Unkown error. Please, report\n" +
                $"{Globals.contributionMessage}", ConsoleColor.Red);
        }

        public static void ErrorInternal()
        {
            _showMessage("Unkown error. Please, report\n" +
                $"{Globals.contributionMessage}", ConsoleColor.Red);
        }

        public static void ErrorCustom(string message)
        {
            _showMessage(message, ConsoleColor.Red);
        }

        private static void _showMessage(string? message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("\n" + message + "\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}

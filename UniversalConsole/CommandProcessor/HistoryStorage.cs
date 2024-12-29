using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalConsoleShared;

namespace UniversalConsole.CommandProcessor
{
    internal static class HistoryStorage
    {
        #region Variables
        private static LinkedList<string> history = new LinkedList<string>();
        #endregion

        #region Functions
        /// <summary>
        /// Writes a command to the linked list of commands.
        /// </summary>
        /// <param name="command"></param>
        public static void Write(string command)
        {
            try
            {
                history.AddFirst(command);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(Globals.contributionMessage);
            }
        }

        /// <summary>
        /// Reads and displays the latest command from the history of commands.
        /// </summary>
        public static void Read()
        {
            try
            {
                Console.Write(history.First);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(Globals.contributionMessage);
            }

        }
        /// <summary>
        /// Reads and displays the latest 50 commands from the history of commands.
        /// </summary>
        public static void ReadFifty()
        {
            try
            {
                Console.Write("Latest: ");
                int count = 0;
                foreach (var command in history)
                {
                    Console.WriteLine(command);
                    count++;
                    if (count >= 50)
                    {
                        break;
                    }
                }

                if (count == 0)
                {
                    Console.WriteLine("No commands in history.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(Globals.contributionMessage);
            }

        }
        #endregion
    }
}

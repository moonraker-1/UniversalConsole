using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.PortableExecutable;
using System.Threading;
using UniversalConsoleShared;
using UniversalConsole.InputProcessor;
using UniversalConsole.CommandProcessor;
using System.Threading.Tasks;
//using MathLibrary;

namespace UniversalConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {

            switch (Globals.accessType)
            {
                case "Free":
                    break;
                case "Advanced":
                    Console.WriteLine("Advanced Access Granted");
                    break;
                case "Premium":
                    Console.WriteLine("Premium Access Granted");
                    break;
            }
            await Run();

        }
        static async Task Run()
        {
#if DEBUG
            ConsoleDBConnection.TestConnection.TestLocal();
#endif
            while (true)
            {
                Console.Write(shortenLocation() + ">>");
                Input input = new Input();
                InputData inpData = input.Take(Convert.ToString(Console.ReadLine()));
                Command command = new Command();
                bool lol = command.Process(inpData);
                //Console.ReadKey();
            }
        }

        private static string shortenLocation()
        {
            string loc = Globals.location;
            const string replacement = "...";
            const int maxLength = 45;

            if (loc.Length <= maxLength)
            {
                return loc;
            }

            int toRemove = loc.Length - maxLength;
            int mid = loc.Length / 2;

            int leftCut = mid - toRemove / 2;
            int rightCut = mid + (toRemove + 1) / 2;

            // Construct shortened string
            string leftPart = loc.Substring(0, leftCut);
            string rightPart = loc.Substring(rightCut);

            return leftPart + replacement + rightPart;
        }

    }
}
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
        static void Main(string[] args)
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
            Run();

        }
        static void Run()
        {

            while (true) 
            {
                Console.Write(Globals.location + ">>");
                Input input = new Input();
                InputData inpData = input.Take(Convert.ToString(Console.ReadLine()));
                Command command = new Command();
                bool lol = command.Process(inpData);
                //Console.ReadKey();
            }
        }

    }
}
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.PortableExecutable;
using System.Threading;
using ConsoleAccess;
using UniversalConsole.InputProcessor;
using UniversalConsole.CommandProcessor;
//using MathLibrary;

namespace UniversalConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();

            //if (CheckAccess.Check())
            //{
            //    Console.WriteLine("Access Granted");
            //    Run();
            //}
            //else
            //{
            //    Console.WriteLine("Access Denied");
            //    Thread.Sleep(1500);
            //}


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
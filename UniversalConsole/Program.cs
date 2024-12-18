using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.PortableExecutable;
using System.Threading;
using ConsoleAccess;
//using MathLibrary;

namespace UniversalConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            if (CheckAccess.Check())
            {
                Console.WriteLine("Access Granted");
                Run();
            }
            else
            {
                Console.WriteLine("Access Denied");
                Thread.Sleep(1500);
            }


        }
        static void Run()
        {

            while (true) 
            {
                Console.Write(Globals.location + ">>");
                InputProcessor.Input input = new InputProcessor.Input();
                input.Take(Convert.ToString(Console.Read()));
                Console.Read();
            }
        }

    }
}
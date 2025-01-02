using System;
using System.Collections.Generic;

namespace HackerConsole
{
    public class Program
    {
        static List<ConsoleColor> colours = new List<ConsoleColor>()
        {
            ConsoleColor.Green, ConsoleColor.DarkGreen,
            ConsoleColor.Yellow, ConsoleColor.Blue
        };

        static List<char> chars = new List<char>()
        {
            'a', 'b', 'c', 'd', 'e',
            'v', 'w', 'x', 'y', 'z',
            '&', '@', '#', '$', '%',
            '^', '*', '(', ')', '+',
            '~'
        };

        static void Main(string[] args)
        {
            Random r = new Random();
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            int countLines = 0;
            while (true)
            {
                for (int i = 0; i < Console.WindowWidth; i++)
                {
                    int colorIndex = r.Next(0, 4);
                    Console.ForegroundColor = colours[colorIndex];
                    Console.Write(chars[r.Next(0, 20)]);
                }
                if (countLines == 1000)
                {
                    Console.Clear();
                    countLines = 0;
                }
                countLines++;
                Console.WriteLine();
            }
        }
    }
}
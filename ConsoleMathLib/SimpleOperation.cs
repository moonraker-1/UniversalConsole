using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalConsoleShared;
namespace ConsoleMathLib
{

    internal static class SimpleOperation
    {
        
        #region Math Functions

        /// <summary>
        /// Returns a result with a lower precision.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void modulusFloat(float a, float b)
        {
            try
            {
                Console.WriteLine($"****************\nAnswer: {a % b}");
            }
            catch (Exception e)
            {
                showError(e);
            }

        }

        /// <summary>
        /// Returns a result with a lower precision.
        /// </summary>
        /// <param name="num"></param>
        /// <param name="pow"></param>
        public static void powerFloat(float num, float pow)
        {
            try
            {
                Console.WriteLine($"****************\nAnswer: {Math.Pow(num, pow)}");
            }
            catch (Exception e)
            {
                showError(e);
            }
        }

        /// <summary>
        /// Returns a result with a lower precision.
        /// </summary>
        /// <param name="pow"></param>
        public static void expFloat(float pow)
        {
            try
            {
                Console.WriteLine($"****************\nAnswer: {Math.Exp(pow)}");
            }
            catch (Exception e)
            {
                showError(e);
            }
        }

        /// <summary>
        /// Returns a result with a lower precision.
        /// </summary>
        /// <param name="a"></param>
        public static void absFloat(float a)
        {
            try
            {
                Console.WriteLine($"****************\nAnswer: {Math.Abs(a)}");
            }
            catch (Exception e)
            {
                showError(e);
            }
        }

        public static void squareRootFloat(float a)
        {
            try
            {
                Console.WriteLine($"****************\nAnswer: {Math.Sqrt(a)}");
            }
            catch (Exception e)
            {
                showError(e);
            }
        }

        public static void sinFloat(float angle)
        {
            try
            {
                Console.WriteLine($"****************\nAnswer: {Math.Sin(angle)}");
            }
            catch (Exception e)
            {
                showError(e);
            }
        }

        public static void cosFloat(float angle)
        {
            try
            {
                Console.WriteLine($"****************\nAnswer: {Math.Cos(angle)}");
            }
            catch (Exception e)
            {
                showError(e);
            }
        }

        public static void tanFloat(float angle)
        {
            try
            {
                Console.WriteLine($"****************\nAnswer: {Math.Tan(angle)}");
            }
            catch (Exception e)
            {
                showError(e);
            }
        }
        public static void cotFloat(float angle)
        {
            try
            {
                Console.WriteLine($"****************\nAnswer: {1 / Math.Tan(angle)}");
            }
            catch (Exception e)
            {
                showError(e);
            }
        }




        #endregion

        #region ErrorMessages
        private static void showError(Exception e)
        {
            Console.WriteLine(e + "\n*********\n");
            Console.WriteLine(Globals.contributionMessage);
        }
        #endregion
    }
}

using Black_Tool_Kit.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Net.NetworkInformation;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace UniversalConsole.CommandProcessor
{
    internal static class KeywordExecutable
    {

        #region Functions

        /// <summary>
        /// Executes a command associated with the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Execute(IKeyWords.Keys key)
        {
            switch (key)
            {
                case IKeyWords.Keys.HELP:
                    return executeHelp();
                case IKeyWords.Keys.OS:
                    return executeOs();
                case IKeyWords.Keys.THIS:
                    return executeThis();
                case IKeyWords.Keys.COMPUTER:
                    return executeComputer();
                case IKeyWords.Keys.CALENDAR:
                    return executeCalendar();
                case IKeyWords.Keys.SYSINFO:
                    return executeSysInfo();
                case IKeyWords.Keys.IP:
                    return executeIP();
                case IKeyWords.Keys.MAC:
                    return executeMAC();
                case IKeyWords.Keys.MATH:
                    return executeMath();
                case IKeyWords.Keys.CLEAR:
                    return executeClear();
                default:
                    return false;
            }
        }

        private static bool executeHelp()
        {
            return false;
        }

        private static bool executeOs()
        {
            try
            {
                Console.WriteLine("Information about the operating system:\n**************\n");
                Console.WriteLine($"Operating System: {Environment.OSVersion.VersionString}\n\n");
                Console.WriteLine($"Is 64-bit OS: {Environment.Is64BitOperatingSystem}\n\n");
                Console.WriteLine($"Machine Name: {Environment.MachineName}\n\n");
                Console.WriteLine($"User Name: {Environment.UserName}\n\n");
                Console.WriteLine($"System Directory: {Environment.SystemDirectory}\n\n");
                Console.WriteLine($"Logical Drives: {string.Join(", ", Environment.GetLogicalDrives())}\n\n");
                Console.WriteLine($"Number of available processors: {Environment.ProcessorCount}\n\n**************\n");
            }
            catch
            {
                Console.WriteLine("No more information could be retrieved.\n");
            }
            return true;
        }

        private static bool executeThis()
        {
            return false;
        }

        private static bool executeComputer()
        {
            return false;
        }

        /// <summary>
        /// Returns the calendar of the current year.
        /// 
        /// Complexity: O(x^4) 
        /// </summary>
        /// <returns></returns>
        private static bool executeCalendar()
        {
            GregorianCalendar gc = new GregorianCalendar();
            string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            /*
             *  o o o o o o o        o o o o o o o        o o o o o o o
             *  o o o o o o o        o o o o o o o        o o o o o o o
             *  o o o o o o o        o o o o o o o        o o o o o o o
             *  o o o o o o o        o o o o o o o        o o o o o o o
             *  o o o o o o o        o o o o o o o        o o o o o o o
             *  o o o o o o o        o o o o o o o        o o o o o o o
             */

            // MONTH ___ MONTH ___ MONTH
            // _____ ___ _____ ___ _____
            // MONTH ___ MONTH ___ MONTH
            // _____ ___ _____ ___ _____
            // MONTH ___ MONTH ___ MONTH
            // _____ ___ _____ ___ _____
            // MONTH ___ MONTH ___ MONTH

            string[][][][] calendar = new string[4][][][];

            DateTime now = DateTime.Now;
            // Current year
            int year = gc.GetYear(now);

            // Current month (for iterations)
            int current_month = 0;

            // Calendar will be divided into quarters, 3 months each
            for (int quarter = 0; quarter < 4; quarter++)
            {
                string[][][] quarterArr = new string[3][][];
                // Each quarter has 4 months
                for (int month = 0; month < 3; month++)
                {
                    string[][] monthArr = new string[6][];
                    // Getting number of days for each month
                    int days_in_months = gc.GetDaysInMonth(now.Year, current_month + 1);

                    // Getting a day number (Mon-Sun, 1 to 7). START form the first day.

                    int day_of_week = Convert.ToInt32(new DateTime(now.Year, current_month + 1, 1).DayOfWeek);


                    int day_number = 1;

                    // Each month will have maximum 6 weeks involved (some are not fully populated by the days of that month)
                    for (int week = 0; week < 6; week++)
                    {
                        string[] weekArr = new string[7];
                        // Each week has 7 days
                        for (int day = 0; day < 7; day++)
                        {

                            if (day + 1 == day_of_week)
                            {
                                weekArr[day] = Convert.ToString(day_number);
                            }
                            else
                            {
                                weekArr[day] = "x";
                            }


                            // Monday to Sunday
                            if (day_of_week == 7)
                            {
                                day_of_week = 1;
                            }
                            else
                            {
                                day_of_week++;
                            }


                            // Check if all days of month have passed
                            day_number++;
                            if (day_number > days_in_months)
                            {
                                break;
                            }

                        }

                        // Each week is added to the month array
                        monthArr[week] = weekArr;
                    }
                    quarterArr[month] = monthArr;
                    //Incrementing current_month(until 12)
                    current_month += 1;
                }
                calendar[quarter] = quarterArr;
            }

            current_month = 0;
            for (int quarter = 0; quarter < calendar.Length; quarter++)
            {
                string[][][] months_in_quarter = calendar[quarter];
                Console.WriteLine($"{months[current_month]}            {months[current_month + 1]}            {months[current_month + 2]}");
                Console.WriteLine("\nMo Tu We Th Fr Sa Su   Mo Tu We Th Fr Sa Su   Mo Tu We Th Fr Sa Su");

                for (int week = 0; week < 6; week++)
                {
                    for (int month = 0; month < 3; month++)
                    {
                        for (int day = 0; day <  7; day++)
                        {
                            string day_ = months_in_quarter[month][week][day];
                            if (day == 6 && month == 2) // Last day
                            {
                                Console.Write(day_ + "|\n");
                            }
                            else if (day == 6 && month != 2)
                            {
                                Console.Write(day_ + "|        |");
                            }
                            else
                            {
                                Console.Write(day_ + " ");
                            }
                        }
                    }
                }
                current_month += 3;
            }

            if (gc.IsLeapYear(year))
            {

            }

            return false;
        }

        private static bool executeSysInfo()
        {
            return false;
        }
        private static bool executeIP()
        {
            try
            {
                Console.WriteLine("IP Configuration:");
                Console.WriteLine("--------------------");

                // Get all network interfaces
                foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
                {
                    // Display basic network interface details
                    Console.WriteLine($"Name: {networkInterface.Name}");
                    Console.WriteLine($"Description: {networkInterface.Description}");
                    Console.WriteLine($"Status: {networkInterface.OperationalStatus}");

                    // Skip if the interface is not up
                    if (networkInterface.OperationalStatus != OperationalStatus.Up)
                    {
                        Console.WriteLine();
                        continue;
                    }

                    // Get IP properties
                    IPInterfaceProperties ipProperties = networkInterface.GetIPProperties();

                    // Display unicast IP addresses
                    Console.WriteLine("IP Addresses:");
                    foreach (UnicastIPAddressInformation ip in ipProperties.UnicastAddresses)
                    {
                        Console.WriteLine($"  - {ip.Address} (Subnet Mask: {ip.IPv4Mask})");
                    }

                    // Display default gateway
                    Console.WriteLine("Default Gateway:");
                    foreach (GatewayIPAddressInformation gateway in ipProperties.GatewayAddresses)
                    {
                        Console.WriteLine($"  - {gateway.Address}");
                    }

                    // Display DNS servers
                    Console.WriteLine("DNS Servers:");
                    foreach (IPAddress dns in ipProperties.DnsAddresses)
                    {
                        Console.WriteLine($"  - {dns}");
                    }

                    Console.WriteLine();
                }
            }
            catch
            {

            }
            return true;
        }

        /// <summary>
        /// Provides MAC-address.
        /// </summary>
        /// <returns></returns>
        private static bool executeMAC()
        {
            try
            {
                var macAddr =
                    (
                        from nic in NetworkInterface.GetAllNetworkInterfaces()
                        where nic.OperationalStatus == OperationalStatus.Up
                        select nic.GetPhysicalAddress().ToString()
                    ).FirstOrDefault();
                Console.WriteLine($"MAC-address : {macAddr?.ToString()}.\n");
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool executeMath()
        {
            while (true)
            {
                Console.WriteLine("Console Calculator supports:\n" +
                "1 - Basic operations (+, -, /, *, ||);\n" +
                "2 - Complex operations (with parentheses);\n" +
                "3 - Comparison operations (>, <, <=, >=, =, <>);\n" +
                "4 - Square root - Sqrt(number);\n" +
                "5 - Round - Round(number);\n" +
                "6 - Exponential - Exp(number);\n" +
                "7 - Natural logarithm - Log(number);\n" +
                "8 - Power - Pow(number, power);\n");
                Console.WriteLine("Provide a mathematical problem to solve (or press 'q' to exit): ");
                try
                {
                    if(Console.ReadKey(false).Key != ConsoleKey.Q)
                    {
                        string? input = Console.ReadLine();

                        DataTable dt = new DataTable();
                        double result = Convert.ToDouble(dt.Compute(input, string.Empty));
                        Console.WriteLine("******************************\n" +
                            $"Result: {result}\n");
                    }
                    else
                    {
                        return true;
                    }
                }
                catch
                {
                    Console.WriteLine("INVALID INPUT PROVIDED.\n");
                    break;
                }
            }
            return false;
        }

        /// <summary>
        /// Clears the console.
        /// </summary>
        /// <returns></returns>
        private static bool executeClear()
        {
            try
            {
                Console.Clear();
                return true;
            }
            catch
            {
                Console.WriteLine("Internal error. Please, proceed with normal operations.\n");
                return false;
            }
        }

        #endregion
    }

    internal static class KeywordObjectExecutable
    {

    }
}

using Black_Tool_Kit.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Net.NetworkInformation;
using System.Net;


using Hardware.Info;
using System.Threading;
using ConsoleMathLib;
using UniversalConsoleShared;
using System.Reflection;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using UniversalConsole.Interfaces;
using System.Security.AccessControl;

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
            HistoryStorage.Write(key.ToString());
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
                case IKeyWords.Keys.IP:
                    return executeIP();
                case IKeyWords.Keys.MAC:
                    return executeMAC();
                case IKeyWords.Keys.MATH:
                    return executeMath();
                case IKeyWords.Keys.CLEAR:
                    return executeClear();
                case IKeyWords.Keys.HISTORY:
                    return executeHistory();
                case IKeyWords.Keys.UPDACCESS:
                    return executeUpdateAccess();
                case IKeyWords.Keys.FONTCOLOR:
                    return executeFontColor();
                case IKeyWords.Keys.WRITE:
                    return executeEditor();
                case IKeyWords.Keys.DIRINFO:
                    // This command may be with a key and no object (no URl).
                    return executeDirectoryInfo(key);
                case IKeyWords.Keys.HACKER:
                    executeHacker();
                    return true;
                case IKeyWords.Keys.GOTO:
                    // This command may be with a key and no object (no URl).
                    return executeGoTo(key);
                case IKeyWords.Keys.LOC:
                    return executeLocation();
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
            catch(Exception e )
            {
                Console.WriteLine("No more information could be retrieved.\n");
                ErrorLog.Write(e.Message, DateTime.Now);
                return false;
            }
            return true;
        }

        private static bool executeThis()
        {
            try
            {
                List<string> thisArgs = new List<string>();
                Console.WriteLine("**********************");
                thisArgs.Add("The information about the machine and the running process:\n");
                thisArgs.Add($"Machine Name: {Environment.MachineName}");
                thisArgs.Add($"User Name: {Environment.UserName}");
                thisArgs.Add($"Operating System: {Environment.OSVersion.VersionString}");

                if (Environment.Is64BitOperatingSystem)
                {
                    thisArgs.Add($"Is 64-bit OS: Yes\n");
                }
                else
                {
                    thisArgs.Add($"Is 64-bit OS: No\n");
                }
                thisArgs.Add($"System Directory: {Environment.SystemDirectory}");
                thisArgs.Add($"Number of available processors: {Environment.ProcessorCount}");

                thisArgs.Add($"*********************\nDate & Time: {DateTime.Now}");

                thisArgs.Add($"Subscription type: {Globals.accessType}");

                thisArgs.Add($"Subscription end date: {Globals.accessEndDate}");

                foreach (string arg in thisArgs)
                {
                    Console.WriteLine(arg);
                }
            AskAboutSaving:
                Console.Write("\nWould you like to get it written to a text file? y/n: ");
                try
                {
                    string input1 = Convert.ToString(Console.ReadLine());
                    if (input1 != null)
                    {
                        if (input1.ToUpper() == "Y" || input1.ToUpper() == "YES")
                        {
                            FileProcessor.FileWriting.WriteToFile(thisArgs);
                        }
                        else if (input1.ToUpper() != "N" && input1.ToUpper() != "NO")
                        {
                            goto AskAboutSaving;
                        }
                    }

                }
                catch
                {
                    Console.WriteLine("Try again");
                }
            }
            catch(Exception e)
            {
                ConsoleError.ErrorUnkown();
                ErrorLog.Write(e.Message, DateTime.Now);
                return false;
            }
            return true;
        }

        private static bool executeComputer()
        {
            bool result = true;
            ComputerInformation computerInformation = new ComputerInformation();
            Console.Write("\nWould you like to get it written to a text file? y/n: ");
            try
            {
                string input1 = Convert.ToString(Console.ReadLine());
                if (input1 != null) 
                {
                    if (input1.ToUpper() == "Y" || input1.ToUpper() == "YES")
                    {
                        Console.WriteLine("Provide the address, or press ENTER - it will be saved on the Desktop:");
                        string? path = Convert.ToString(Console.ReadLine());
                        if (!string.IsNullOrEmpty(path))
                        {
                            result = computerInformation.Retrieve(true, path);
                        }
                        else
                        {
                            result = computerInformation.Retrieve(true, null);
                        }
                    }
                    else
                    {
                        result = computerInformation.Retrieve(false, null);
                    }
                }

                return result;
            }
            catch (Exception e)
            {
                ConsoleError.ErrorUnkown();
                ErrorLog.Write(e.Message, DateTime.Now);
                return false;
            }
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
                Console.WriteLine("\nMo Tu We Th Fr Sa Su          Mo Tu We Th Fr Sa Su          Mo Tu We Th Fr Sa Su");

                for (int week = 0; week < 6; week++)
                {
                    for (int month = 0; month < 3; month++)
                    {
                        int week_string_length = 0;
                        for (int day = 0; day <  7; day++)
                        {
                            string day_ = months_in_quarter[month][week][day]; 
                            if (day_ is not null)
                            {
                                if (day_.Length == 1)
                                {
                                    day_ += " ";
                                }
                            }
                            if (day == 6 && month == 2) // Last day
                            {
                                Console.Write(day_ + "|\n");
                                week_string_length++;
                            }
                            else if (day == 6 && month != 2)
                            {
                                Console.Write(day_ + "|        |");
                                week_string_length++;
                            }
                            else
                            {
                                Console.Write(day_ + " ");
                                week_string_length++;
                            }

                        }
                        if (week_string_length < 20)
                        {
                            for (int w = week_string_length; w <= 20; w++)
                            {
                                Console.Write(" ");
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

        private static bool executeIP()
        {
            try
            {
                List<string> IPArgs = new List<string>();
                IPArgs.Add("IP Configuration:");
                IPArgs.Add("--------------------");

                // Get all network interfaces
                foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
                {
                    // Display basic network interface details
                    IPArgs.Add($"Name: {networkInterface.Name}");
                    IPArgs.Add($"Description: {networkInterface.Description}");
                    IPArgs.Add($"Status: {networkInterface.OperationalStatus}");

                    // Skip if the interface is not up
                    if (networkInterface.OperationalStatus != OperationalStatus.Up)
                    {
                        IPArgs.Add("\n");
                        continue;
                    }

                    // Get IP properties
                    IPInterfaceProperties ipProperties = networkInterface.GetIPProperties();

                    // Display unicast IP addresses
                    IPArgs.Add("IP Addresses:");
                    foreach (UnicastIPAddressInformation ip in ipProperties.UnicastAddresses)
                    {
                        IPArgs.Add($"  - {ip.Address} (Subnet Mask: {ip.IPv4Mask})");
                    }

                    // Display default gateway
                    IPArgs.Add("Default Gateway:");
                    foreach (GatewayIPAddressInformation gateway in ipProperties.GatewayAddresses)
                    {
                        IPArgs.Add($"  - {gateway.Address}");
                    }

                    // Display DNS servers
                    IPArgs.Add("DNS Servers:");
                    foreach (IPAddress dns in ipProperties.DnsAddresses)
                    {
                        IPArgs.Add($"  - {dns}");
                    }

                    IPArgs.Add("\n");
                }

                foreach (string arg in IPArgs)
                {
                    Console.WriteLine(arg);
                }

                AskAboutSavingIP:
                Console.Write("\nWould you like to get it written to a text file? y/n: ");
                try
                {
                    string input1 = Convert.ToString(Console.ReadLine());
                    if (input1 != null)
                    {
                        if (input1.ToUpper() == "Y" || input1.ToUpper() == "YES")
                        {
                            FileProcessor.FileWriting.WriteToFile(IPArgs);
                        }
                        else if (input1.ToUpper() != "N" && input1.ToUpper() != "NO")
                        {
                            goto AskAboutSavingIP;
                        }
                    }

                }
                catch
                {
                    Console.WriteLine("Try again");
                }


                return true;

            }
            catch(Exception e)
            {
                ConsoleError.ErrorUnkown();
                ErrorLog.Write(e.Message, DateTime.Now);
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

                AskAboutSavingMAC:
                Console.Write("\nWould you like to get it written to a text file? y/n: ");
                try
                {
                    string input1 = Convert.ToString(Console.ReadLine());
                    if (input1 != null)
                    {
                        if (input1.ToUpper() == "Y" || input1.ToUpper() == "YES")
                        {
                            FileProcessor.FileWriting.WriteToFile(macAddr?.ToString());
                        }
                        else if (input1.ToUpper() != "N" && input1.ToUpper() != "NO")
                        {
                            goto AskAboutSavingMAC;
                        }
                    }

                }
                catch
                {
                    Console.WriteLine("Try again");
                }
                return true;
            }
            catch(Exception e)
            {
                ConsoleError.ErrorUnkown();
                ErrorLog.Write(e.Message, DateTime.Now);
                return false;
            }
        }

        private static bool executeMath()
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
            Thread.Sleep(1000);
            if (CalculusOperations.IsAccessible())
            {
                Console.WriteLine("");
            }
            while (true)
            {

                Console.WriteLine("Provide a mathematical problem to solve (or press 'q' to exit): ");
                try
                {
                    string? input = "";
                    //string
                    while (true)
                    {
                        ConsoleKeyInfo i = Console.ReadKey();
                        if (i.Key == ConsoleKey.Q && !(input.EndsWith('s') || input.EndsWith('S')))
                        {
                            Console.WriteLine();
                            return true;
                        }
                        else if (i.Key == ConsoleKey.Enter)
                        {
                            break;
                        }
                        else
                        {
                            input += i.KeyChar;
                        }
                        
                    }
                    //if (Console.ReadKey(false).Key != ConsoleKey.Q)
                    //{
                    //    string? input = Console.ReadLine();

                        DataTable dt = new DataTable();
                        double result = Convert.ToDouble(dt.Compute(input, string.Empty));
                        Console.WriteLine("******************************\n" +
                            $"Result: {result}\n");
                    //}
                    //else
                    //{
                    //    return true;
                    //}
                }
                catch (Exception e)
                {
                    ConsoleError.ErrorIncorrectParameter(e);
                    ErrorLog.Write(e.Message, DateTime.Now);
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
            catch (Exception e)
            {
                Console.WriteLine("Internal error. Please, proceed with normal operations.\n");
                ErrorLog.Write(e.Message, DateTime.Now);
                return false;
            }
        }

        /// <summary>
        /// Returns 50 latest commands.
        /// </summary>
        /// <returns></returns>
        private static bool executeHistory()
        {
            try
            {
                HistoryStorage.ReadFifty();
            }
            catch(Exception e)
            {
                ErrorLog.Write(e.Message, DateTime.Now);
                return false;
            }
            return true;
        }

        private static bool executeUpdateAccess()
        {
            Task.Run(() => 
            {
                // To be implemented.
            });
            return true;
        }

        private static bool executeFontColor()
        {
            Console.WriteLine("\n**************");
            Console.Write("Which color would you like (type a number):\n");
            Console.Write("0 - White;\n" +
                "1 - Red;\n" +
                "2 - Blue;\n" +
                "3 - Green;\n" +
                "4 - Yellow;\n" +
                "5 - Magenta;\n" +
                "6 - Cyan;\n" +
                "7 - Gray;\n" +
                "8 - Black;\n" +
                "9 - Dark Red;\n" +
                "10 - Dark Blue;\n" +
                "11 - Dark Green;\n" +
                "12 - Dark Yellow;\n" +
                "13 - Dark Magenta;\n" +
                "14 - Dark Cyan;\n" +
                "15 - Dark Gray;\n");
            Console.WriteLine("\n**************");
            Console.Write("\n:>>> ");
            try
            {
                int color = Convert.ToInt16(Console.ReadLine());
                Console.ForegroundColor = Globals.consoleColors[color];
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR: Incorrect Input.");
                ErrorLog.Write(e.Message, DateTime.Now);
                return false;
            }
        }

        private static bool executeEditor()
        {
            return true;
        }

        private static bool executeDirectoryInfo(IKeyWords.Keys key)
        {
            return KeywordObjectExecutable.Execute(key, null);
        }

        /// <summary>
        /// Opens another terminal where random 'matrix-like' characters will be generated to fill
        /// that terminal.
        /// </summary>
        private static void executeHacker()
        {
            Thread t = new Thread(() =>
            {
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = "HackerConsole.exe",
                        Arguments = "/K dotnet run",
                        UseShellExecute = true,
                        CreateNoWindow = false
                    };

                    Process.Start(startInfo);
                }
                catch (Exception e)
                {
                    ConsoleError.ErrorInternal();
                    ErrorLog.Write(e.Message, DateTime.Now);
                }
            });
            t.Start();
        }

        private static bool executeGoTo(IKeyWords.Keys key)
        {
            return KeywordObjectExecutable.Execute(key, null);
        }

        private static bool executeLocation()
        {
            ConsoleInformation.Custom($"Current location: \n{Globals.location}\n", false);
            return true;
        }

        #endregion
    }

    internal static class KeywordObjectExecutable
    {
        public static bool Execute(IKeyWords.Keys key, string? obj)
        {
            switch (key)
            {
                case IKeyWords.Keys.FILEINFO:
                    return executeFileInfo(obj);
                case IKeyWords.Keys.DIRINFO:
                    return executeDirInfo(obj);
                case IKeyWords.Keys.REMOVE:
                    return executeRemove(obj);
                case IKeyWords.Keys.GOTO:
                    return executeGoTo(obj);
                default:
                    return false;
            }
        }

        /// <summary>
        /// Provides information about the specified file.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private static bool executeFileInfo(string? filename) =>
            DirectoryFileInfo.GetFileInfo(filename);

        /// <summary>
        /// Provides information about the current or another directory.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static bool executeDirInfo(string? path) =>
            DirectoryFileInfo.GetDirInfo(path);

        private static bool executeRemove(string? whatToRemove)
        {
            try
            {
                if (Enum.TryParse(whatToRemove.ToUpper(), out IObjects.Params param))
                {
                    if (param.ToString().ToUpper() == "ALL")
                    {

                    }
                }
            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: you did not clearly specify what to remove.");
                Console.ForegroundColor = ConsoleColor.White;

                ErrorLog.Write(e.Message, DateTime.Now);
                return false;
            }
            
            return true;
        }

        private static bool executeGoTo(string? location)
        {
            if (string.IsNullOrEmpty(location))
            {
                Environment.CurrentDirectory = "../";
                Globals.location = Environment.CurrentDirectory;
                ConsoleInformation.CurrentDirectoryChange(Globals.location);
                return true;
            }
            else if (Enum.TryParse(location.ToUpper(), out IObjects.Params param))
            {
                if (param == IObjects.Params.BASE)
                {
                    Environment.CurrentDirectory = Environment.SystemDirectory;
                    Globals.location = Environment.CurrentDirectory;
                    ConsoleInformation.CurrentDirectoryChange(Globals.location);
                    return true;
                }
                return false;
            }
            else
            {
                try
                {
                    Environment.CurrentDirectory = location;
                    Globals.location = Environment.CurrentDirectory;
                    ConsoleInformation.CurrentDirectoryChange(Globals.location);
                    return true;
                }
                catch (Exception e)
                {
                    ConsoleError.ErrorCustom($"Location {location} cannot be found, please, check spelling.\n");
                    ErrorLog.Write(e.Message, DateTime.Now);
                    return false;
                }
            }
        }
    }
}


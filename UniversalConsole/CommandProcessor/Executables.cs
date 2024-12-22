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
            return false;
        }

        private static bool executeThis()
        {
            return false;
        }

        private static bool executeComputer()
        {
            return false;
        }
        private static bool executeCalendar()
        {
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
                    if(Console.ReadKey().Key != ConsoleKey.Q)
                    {
                        string input = Convert.ToString(Console.ReadLine());

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
                    Console.WriteLine("INVALID INPUR PROVIDED.\n");
                    return false;
                }
            }
        }

        #endregion
    }

    internal static class KeywordObjectExecutable
    {

    }
}

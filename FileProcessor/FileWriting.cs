using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Principal;
namespace FileProcessor
{
    public static class FileWriting
    {
        /// <summary>
        /// Created specifically for writing the information about a computer to a text file.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool WriteToFile(Dictionary<string, Dictionary<string, List<string>>> content, string path)
        {
            string os = OS();
            switch (os)
            {
                case "Windows":
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(path))
                        {
                            sw.WriteLine("Hardware Information:\n\n");

                            sw.WriteLine("*****************************");
                            // Iterate through all hardware components
                            foreach (string key in content.Keys)
                            {
                                sw.WriteLine("*****************************");
                                sw.WriteLine(key);
                                Dictionary<string, List<string>> hardwareComponent = content[key];
                                // Iterate through all components instances (e.g. may be 2 monitors)
                                foreach (string componentInstance in hardwareComponent.Keys)
                                {
                                    // Iterate through all characteristics of each component
                                    List<string> componentInstanceCharacteristics = hardwareComponent[componentInstance];
                                    foreach (string characteristic in componentInstanceCharacteristics)
                                    {
                                        sw.WriteLine(characteristic);
                                    }
                                }
                                sw.WriteLine("*****************************");
                                sw.WriteLine();
                                sw.WriteLine("*****************************");
                            }

                        }
                    }
                    catch
                    {
                        Console.WriteLine("Error while writing the information to the specified path." +
                            " The default path will be used:\n");

                        Random random = new Random();
                        string alter_path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) +
                            $"/computer_information_{random.Next(10000)}";

                        Console.WriteLine(alter_path);

                        using (StreamWriter sw = new StreamWriter(alter_path))
                        {
                            sw.WriteLine("Hardware Information:\n\n");

                            sw.WriteLine("*****************************");
                            // Iterate through all hardware components
                            foreach (string key in content.Keys)
                            {
                                sw.WriteLine("*****************************");
                                sw.WriteLine(key);
                                Dictionary<string, List<string>> hardwareComponent = content[key];
                                // Iterate through all components instances (e.g. may be 2 monitors)
                                foreach (string componentInstance in hardwareComponent.Keys)
                                {
                                    // Iterate through all characteristics of each component
                                    List<string> componentInstanceCharacteristics = hardwareComponent[componentInstance];
                                    foreach (string characteristic in componentInstanceCharacteristics)
                                    {
                                        sw.WriteLine(characteristic);
                                    }
                                }
                                sw.WriteLine("*****************************");
                                sw.WriteLine();
                                sw.WriteLine("*****************************");
                            }

                        }

                    }
                    break;

            }
            return true;
        }

        private static string OS()
        {
            if (OperatingSystem.IsWindows())
            {
                return "Windows";
            }
            else if (OperatingSystem.IsLinux())
            {
                return "Linux";
            }
            else if (OperatingSystem.IsMacOS())
            {
                return "MacOS";
            }
            return "undefined";
        } 
    }
}

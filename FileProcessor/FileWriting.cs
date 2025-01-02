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
                            sw.Close();
                        }

                    }
                    break;

            }
            return true;
        }


        public static bool WriteToFile(List<string> content)
        {
            try
            {
                Random random = new Random();
                string fileName = $"\\{random.Next(10000)}.txt";
                string folder = GetAppropriateFolder();
                string defaultPath = folder + $"{fileName}";
                using (StreamWriter sw = new StreamWriter(defaultPath))
                {
                    foreach (string c in content)
                    {
                        sw.WriteLine(c);
                    }
                    sw.Close();
                }
                Console.WriteLine($"The file {fileName} has been saved to {folder}\n");
                return true;
            }
            catch
            {
                Console.WriteLine("File cannot be saved. Please, save " +
                    "the text output in the terminal yourself.\n");
                return false;
            }
        }

        public static bool WriteToFile(string? content)
        {
            try
            {
                if (string.IsNullOrEmpty(content))
                {
                    return false;
                }

                Random random = new Random();
                string fileName = $"\\{random.Next(10000)}.txt";
                string folder = GetAppropriateFolder();
                string defaultPath = folder + $"{fileName}";
                using (StreamWriter sw = new StreamWriter(defaultPath))
                {
                    sw.WriteLine(content);
                    sw.Close();
                }
                Console.WriteLine($"The file {fileName} has been saved to {folder}\n");
                return true;
            }
            catch
            {
                Console.WriteLine("File cannot be saved. Please, save " +
                    "the text output in the terminal yourself.\n");
                return false;
            }
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

        /// <summary>
        /// Find a folder to save a file to.
        /// </summary>
        /// <returns>A path to that folder, or null.</returns>
        private static string? GetAppropriateFolder()
        {
            string? path = FolderSearch.SearchDesktop();
            if (path == null || path == "")
            {
                path = FolderSearch.SearchDesktopDirectory();
                if (path == null || path == "")
                {
                    path = FolderSearch.SearchMyDocuments();
                    if (path == null || path == "")
                    {
                        path = FolderSearch.SearchPersonal();
                    }
                }
            }
            return path;
        }
    }
}

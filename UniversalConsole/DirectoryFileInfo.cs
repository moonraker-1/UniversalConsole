using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using UniversalConsoleShared;

namespace UniversalConsole
{
    internal static class DirectoryFileInfo
    {
        public static bool GetFileInfo(string? path)
        {
            try
            {
                if (File.Exists(path))
                {
                    List<string> fileInfoList = new List<string>();

                    FileInfo fileInfo = new FileInfo(path);
                    fileInfoList.Add("File Information:");
                    fileInfoList.Add($"Full Name: {fileInfo.FullName}");
                    fileInfoList.Add($"Name: {fileInfo.Name}");
                    fileInfoList.Add($"Extension: {fileInfo.Extension}");
                    fileInfoList.Add($"Directory: {fileInfo.DirectoryName}");
                    fileInfoList.Add($"Size: {fileInfo.Length} bytes");
                    fileInfoList.Add($"Created: {fileInfo.CreationTime}");
                    fileInfoList.Add($"Last Accessed: {fileInfo.LastAccessTime}");
                    fileInfoList.Add($"Last Modified: {fileInfo.LastWriteTime}");
                    fileInfoList.Add($"Read-Only: {fileInfo.IsReadOnly}");

                    Console.WriteLine("********************\n");

                    foreach(string info in fileInfoList)
                    {
                        Console.WriteLine(info);
                    }
                    Console.WriteLine("\n********************\n");

                    AskAboutSavingFileInfo:
                    Console.Write("\nWould you like to get it written to a text file? y/n: ");
                    try
                    {
                        string? input1 = Convert.ToString(Console.ReadLine());
                        if (input1 != null)
                        {
                            if (input1.ToUpper() == "Y" || input1.ToUpper() == "YES")
                            {
                                FileProcessor.FileWriting.WriteToFile(fileInfoList);
                            }
                            else if (input1.ToUpper() != "N" && input1.ToUpper() != "NO")
                            {
                                goto AskAboutSavingFileInfo;
                            }
                        }

                    }
                    catch
                    {
                        Console.WriteLine("Try again");
                    }
                    return true;
                }
                else
                {

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: The file does not exist");
                    Console.ForegroundColor = ConsoleColor.White;
                    return false;
                }
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: provide a correct file name, or path to a file from another directory.");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
        }

        public static bool GetDirInfo(string? path)
        {

            if (string.IsNullOrEmpty(path))
            {
                path = Environment.CurrentDirectory;
            }
            else if (!Directory.Exists(path))
            {
                string message = "ERROR: provide a correct directory name, or do not provide anything " +
                    "- the program will\nanalyze the current directory.";
                ConsoleAlert.ErrorCustom(message);
                ErrorLog.Write(message, DateTime.Now);
                return false;
            }

            try
            {
                List<string> dirInfoList = new List<string>();
                DirectoryInfo dirInfo = new DirectoryInfo(path);

                // Get details about the directory
                dirInfoList.Add("\nDirectory Information:");
                dirInfoList.Add($"Full Name: {dirInfo.FullName}");
                dirInfoList.Add($"Name: {dirInfo.Name}");
                dirInfoList.Add($"Parent Directory: {dirInfo.Parent}");
                dirInfoList.Add($"Creation Time: {dirInfo.CreationTime}");
                dirInfoList.Add($"Last Access Time: {dirInfo.LastAccessTime}");
                dirInfoList.Add($"Last Write Time: {dirInfo.LastWriteTime}");

                // Check attributes
                dirInfoList.Add("\nDirectory Attributes:");
                dirInfoList.Add($"Read-only: {dirInfo.Attributes.HasFlag(FileAttributes.ReadOnly)}");
                dirInfoList.Add($"Hidden: {dirInfo.Attributes.HasFlag(FileAttributes.Hidden)}");

                // List files in the directory
                dirInfoList.Add("\nFiles in Directory:");
                FileInfo[] files = dirInfo.GetFiles();
                foreach (FileInfo file in files)
                {
                    dirInfoList.Add($"- {file.Name} ({(double)file.Length / 1024} kilobytes)");
                }

                // List subdirectories
                dirInfoList.Add("\nSubdirectories:");
                DirectoryInfo[] subDirs = dirInfo.GetDirectories();
                foreach (DirectoryInfo subDir in subDirs)
                {
                    dirInfoList.Add($"- {subDir.Name}");
                }

                foreach (string info in dirInfoList)
                {
                    Console.WriteLine(info);
                }

                AskAboutSavingDirInfo:
                Console.Write("\nWould you like to get it written to a text file? y/n: ");
                try
                {
                    string? input1 = Convert.ToString(Console.ReadLine());
                    if (input1 != null)
                    {
                        if (input1.ToUpper() == "Y" || input1.ToUpper() == "YES")
                        {
                            FileProcessor.FileWriting.WriteToFile(dirInfoList);
                        }
                        else if (input1.ToUpper() != "N" && input1.ToUpper() != "NO")
                        {
                            goto AskAboutSavingDirInfo;
                        }
                    }

                }
                catch
                {
                    Console.WriteLine("Try again");
                }
                return true;

            }
            catch
            {
                Console.WriteLine("Unknown error. Please report.");
                Console.WriteLine(Globals.contributionMessage);
                return false;
            }
        }

    }
}

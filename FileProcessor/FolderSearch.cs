using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcessor
{
    internal static class FolderSearch
    {

        /// <summary>
        /// Look for a Desktop Directory folder or its equivalent.
        /// </summary>
        /// <returns>Path to the Desktop Directory folder or its equivalent.</returns>
        public static string? SearchDesktopDirectory()
        {
            string desktopDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            if (Directory.Exists(desktopDirectory))
            {
                return desktopDirectory;
            }
            return null;
        }

        /// <summary>
        /// Look for a Desktop folder or its equivalent.
        /// </summary>
        /// <returns>Path to the Desktop folder or its equivalent.</returns>
        public static string? SearchDesktop()
        {

            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (Directory.Exists(desktop))
            {
                return desktop;
            }
            return null;
        }

        /// <summary>
        /// Look for a Personal folder or its equivalent.
        /// </summary>
        /// <returns>Path to the Personal folder or its equivalent.</returns>
        public static string? SearchPersonal()
        {
            string personal = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            if (Directory.Exists(personal))
            {
                return personal;
            }
            return null;
        }

        /// <summary>
        /// Look for a My Documents folder or its equivalent.
        /// </summary>
        /// <returns>Path to the My Documents folder or its equivalent.</returns>
        public static string? SearchMyDocuments()
        {
            string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (Directory.Exists(myDocuments))
            {
                return myDocuments;
            }
            return null;
        }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalConsoleShared
{
    public static class ErrorLog
    {
        /// <summary>
        /// Write an error to the error log file.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="dateTime"></param>
        public static void Write(string message, DateTime dateTime)
        {
            if (File.Exists(Globals.errorLogFile))
            {
                using (StreamWriter sw = new StreamWriter(Globals.errorLogFile, append: true))
                {
                    sw.WriteLine("\n****************************\n");
                    sw.WriteLine(dateTime.ToString());
                    sw.WriteLine(message);
                    sw.WriteLine("\n****************************\n");
                    sw.Close();
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(Globals.errorLogFile))
                {
                    sw.WriteLine("\n****************************\n");
                    sw.WriteLine(dateTime.ToString());
                    sw.WriteLine(message);
                    sw.WriteLine("\n****************************\n");
                    sw.Close();
                }
            }
        }

    }
    public static class EventLog
    {
        /// <summary>
        /// Write an event to the event log file.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="dateTime"></param>
        public static void Write(string message, DateTime dateTime)
        {
            if (File.Exists(Globals.eventLogFile))
            {
                using (StreamWriter sw = new StreamWriter(Globals.eventLogFile, append: true))
                {
                    sw.WriteLine("\n****************************\n");
                    sw.WriteLine(dateTime.ToString());
                    sw.WriteLine(message);
                    sw.WriteLine("\n****************************\n");
                    sw.Close();
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(Globals.eventLogFile))
                {
                    sw.WriteLine("\n****************************\n");
                    sw.WriteLine(dateTime.ToString());
                    sw.WriteLine(message);
                    sw.WriteLine("\n****************************\n");
                    sw.Close();
                }
            }
        }
    }
}

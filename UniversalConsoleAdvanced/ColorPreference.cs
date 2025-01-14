using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using UniversalConsoleShared;
using System.Drawing;

namespace UniversalConsoleAdvanced
{
    public static class ColorPreference
    {
        [Serializable]
        private struct SavedColorPreferences
        {
            public ConsoleColor BackgroundColor { get; set; }
            public ConsoleColor ForegroundColor { get; set; }
        }

        /// <summary>
        /// Applies color preferences of the user.
        /// </summary>
        public static void ApplyColors()
        {
            try
            {
                string preferences = File.ReadAllText("usrprefs.json");

                SavedColorPreferences userPreferences = JsonSerializer.Deserialize<SavedColorPreferences>(preferences);
                AskAboutClearing:
                Console.Write("To set the background color, the terminal needs to be cleared. Proceed? (y/n): ");

                string? input = Convert.ToString(Console.ReadLine());
                if (string.IsNullOrEmpty(input))
                {
                    goto AskAboutClearing;
                }
                else
                {
                    if (input.ToUpper() == "Y" || input.ToUpper() == "YES")
                    {
                        Console.Clear();
                        Console.BackgroundColor = userPreferences.BackgroundColor;
                        Globals.terminalBackColor = userPreferences.BackgroundColor;
                    }
                    Console.ForegroundColor = userPreferences.ForegroundColor;
                    Globals.terminalFontColor = userPreferences.ForegroundColor;
                }


            }
            catch(Exception ex)
            {
                ConsoleError.ErrorCustom("Color preferences cannot be retrieved");
                ErrorLog.Write(ex.Message, DateTime.Now);
            }
        }

        /// <summary>
        /// Sets color preferences for further use.
        /// </summary>
        /// <param name="fontColorCode"></param>
        /// <param name="backColorCode"></param>
        public static void WritePreferences(int fontColorCode, int backColorCode)
        {

            ConsoleColor font = Globals.consoleColors[fontColorCode];
            ConsoleColor back = Globals.consoleColors[backColorCode];
            SavedColorPreferences newPref = new SavedColorPreferences
            {
                BackgroundColor = back,
                ForegroundColor = font
            };

            try
            {
                string prefs = JsonSerializer.Serialize(newPref);
                File.WriteAllText("usrprefs.json", prefs);
            }
            catch(Exception e)
            {
                ConsoleError.ErrorUnkown();
                ErrorLog.Write(e.Message, DateTime.Now);
            }

        }

        /// <summary>
        /// Resets console colors to default colors of the system.
        /// </summary>
        public static void Reset()
        {
            Console.ResetColor();
            ConsoleInformation.Custom("\nSuccessfully re - set to default colours of the system.\n");
        }
    }
}

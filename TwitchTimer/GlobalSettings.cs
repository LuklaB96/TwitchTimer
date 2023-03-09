using System;
using System.IO;
using System.Windows.Forms;

namespace TwitchTimer
{
    internal class GlobalSettings
    {
        public const int WM_HOTKEY_MSG_ID = 0x0312;
        private const string settingsPath = "userSettings.txt";

        public static string pauseKeyName = "NumPad1";
        public static int pauseKeyCode = 97;
        public static string resumeKeyName = "NumPad2";
        public static int resumeKeyCode = 98;
        public static string resetKeyName = "NumPad3";
        public static int resetKeyCode = 99;

        public static void Save()
        {
            using (StreamWriter sw = File.CreateText(settingsPath))
            {
                sw.WriteLine(pauseKeyName);
                sw.WriteLine(pauseKeyCode);
                sw.WriteLine(resumeKeyName);
                sw.WriteLine(resumeKeyCode);
                sw.WriteLine(resetKeyName);
                sw.WriteLine(resetKeyCode);
            }
        }
        public static void Load()
        {
            if (File.Exists(settingsPath))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(settingsPath))
                    {
                        pauseKeyName = sr.ReadLine();
                        pauseKeyCode = Int32.Parse(sr.ReadLine());
                        resumeKeyName = sr.ReadLine();
                        resumeKeyCode = Int32.Parse(sr.ReadLine());
                        resetKeyName = sr.ReadLine();
                        resetKeyCode = Int32.Parse(sr.ReadLine());
                    }
                } catch(Exception ex)
                {
                    MessageBox.Show("userSettings.txt file is corrupted!");
                }
            }
        }
    }
}

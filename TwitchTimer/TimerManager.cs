using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing.Drawing2D;

namespace TwitchTimer
{
    internal class TimerManager
    {
        private const string timerSavePath = "save.txt";
        private const string timerTxtPath = "timer.txt";
        private bool paused = true;
        private int seconds = 0;
        private int minutes = 0;
        private int hours = 0;
        public TimerManager() 
        {
            Initalize();
        }
        private void Initalize()
        {
            loadTimerFromFile();
        }
        public void Pause()
        {
            paused = true;
            saveTimerToFile();
        }
        public void Resume()
        {
            paused = false;
            saveTimerToFile();
        }
        public void Reset()
        {
            seconds = 0;
            minutes = 0;
            hours = 0;
            paused = true;
            saveTimerToFile();
        }
        private void saveTimerToFile()
        {
            if (!File.Exists(timerSavePath))
            {
                using (StreamWriter sw = File.CreateText(timerSavePath))
                {
                    int secs = (hours * 60 * 60) + (minutes * 60) + seconds;
                    sw.WriteLine(secs);
                }
            }
            else
            {
                using (StreamWriter sw = File.CreateText(timerSavePath))
                {
                    int secs = (hours * 60 * 60) + (minutes * 60) + seconds;
                    sw.WriteLine(secs);
                }
            }
            if (!File.Exists(timerTxtPath))
            {
                using (StreamWriter sw = File.CreateText(timerTxtPath))
                {
                    sw.WriteLine(GetTimer());
                }
            }
            else
            {
                using (StreamWriter sw = File.CreateText(timerTxtPath))
                {
                    sw.WriteLine(GetTimer());
                }
            }
        }
        private void loadTimerFromFile()
        {
            if (!File.Exists(timerSavePath))
            {
                using (StreamWriter sw = File.CreateText(timerSavePath))
                {
                    int secs = (hours * 60 * 60) + (minutes * 60) + seconds;
                    sw.WriteLine(secs);
                }
            }
            else
            {
                using(StreamReader sr = new StreamReader(timerSavePath))
                {
                    string time = sr.ReadLine();
                    int current = 0;
                    Int32.TryParse(time, out current);
                    TimeSpan interval = TimeSpan.FromSeconds(current);
                    seconds = interval.Seconds;
                    minutes= interval.Minutes;
                    hours = interval.Hours;
                }

            }

        }
        public void Update()
        {
            if (!paused)
            {
                if (seconds < 59)
                {
                    seconds += 1;
                }
                else
                {
                    seconds = 0;
                    if (minutes < 59)
                    {
                        minutes += 1;
                    }
                    else
                    {
                        minutes = 0;
                        hours += 1;
                    }
                }
                saveTimerToFile();
            }
        }

        public string GetTimer()
        {
            string formatedTimer = "";
            if (hours < 100)
                formatedTimer = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
            else
                formatedTimer = string.Format("{0:000}:{1:00}:{2:00}", hours, minutes, seconds);
            return formatedTimer;
        }


    }
}

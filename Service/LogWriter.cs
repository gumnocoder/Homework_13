﻿using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Homework_13.Service
{
    class LogWriter
    {
        private static string _logFileName;
        static bool _firstStart = true;

        private static void SetCurrentLogFileName()
        {
            var now = DateTime.UtcNow;
            _logFileName = $"{(now.Year.ToString())}_{now.Month.ToString()}_{now.Day.ToString()}_log.txt";
            _firstStart = false;
        }
        private static bool CheckFile(string FileName)
        {
            if (!File.Exists(FileName)) File.Create(FileName);
            return true;
        }

        static void WriteEventToLog(string EventDescription)
        {
            string dir = "logs";
            var path = System.Environment.CurrentDirectory + @"\" + dir;
            if (_firstStart) SetCurrentLogFileName();
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            if (!File.Exists(_logFileName)) 
            { 
                using (FileStream fs = new(Path.Combine(path, _logFileName), FileMode.OpenOrCreate)) 
                { 
                    File.Create(Path.Combine(path, _logFileName)); 
                }
            };

            using (StreamWriter sr = new StreamWriter(Path.Combine(path, _logFileName), true))
            {
                sr.WriteLine(EventDescription);
            };
        }

        public static async void WriteToLog(string EventDescription) =>
            await Task.Run(() => WriteEventToLog(EventDescription));
    }
}
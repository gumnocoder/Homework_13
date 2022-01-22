using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Homework_13.ViewModel;

namespace Homework_13.Service
{
    class LogWriter
    {
        private static string _logFileName;
        public static ObservableCollection<string> Logs = new();
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

        static string path = System.Environment.CurrentDirectory + @"\logs";
        static void WriteEventToLog(string EventDescription)
        {
            string dir = "logs";

            if (_firstStart) SetCurrentLogFileName();
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            if (!File.Exists(_logFileName)) 
            { 
                using (FileStream fs = new(Path.Combine(path, _logFileName), FileMode.OpenOrCreate)) 
                { 
                    File.Create(Path.Combine(path, _logFileName)); 
                }
            };
        }

        private static void AddToLogsList(string EventDescription) =>
            Logs.Add(EventDescription);

        public static async void WriteToLog(string EventDescription)
        {
            string AddedEventDescription = $"{DateTime.UtcNow} : {EventDescription} : {MainWindowViewModel.CurrentUser}";
            await Task.Run(() => WriteEventToLog(AddedEventDescription));
            AddToLogsList(AddedEventDescription);

            using (StreamWriter sr = new StreamWriter(Path.Combine(path, _logFileName), true))
            {
                sr.WriteLine(AddedEventDescription);
                sr.Close();
            };
        }     
    }
}

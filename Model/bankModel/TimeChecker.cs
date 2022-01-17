using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Homework_13.Model.bankModel
{
    public sealed class TimeChecker
    {
        public delegate void Timer();

        public event Timer OnTimerSignal;

        private string _settingsFile = "_startTime.ini";

        static TimeChecker _timeCheck = null;

        static TimeChecker() { }

        public static TimeChecker TimeCheck
        {
            get
            {
                if (_timeCheck == null) _timeCheck = new();
                return _timeCheck;
            }
        }

        public string SettingsFile
        {
            get => _settingsFile;
            set => _settingsFile = value;
        }

        private DateTime _startTime;

        public void StartTimeChecking()
        {
            if (_startTime == default)
            {
                _startTime = DateTime.UtcNow;
                SaveCurrentStartTime();
            }

            while (true)
            {
                if (Math.Abs((DateTime.UtcNow - _startTime).TotalHours) >= 6)
                {
                    _startTime.AddHours(6);
                    OnTimerSignal();
                    Thread.Sleep(1000);
                }
                else break;
            }

            Thread.Sleep(21600000);
            StartTimeChecking();
        }

        public async void StartCount() => 
            await Task.Run(() => StartTimeChecking());

        public void LoadPastStartTime()
        {
            FileStream fs;

            if (!File.Exists(SettingsFile))
            {
                fs = new(SettingsFile, FileMode.Create);
                _startTime = default;
                Debug.WriteLine($"Файла не существует!");
            }
            else
            {
                StreamReader sr;
                string tmp;

                using (fs = new FileStream(SettingsFile, FileMode.Open, FileAccess.Read))
                {
                    using (sr = new(fs))
                    { tmp = sr.ReadLine(); }
                }

                if (DateTime.TryParse(tmp, out DateTime tempDate))
                {
                    _startTime = tempDate;
                    Debug.WriteLine($"парсинг прошёл успешно {_startTime}");
                }
                else
                {
                    _startTime = default;
                    Debug.WriteLine($"не удалось распознать содержимое файла {SettingsFile}");
                }
            }
        }

        public void SaveCurrentStartTime()
        {
            FileStream fs;
            StreamWriter sr;

            if (!File.Exists(SettingsFile))
            { fs = new(SettingsFile, FileMode.Create); }

            using (fs = new FileStream(SettingsFile, FileMode.Open, FileAccess.Write))
            {
                using (sr = new(SettingsFile))
                { sr.WriteLine(_startTime); }
            }
        }
    }
}

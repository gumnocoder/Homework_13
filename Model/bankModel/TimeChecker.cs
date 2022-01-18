using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Homework_13.Model.bankModel
{
    /// <summary>
    /// Сигнализирует о необходимости проверки текущей даты, 1 раз в 4 часа
    /// </summary>
    public sealed class TimeChecker
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        static TimeChecker() { }

        #region Поля
        private string _settingsFile = "_startTime.ini";
        private static TimeChecker _timeCheck = null;
        private DateTime _startTime;
        #endregion

        #region Свойства
        /// <summary>
        /// Создаёт экземпляр в случае null или возвращает имеющийся
        /// </summary>
        public static TimeChecker TimeCheck
        {
            get
            {
                if (_timeCheck == null) _timeCheck = new();
                return _timeCheck;
            }
        }

        /// <summary>
        /// Файл, содержащий последнюю сохраненную дату отсчёта
        /// </summary>
        public string SettingsFile
        {
            get => _settingsFile;
            set => _settingsFile = value;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Асинхронно запускает группу методов организующий логику
        /// инициализации события OnTimerSignal
        /// </summary>
        public async void StartCount() =>
            await Task.Run(() => StartTimeChecking());

        /// <summary>
        /// Логика для инициализаии события OnTimerSignal
        /// на которое подписаны методы обязанные отслежить текущую дату
        /// </summary>
        public void StartTimeChecking()
        {
            Debug.WriteLine(_startTime);
            Debug.WriteLine("StartTimeChecking()");

            LoadPastStartTime();

            if (_startTime == default)
            {
                _startTime = DateTime.UtcNow;
                SaveCurrentStartTime();
            }

            /// будет выполняться до тех пор, пока _startTime не будет отличаться
            /// от текущего времени менее чем на 4 часа
            while (true)
            {
                if (Math.Abs((DateTime.UtcNow - _startTime).TotalHours) >= 4)
                {
                    Debug.WriteLine($"{_startTime}");
                    _startTime = _startTime.AddHours(4);
                    Debug.WriteLine($"{_startTime}");
                    Debug.WriteLine("more than 4 hours");
                    SaveCurrentStartTime();
                    OnTimerSignal?.Invoke();
                    Thread.Sleep(1000);
                }
                else break;
            }

            OnTimerSignal?.Invoke();
            Thread.Sleep(21600000);
            StartTimeChecking();
        }

        /// <summary>
        /// Выполняет парсинг _startTime.ini в DateTime
        /// и заносит результат парсинга в переменную _startTime
        /// Или записывает в неё default значение если не удалось распарсить
        /// </summary>
        public void LoadPastStartTime()
        {
            FileStream fs;

            if (!File.Exists(SettingsFile))
            {
                fs = new(SettingsFile, FileMode.Create);
                _startTime = default;
                Debug.WriteLine($"Файла не существует!");
                fs.Close();
            }
            else
            {
                StreamReader sr;
                string tmp;

                using (fs = new FileStream(
                    SettingsFile,
                    FileMode.Open, 
                    FileAccess.Read))
                {
                    using (sr = new(fs))
                    { tmp = sr.ReadLine(); }
                    sr.Close();
                }
                fs.Close();

                if (DateTime.TryParse(tmp, out DateTime tempDate))
                {
                    _startTime = tempDate;
                    Debug.WriteLine($"" +
                        $"парсинг прошёл успешно {_startTime}");
                }
                else
                {
                    _startTime = default;
                    Debug.WriteLine($"" +
                        $"не удалось распознать содержимое " +
                        $"файла {SettingsFile}");
                }
            }
        }

        /// <summary>
        /// Сохранение новой даты в файл настроек _startTime.ini
        /// </summary>
        public void SaveCurrentStartTime()
        {
            FileStream fs;
            StreamWriter sr;

            if (!File.Exists(SettingsFile))
            { fs = new(SettingsFile, FileMode.Create); fs.Close(); }

            using (sr = new(SettingsFile))
            { sr.WriteLine(_startTime); }
            sr.Close();
        }

        #endregion

        public delegate void Timer();

        /// <summary>
        /// событие изменения текущего времени
        /// </summary>
        public event Timer OnTimerSignal;
    }
}

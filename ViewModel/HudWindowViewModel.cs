using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using static System.Windows.SystemParameters;

namespace Homework_13.ViewModel
{
    class HudWindowViewModel : BaseViewModel
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public HudWindowViewModel()
        {
            ListOfMessages.Add(this);

            if (ListOfMessages.Count > 1)
            {
                foreach (var e in ListOfMessages)
                {
                    if (e != this) e.ChangeMessagePosition();
                    Debug.WriteLine(e);
                }
            }

            Task.Run(() => NotificationRemover());
        }

        #region Поля

        bool _positive;
        bool _negative;

        const double 
            _testViewWindowHeight = 100,
            _testViewWindowWidth = 450;

        double
            _top = WorkArea.Bottom - _testViewWindowHeight,
            _left = WorkArea.Right - _testViewWindowWidth;

        static List<HudWindowViewModel> _listOfMessages = new();

        #endregion

        #region Свойства

        /// <summary>
        /// Список активных оповещений
        /// </summary>
        public static List<HudWindowViewModel> ListOfMessages
        {
            get
            {
                if (_listOfMessages == null) _listOfMessages = new();
                return _listOfMessages;
            }
                
            set => _listOfMessages = value;
        }

        /// <summary>
        /// Выравнивание по высоте
        /// </summary>
        public double Top
        {
            get => _top;
            set
            {
                _top = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Выравнивание по ширине
        /// </summary>
        public double Left
        {
            get => _left;
            set
            {
                _left = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Текст оповещения
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// Флаг зеленого индикатора
        /// </summary>
        public bool Positive
        {
            get => _positive;
            set
            {
                _positive = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Флаг красного индикатора
        /// </summary>
        public bool Negative
        {
            get => _negative;
            set
            {
                _negative = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Методы

        /// <summary>
        /// Смещает оповещение вверх на его высоту
        /// </summary>
        public async void ChangeMessagePosition() => 
            await Task.Run(() => Top -= (_testViewWindowHeight/2));


        /// <summary>
        /// Удаляет оповещение из списка активнх по заверщению 4 сек
        /// </summary>
        public async void NotificationRemover()
        {
            await Task.Delay(4000);
            ListOfMessages.Remove(this);
        }

        #endregion
    }
}

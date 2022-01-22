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

            Task.Run(() => TestViewModelRemover());
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

        public static List<HudWindowViewModel> ListOfMessages
        {
            get
            {
                if (_listOfMessages == null) _listOfMessages = new();
                return _listOfMessages;
            }
                
            set => _listOfMessages = value;
        }

        public double Top
        {
            get => _top;
            set
            {
                _top = value;
                OnPropertyChanged();
            }
        }

        public double Left
        {
            get => _left;
            set
            {
                _left = value;
                OnPropertyChanged();
            }
        }

        public string Message { get; set; }

        public bool Positive
        {
            get => _positive;
            set
            {
                _positive = value;
                OnPropertyChanged();
            }
        }

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

        public async void ChangeMessagePosition() => 
            await Task.Run(() => Top -= (_testViewWindowHeight/2));


        public async void TestViewModelRemover()
        {
            await Task.Delay(4000);
            ListOfMessages.Remove(this);
        }

        #endregion
    }
}

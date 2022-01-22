using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using static System.Windows.SystemParameters;

namespace Homework_13.ViewModel
{
    class HudWindowViewModel : BaseViewModel
    {
        static List<HudWindowViewModel> _listOfMessages = new();

        public static List<HudWindowViewModel> ListOfMessages
        {
            get
            {
                if (_listOfMessages == null) _listOfMessages = new();
                return _listOfMessages;
            }
                
            set => _listOfMessages = value;
        }

        const double _testViewWindowHeight = 100;
        const double _testViewWindowWidth = 450;

        double _top = WorkArea.Bottom - _testViewWindowHeight;
        
        public double Top
        {
            get => _top;
            set
            {
                _top = value;
                OnPropertyChanged();
            }
        }

        public double _left = WorkArea.Right - _testViewWindowWidth;

        public double Left
        {
            get => _left;
            set
            {
                _left = value;
                OnPropertyChanged();
            }
        }

        public async void ChangeMessagePosition() => 
            await Task.Run(() => Top -= (_testViewWindowHeight/2));
        
        //private string _sendedMessage { get => Homework_13.Service.test.Message; set => Homework_13.Service.test.Message = value; }
        public string Message { get; set; }
        //public string Message { get => $"{_sendedMessage} : {Homework_13.ViewModel.MainWindowViewModel.CurrentUser} : {DateTime.UtcNow}"; set => Message = value; }

        public async void TestViewModelRemover()
        {
            await Task.Delay(4000);
            ListOfMessages.Remove(this);
        }

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

        //~testViewModel() { ListOfMessages.Remove(this); }
    }
}

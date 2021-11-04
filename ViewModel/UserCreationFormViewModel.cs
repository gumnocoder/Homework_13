using System.Windows;
using System.Windows.Input;

namespace Homework_13.ViewModel
{
    class UserCreationFormViewModel : BaseViewModel
    {
        private string _login;
        private string _name;
        private string _pass = "*****";


        public string Login
        {
            get => _login;
            set
            {
                if (_login == value) return;
                _login = value;
                OnPropertyChanged();
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Pass
        {
            get => _pass;
            set
            {
                if (value == _pass) return;
                _pass = value;
                OnPropertyChanged();
            }
        }

        public string[] Types = new string[] {
            "Адмнистратор",
            "Модератор",
            "Оператор",
            "Специалист по VIP",
            "Специалист по компаниям",
        };
    }
}

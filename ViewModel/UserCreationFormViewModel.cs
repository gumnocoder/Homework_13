using Homework_13.Service.Command;

namespace Homework_13.ViewModel
{
    class UserCreationFormViewModel : BaseViewModel
    {
        private string _pass = "*****";
        private string[] _types = new string[] {
            "администратор",
            "модератор",
            "оператор",
            "специалист по VIP",
            "специалист по компаниям",
        };

        private bool _permissionsVisible = false;

        public bool PermissionsVisible
        {
            get => _permissionsVisible;
            set
            {
                _permissionsVisible = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _showUserPermissions;

        public RelayCommand ShowUserPermissions
        {
            get => _showUserPermissions ??= new(ShowPermissions);
        }
        private void ShowPermissions(object s)
        {
            PermissionsVisible = !PermissionsVisible;
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

        public string[] Types
        {
            get { return _types; }
        }
    }
}

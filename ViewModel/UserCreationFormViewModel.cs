using Homework_13.Service.Command;

namespace Homework_13.ViewModel
{
    /// <summary>
    /// Логика взаимодействия для UserCreationForm.xaml
    /// </summary>
    class UserCreationFormViewModel : BaseViewModel
    {
        #region Поля
        /// <summary>
        /// Пароль
        /// </summary>
        private string _pass = "*****";

        /// <summary>
        /// Типы
        /// </summary>
        private string[] _types = new string[] {
            "администратор",
            "модератор",
            "оператор",
            "специалист по VIP",
            "специалист по компаниям",
        };

        /// <summary>
        /// видимость формы
        /// </summary>
        private bool _permissionsVisible = false;
        #endregion

        #region Свойства

        /// <summary>
        /// Видимость формы с правами пользователя
        /// </summary>
        public bool PermissionsVisible
        {
            get => _permissionsVisible;
            set
            {
                _permissionsVisible = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Пароль
        /// </summary>
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

        /// <summary>
        /// Тип пользователя
        /// </summary>
        public string[] Types
        {
            get { return _types; }
        }

        #endregion

        #region Команда для отображения информации

        private RelayCommand _showUserPermissions;

        public RelayCommand ShowUserPermissions
        {
            get => _showUserPermissions ??= new(ShowPermissions);
        }

        /// <summary>
        /// Отображает / скрывает форму с назначением прав пользователя
        /// </summary>
        /// <param name="s"></param>
        private void ShowPermissions(object s)
        {
            PermissionsVisible = !PermissionsVisible;
        }

        #endregion
    }
}

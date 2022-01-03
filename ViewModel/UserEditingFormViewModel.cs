using Homework_13.Model;

namespace Homework_13.ViewModel
{
    /// <summary>
    /// Логика взаимодействия для UserEditingForm.xaml
    /// </summary>
    class UserEditingFormViewModel : BaseViewModel
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public UserEditingFormViewModel() { }

        /// <summary>
        /// Конструктор с параметром User
        /// </summary>
        /// <param name="user">Пользователь над которым будут
        /// произведены действия</param>
        public UserEditingFormViewModel(User user)
        {
            EditableUser = new(user);

            Pass = user.Pass;
            PassConfirm = Pass;
        }

        #region Поля
        /// <summary>
        /// пароль
        /// </summary>
        private string _pass;
        /// <summary>
        /// подтверждение пароля
        /// </summary>
        private string _passConfirm;
        /// <summary>
        /// пользователь выбранный для редактирования
        /// </summary>
        private static User _editableUser;
        #endregion

        #region Свойства
        /// <summary>
        /// Пароль
        /// </summary>
        public string Pass
        {
            get => _pass;
            set
            {
                _pass = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Подтверждение пароля
        /// </summary>
        public string PassConfirm
        {
            get => _passConfirm;
            set
            {
                _passConfirm = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Редактируемый пользователь
        /// </summary>
        public static User EditableUser
        {
            get => _editableUser;
            set
            {
                _editableUser = value;
            }
        }
        #endregion
    }
}

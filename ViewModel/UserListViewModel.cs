using System.Collections.ObjectModel;
using Homework_13.Model;
using Homework_13.Service;
using Homework_13.Service.Command;

namespace Homework_13.ViewModel
{
    class UserListViewModel : WindowsBasicFuncs
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public UserListViewModel() { }

        #region Поля
        /// <summary>
        /// Выбранный пользователь
        /// </summary>
        private static User _selectedUser;

        /// <summary>
        /// прокси для открытия диалоговых окон
        /// </summary>
        private UserDialogService _dialogService = new();

        #endregion

        #region Свойства

        /// <summary>
        /// Ссылка на лист содержащий
        /// зарегистрированные экземпляры 
        /// пользователей
        /// </summary>
        public static ObservableCollection<User> Users 
        { get => UserList<User>.UsersList; }

        /// <summary>
        /// Пользователь выбранный из списка Users
        /// </summary>
        public static User SelectedUser
        {
            get => _selectedUser;
            set => _selectedUser = value;
        }

        #endregion

        #region Команда редактирования пользователя

        private RelayCommand _editUser;

        public RelayCommand EditUser
        {
            get => _editUser ??= new(UserEditCommand);
        }

        /// <summary>
        /// запускает сценарий редактирования пользователя
        /// </summary>
        /// <param name="s"></param>
        private void UserEditCommand(object s)
        {
            if (SelectedUser != null)
            {
                _dialogService.EditUser(SelectedUser);
            }
        }

        #endregion
    }
}

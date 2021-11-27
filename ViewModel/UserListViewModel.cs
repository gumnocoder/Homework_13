using System;
using System.Collections.ObjectModel;
using Homework_13.Model;
using Homework_13.Service;
using Homework_13.Service.Command;

namespace Homework_13.ViewModel
{
    class UserListViewModel : WindowsBasicFuncs
    {
        private static User _selectedUser;

        private UserDialogService _dialogService = new();
        /// <summary>
        /// Ссылка на лист содержащий
        /// зарегистрированные экземпляры 
        /// пользователей
        /// </summary>
        public static ObservableCollection<User> Users 
        { get => UserList<User>.UsersList; }

        public static User SelectedUser
        {
            get => _selectedUser;
            set => _selectedUser = value;
        }

        public UserListViewModel() { }

        private RelayCommand _editUser;

        public RelayCommand EditUser
        {
            get => _editUser ??= new(UserEditCommand);
        }

        private void UserEditCommand(object s)
        {
            if (SelectedUser != null)
            {
                _dialogService.EditUser(SelectedUser);
            }
        }
    }
}

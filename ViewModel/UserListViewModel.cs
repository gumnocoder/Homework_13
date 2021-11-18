using System.Collections.ObjectModel;
using Homework_13.Model;

namespace Homework_13.ViewModel
{
    class UserListViewModel : WindowsBasicFuncs
    {

        /// <summary>
        /// Ссылка на лист содержащий
        /// зарегистрированные экземпляры 
        /// пользователей
        /// </summary>
        public static ObservableCollection<User> Users 
        { get => UserList<User>.UsersList; }

        public UserListViewModel() { }

    }
}

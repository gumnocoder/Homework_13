using System.Collections.ObjectModel;
using Homework_13.Model;
using Homework_13.Service;

namespace Homework_13.ViewModel
{
    class UserListViewModel : BaseViewModel
    {
        public static ObservableCollection<User> Users { get => UserList<User>.UsersList; } 
        public UserListViewModel()
        {
            DataLoader<User>.LoadFromJson(UserList<User>.UsersList, "users.json");
        }
    }
}

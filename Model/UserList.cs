using System.Collections.ObjectModel;
using Homework_13.Model.Interfaces;

namespace Homework_13.Model
{
    class UserList<T> : ICollectionContainer<T> where T : User
    {
        private static string _usersPath = "users.json";

        public static ObservableCollection<T> UsersList = new();

        public static string UsersPath
        {
            get => _usersPath;
            set => _usersPath = value;
        }
    }
}

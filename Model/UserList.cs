using System.Collections.ObjectModel;
using Homework_13.Model.Interfaces;

namespace Homework_13.Model
{
    public class UserList<T> : ICollectionContainer<T> where T : User
    {
        public static ObservableCollection<T> UsersList;

        public UserList() { }
    }
}

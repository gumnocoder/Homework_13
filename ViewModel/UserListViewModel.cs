using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework_13.Model;

namespace Homework_13.ViewModel
{
    class UserListViewModel
    {
        public ObservableCollection<User> Users { get => UserList<User>.UsersList; }
        public UserListViewModel()
        {
        }
    }
}

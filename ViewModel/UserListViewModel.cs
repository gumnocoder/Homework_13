﻿using System.Collections.ObjectModel;
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
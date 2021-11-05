using System;
using System.Collections;
using System.Collections.Generic;
using Homework_13.Model;
using Homework_13.Model.Interfaces;
using Homework_13.Service;
using Homework_13.Service.Command;

namespace Homework_13.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {
        private string _tittle;
        public string Tittle
        {
            get => _tittle;
            set { _tittle = value; OnPropertyChanged(); }
        }

        public MainWindowViewModel()
        {
            Tittle = "Банк";
            //ListsOperator<User> listOperator = new();
            //listOperator.AddToList(UserList<User>.UsersList, new User("Админ", "admin", "admin", "администратор"));
            //DataSaver<User>.JsonSeralize(UserList<User>.UsersList, "users.json");
            
        }

        private RelayCommand _appExit;
        public RelayCommand AppExit
        {
            get => _appExit ??= new(ExitBtnClick);
        }
        private void ExitBtnClick(object s) => Environment.Exit(0);
    }
}

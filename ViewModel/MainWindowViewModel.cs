using System;
using Homework_13.Model;
using Homework_13.Service;
using Homework_13.Service.Command;
using static Homework_13.Model.bankModel.Bank;

namespace Homework_13.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {
        private UserDialogService _dialogService = new();

        private static User _currentUser = new("11111", "111", "1111", "2");
        public static User CurrentUser
        {
            get => _currentUser;
            set { _currentUser = value; }
        }

        public static string LoginedUser
        {
            get => CurrentUser.ToString();
        }

        private string _tittle;
        public string Tittle
        {
            get => _tittle;
            set { _tittle = value; OnPropertyChanged(); }
        }

        public MainWindowViewModel()
        {
            DataLoader<User>.LoadFromJson(UserList<User>.UsersList, "users.json");
            _dialogService.Edit(CurrentUser);
            Tittle = "Банк";
            new BankSettingsLoader(ThisBank);
            new BankSettingsSaver(ThisBank);
            //ListsOperator<User> listOperator = new();
            //listOperator.AddToList(UserList<User>.UsersList, new User("Админ", "admin", "admin", "администратор"));
            //DataSaver<User>.JsonSeralize(UserList<User>.UsersList, "users.json");

        }

        private RelayCommand _loadBank;
        public RelayCommand LoadBank => _loadBank ??= new(LoadBankBtnClick);
        private void LoadBankBtnClick(object s)
        {
            new BankSettingsLoader(ThisBank);
        }

        private RelayCommand _appExit;
        public RelayCommand AppExit => _appExit ??= new(ExitBtnClick);
        private void ExitBtnClick(object s) => Environment.Exit(0);
    }
}

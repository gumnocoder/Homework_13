using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Homework_13.Model;
using Homework_13.Model.bankModel;
using Homework_13.Service;
using Homework_13.Service.Command;
using static Homework_13.Model.bankModel.Bank;
using static Homework_13.Model.ClientList<Homework_13.Model.Client>;

namespace Homework_13.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {
        private void FillList<T>(
            ObservableCollection<BankAccount> TargetList, 
            ObservableCollection<T> InputList) 
            where T : BankAccount
        {
            foreach (var e in InputList)
            {
                TargetList.Add(e);
                Debug.WriteLine($"{e} added to {TargetList}");
            }
            Debug.WriteLine($"load into {TargetList} complete");
        }
        /// <summary>
        /// конструктор MainWindow
        /// </summary>
        public MainWindowViewModel()
        {
            DataLoader<User>.LoadFromJson(UserList<User>.UsersList, "users.json");
            _dialogService.StartDialogScenario(CurrentUser);
            Tittle = "Банк";

            DataLoader<BankAccount>.LoadingChain();
        }

        #region Поля

        /// <summary>
        /// Экземпляр сервиса управляющего диалоговыми окнами
        /// </summary>
        private UserDialogService _dialogService = new();

        private static User _currentUser = new();

        private string _tittle;

        #endregion

        #region Свойства
        /// <summary>
        /// Авторизированный пользователь
        /// </summary>
        public static User CurrentUser
        {
            get => _currentUser;
            set { _currentUser = value; }
        }

        /// <summary>
        /// заголовок окна
        /// </summary>
        public string Tittle
        {
            get => _tittle;
            set { _tittle = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Проверяет тип пользователя 
        /// для доступа к привелегиям
        /// </summary>
        public bool IsAdmin
        {
            get => CurrentUser.Type == "администратор";
        }
        /// <summary>
        /// Конвертирует информацию о 
        /// авторизированном пользователе в строку
        /// </summary>
        public string LoginedUser
        {
            get 
            { 
                if (CurrentUser != null) return CurrentUser.ToString();
                else return string.Empty; 
            }
        }

        #endregion

        #region Команда загрузки данных в синглтон банка

        private RelayCommand _loadBank;
        public RelayCommand LoadBank => 
            _loadBank ??= new(LoadBankBtnClick);
        private void LoadBankBtnClick(object s)
        {
            new BankSettingsLoader(ThisBank);
        }

        #endregion

        private RelayCommand _saveData;
        public RelayCommand SaveData =>
            _saveData ??= new(SaveAllData);

        private void SaveAllData(object s)
        {
            DataSaver<Client>.DataSaverChain();
        }

        #region Команда выход из приложения

        private RelayCommand _appExit;
        public RelayCommand AppExit =>
            _appExit ??= new(ExitBtnClick);
        private void ExitBtnClick(object s) => 
            Environment.Exit(0);

        #endregion

        #region Команда вызова окна создания пользователя

        private RelayCommand _createUser;

        public RelayCommand CreateUser =>
            _createUser ??= new(CreateUserCommand);
        public void CreateUserCommand(object s)
        {
            _dialogService.StartDialogScenario(new UserCreationFormViewModel());
        }

        #endregion

        #region Команда вызова окна создания клиента

        private RelayCommand _createClient;

        public RelayCommand CreateClient =>
            _createClient ??= new(CreateClientCommand);
        public void CreateClientCommand(object s)
        {
            _dialogService.StartDialogScenario(new ClientCreationFormViewModel());
        }

        #endregion

        #region Команда вызова окна списка пользователей

        private RelayCommand _viewUserDB;

        public RelayCommand ViewUserDB =>
            _viewUserDB ??= new(ShowUserDB);
        public void ShowUserDB(object s)
        {
            _dialogService.StartDialogScenario(new UserListViewModel());
        }

        #endregion

        #region Команда вызова окна списка клиентов

        private RelayCommand _viewClientDB;

        public RelayCommand ViewClientDB =>
            _viewClientDB ??= new(ShowClientDB);
        public void ShowClientDB(object s)
        {
            _dialogService.StartDialogScenario(new ClientListViewModel());
        }

        #endregion

    }
}

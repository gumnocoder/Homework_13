using System;
using Homework_13.Model;
using Homework_13.Service;
using Homework_13.Service.Command;
using Homework_13.View.UserControls;
using static Homework_13.Model.bankModel.Bank;

namespace Homework_13.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {
        /// <summary>
        /// конструктор MainWindow
        /// </summary>
        public MainWindowViewModel()
        {
            DataLoader<User>.LoadFromJson(UserList<User>.UsersList, "users.json");
            //DataSaver<User>.JsonSeralize(UserList<User>.UsersList, "users.json");
            _dialogService.Edit(CurrentUser);
            Tittle = "Банк";
            new BankSettingsLoader(ThisBank);
            new BankSettingsSaver(ThisBank);
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
            _dialogService.Edit(new UserCreationForm());
        }

        #endregion

        #region Команда вызова окна списка пользователей

        private RelayCommand _viewUserDB;

        public RelayCommand ViewUserDB =>
            _viewUserDB ??= new(ShowUserDB);
        public void ShowUserDB(object s)
        {
            _dialogService.Edit(new UserListView());
        }

        #endregion

        #region Команда вызова окна списка клиентов

        private RelayCommand _viewClientDB;

        public RelayCommand ViewClientDB =>
            _viewClientDB ??= new(ShowClientDB);
        public void ShowClientDB(object s)
        {
            _dialogService.Edit(new ClientListView());
        }

        #endregion
    }
}

using System;
using Homework_13.Model;
using Homework_13.Service;
using Homework_13.Service.Command;
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
    }
}

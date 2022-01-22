using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Homework_13.Model;
using Homework_13.Model.bankModel;
using Homework_13.Service;
using Homework_13.Service.Command;
using static Homework_13.Model.bankModel.Bank;
using static Homework_13.Model.bankModel.TimeChecker;

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
            _dialogService.StartDialogScenario(CurrentUser);
            Tittle = "Банк";

            DataLoader<BankAccount>.LoadingChain();

            SubscribeCredits();
            SubscribeDeposits();

            Task.Run(() => TimeCheck.StartTimeChecking());
        }

        /// <summary>
        /// Подписка всех депозитов на сигнал проверки даты
        /// </summary>
        private void SubscribeDeposits()
        {
            if (ThisBank.Deposits == null) ThisBank.Deposits = new();
            else
            {
                foreach (var e in ThisBank.Deposits)
                    TimeCheck.OnTimerSignal += (e as BankDepositAccount).DateComparer;
            }
        }

        /// <summary>
        /// Подписка всех кредитов на сигнал проверки даты
        /// </summary>
        private void SubscribeCredits()
        {
            if (ThisBank.Credits == null) ThisBank.Credits = new();
            else
            {
                foreach (var e in ThisBank.Credits)
                    TimeCheck.OnTimerSignal += (e as BankCreditAccount).DateComparer;
            }
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
        /// Ссылка на LogWriter.Logs содержащий логи текущей сессии
        /// </summary>
        public ObservableCollection<string> LogsList
        {
            get => LogWriter.Logs;
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

        #region Команды

        #region Команда загрузки данных в синглтон банка

        private RelayCommand _loadBank;

        /// <summary>
        /// вызывает загрузку данных банка
        /// </summary>
        public RelayCommand LoadBank => 
            _loadBank ??= new(LoadBankBtnClick);

        /// <summary>
        /// загружает настройки банка
        /// </summary>
        /// <param name="s"></param>
        private void LoadBankBtnClick(object s)
        {
            new BankSettingsLoader(ThisBank);
        }

        #endregion

        #region Команда сохранения данных приложения

        private RelayCommand _saveData;

        /// <summary>
        /// вызывает сохранение данных приложения
        /// </summary>
        public RelayCommand SaveData =>
            _saveData ??= new(SaveAllData);

        /// <summary>
        /// Метод сохранения данных приложения
        /// </summary>
        /// <param name="s"></param>
        private void SaveAllData(object s)
        {
            EventAction += HudViewer.ShowHudWindow;
            DataSaver<Client>.DataSaverChain();
            OnEventAction("Данные успешно сохранены", true, false);
        }

        #endregion

        #region Команда выхода из приложения

        private RelayCommand _appExit;

        /// <summary>
        /// Вызывает метод выхода из приложения
        /// </summary>
        public RelayCommand AppExit =>
            _appExit ??= new(ExitBtnClick);

        /// <summary>
        /// Выход из приложения
        /// </summary>
        /// <param name="s"></param>
        private void ExitBtnClick(object s) => 
            Environment.Exit(0);

        #endregion

        #region Команда вызова окна создания пользователя

        private RelayCommand _createUser;

        /// <summary>
        /// вызывает форму создания пользователя
        /// </summary>
        public RelayCommand CreateUser =>
            _createUser ??= new(CreateUserCommand);

        /// <summary>
        /// Открывает форму для создания пользователя
        /// </summary>
        /// <param name="s"></param>
        public void CreateUserCommand(object s)
        {
            _dialogService.StartDialogScenario(new UserCreationFormViewModel());
        }

        #endregion

        #region Команда вызова окна создания клиента

        private RelayCommand _createClient;

        /// <summary>
        /// Вызывает форму создания клиента
        /// </summary>
        public RelayCommand CreateClient =>
            _createClient ??= new(CreateClientCommand);

        /// <summary>
        /// Открывает форму создания клиента
        /// </summary>
        /// <param name="s"></param>
        public void CreateClientCommand(object s)
        {
            _dialogService.StartDialogScenario(new ClientCreationFormViewModel());
        }

        #endregion

        #region Команда вызова окна списка пользователей

        private RelayCommand _viewUserDB;

        /// <summary>
        /// Вызывает метод открывающий список пользователей
        /// </summary>
        public RelayCommand ViewUserDB =>
            _viewUserDB ??= new(ShowUserDB);

        /// <summary>
        /// Открывает список пользователей
        /// </summary>
        /// <param name="s"></param>
        public void ShowUserDB(object s)
        {
            _dialogService.StartDialogScenario(new UserListViewModel());
        }

        #endregion

        #region Команда вызова окна списка клиентов

        private RelayCommand _viewClientDB;

        /// <summary>
        /// Вызывает метод открывающий список клиентов
        /// </summary>
        public RelayCommand ViewClientDB =>
            _viewClientDB ??= new(ShowClientDB);

        /// <summary>
        /// Открывает список клиентов
        /// </summary>
        /// <param name="s"></param>
        public void ShowClientDB(object s)
        {
            _dialogService.StartDialogScenario(new ClientListViewModel());
        }

        #endregion

        #endregion

    }
}

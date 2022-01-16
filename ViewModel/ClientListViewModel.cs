using System.Collections.ObjectModel;
using System.Diagnostics;
using Homework_13.Model;
using Homework_13.Service;
using Homework_13.Service.Command;
using static Homework_13.ViewModel.ParameterChangingInputVM;
using static Homework_13.Model.bankModel.Bank;
using static Homework_13.Service.InformationDialogService;
using Homework_13.Model.bankModel;
using System.Windows;

namespace Homework_13.ViewModel
{
    /// <summary>
    /// Логика взаимодействия с ClientListView.xaml
    /// </summary>
    class ClientListViewModel : WindowsBasicFuncs
    {
        /// <summary>
        /// Безпараметрический конструктор
        /// </summary>
        public ClientListViewModel() { }

        #region Поля

        /// <summary>
        /// выбранный клиент
        /// </summary>
        private static  Client _selectedClient;

        /// <summary>
        /// прокси для открытия диалоговых окон во избежание нарушения концепции mvvm 
        /// </summary>
        private UserDialogService _dialogService = new();

        /// <summary>
        /// прокси для открытия информационных окон
        /// </summary>
        private InformationDialogService _informDialogService = new();

        /// <summary>
        /// текст в окошке поиска
        /// </summary>
        private string _findText = "поиск";

        /// <summary>
        /// Выбранный счёт
        /// </summary>
        private static BankAccount _selectedAccount;

        /// <summary>
        /// Активность кредита
        /// </summary>
        private bool _creditEnabled;

        #endregion

        #region Свойства

        /// <summary>
        /// Выбранный из списка Clients клиент
        /// </summary>
        public static Client SelectedClient
        {
            get => _selectedClient;
            set
            {
                if (value != null && value.GetType() == typeof(Client))
                {
                    _selectedClient = value;
                }
                else Debug.WriteLine("Передаваемое значение должно иметь тип Client");
            }
        }

        /// <summary>
        /// Передает ссылку на ClientsList для вывода списка клиентов
        /// </summary>
        public static ObservableCollection<Client> Clients
        { get => ClientList<Client>.ClientsList; }

        /// <summary>
        /// Поиск
        /// </summary>
        public string FindText
        {
            get => _findText;
            set
            {
                _findText = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Выбранный счёт
        /// </summary>
        public static BankAccount SelectedAccount
        {
            get => _selectedAccount;
            set => _selectedAccount = value;
        }
        
        /// <summary>
        /// Флаг активности кредита
        /// </summary>
        public bool CreditEnabled
        {
            get
            {
                if (SelectedClient == null) return false;
                return SelectedClient.CreditIsActive;
            }
            set => _creditEnabled = SelectedClient.CreditIsActive;
        }

        #endregion

        #region Команды

        #region Команда редактирования клиента

        private RelayCommand _editClient;

        public RelayCommand EditClient
        {
            get => _editClient ??= new(EditClientCommand);
        }

        /// <summary>
        /// Запускает  сценарий редактирования клиента
        /// </summary>
        private void EditClientCommand(object s)
        {
            if (SelectedClient != null)
            {
                _dialogService.StartDialogScenario(SelectedClient);
            }
        }

        #endregion

        #region Команда открытия иинформационного окна для выбранного клиента

        private RelayCommand _showClientInfo;

        public RelayCommand ShowClientInfo
        {
            get => _showClientInfo ??= new(ShowClientInfoCommand);
        }

        /// <summary>
        /// Запускает  сценарий редактирования клиента
        /// </summary>
        private void ShowClientInfoCommand(object s)
        {
            if (SelectedClient != null)
            {
                _dialogService.StartDialogScenario(new ClientInformationViewModel());
            }
        }

        #endregion

        #region Команда пополнения счёта
        private RelayCommand _makeDepositWindow;

        /// <summary>
        /// пополнение счёта
        /// </summary>
        public RelayCommand MakeDepositWindow => 
            _makeDepositWindow ??= new(MakeDepositOpenWindow);

        /// <summary>
        /// запускает окно пополнения выбранного счёта
        /// </summary>
        /// <param name="s"></param>
        private void MakeDepositOpenWindow(object s)
        {
            SelectedAccount = s as BankAccount;
            Debug.WriteLine(SelectedAccount);
            _dialogService.StartDialogScenario(SelectedAccount);
        }
        #endregion

        #region Просмотр информации по кредиту

        private RelayCommand _checkCreditInfoCommand;
        
        public RelayCommand CheckCreditInfoCommand =>
            _checkCreditInfoCommand ??= new(ShowCreditInfo);
        public void ShowCreditInfo(object s)
        {
            if (SelectedClient != null)
            {
                Debug.WriteLine($"" +
                    $"{SelectedClient.ClientsCreditAccount} " +
                    $"{SelectedClient.ID} " +
                    $"{SelectedClient.CreditAccountID}");
            }
        }

        #endregion

        #region Команда повышения репутации

        private RelayCommand _parameterIncrease;

        /// <summary>
        /// повышение репутации
        /// </summary>
        public RelayCommand ParameterIncrease =>
            _parameterIncrease ??= new(ShowParameterChangerIncrease);

        /// <summary>
        /// запускает окно повышения репутации
        /// </summary>
        /// <param name="s"></param>
        public void ShowParameterChangerIncrease(object s)
        {
            if (SelectedClient != null)
            {
                incr = true;
                _dialogService.StartDialogScenario(new ParameterChangingInputVM());
            }
            else
            {
                ShowError("Выберите клиента!");
            }
        }

        #endregion

        #region Команда понижения репутации

        private RelayCommand _parameterDecrease;

        /// <summary>
        /// уменьшение репутации клиента
        /// </summary>
        public RelayCommand ParameterDecrease =>
            _parameterDecrease ??= new(ShowParameterChangerDecrease);

        /// <summary>
        /// Вызывает диалоговое окно успеньшения репутации клиента
        /// </summary>
        /// <param name="s"></param>
        public void ShowParameterChangerDecrease(object s)
        {
            if (SelectedClient != null)
            {
                decr = true;
                _dialogService.StartDialogScenario(new ParameterChangingInputVM());
            }
            else
            {
                ShowError("Выберите клиента!");
            }
        }
        #endregion

        #region Команда выполнения поиска клиента

        private RelayCommand _search;

        /// <summary>
        /// Поиск
        /// </summary>
        public RelayCommand SearchCommand =>
            _search ??= new(Search);

        /// <summary>
        /// Выполняет поиск клиента по имени или id
        /// </summary>
        /// <param name="s"></param>
        public void Search(object s)
        {
            if ((string)s != string.Empty && (string)s != "поиск")
            {
                if (long.TryParse((string)s, out long tmp))
                {
                    SelectedClient = (SearchEngine.SearchByID(ClientList<Client>.ClientsList, tmp));
                    if (SelectedClient != null)
                    {
                        _dialogService.StartDialogScenario(new ClientInformationViewModel());
                    }
                    else ShowError($"Не удалось найти клиента {(string)s} в базе");
                }
                else
                {
                    SelectedClient = (SearchEngine.SearchByName(ClientList<Client>.ClientsList, (string)s));
                    if (SelectedClient != null)
                    {
                        _dialogService.StartDialogScenario(new ClientInformationViewModel());
                    }
                    else ShowError($"Не удалось найти клиента {(string)s} в базе");
                }
            }
            else ShowError($"пустое значение");
        }

        #endregion

        #region Команда очистки текстовой переменной

        private RelayCommand _clearText;

        /// <summary>
        /// вызывает метод который очищает значение переменной string
        /// </summary>
        public RelayCommand ClearText =>
            _clearText ??= new(ClearSearchText);

        /// <summary>
        /// очищает значение переменнйо string
        /// </summary>
        /// <param name="s"></param>
        private void ClearSearchText(object s) =>
            FindText = string.Empty;
        #endregion

        #endregion

    }
}

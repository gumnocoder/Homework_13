using System.Collections.ObjectModel;
using System.Diagnostics;
using Homework_13.Model;
using Homework_13.Service;
using Homework_13.Service.Command;
using static Homework_13.ViewModel.ParameterChangingInputVM;
using static Homework_13.Model.bankModel.Bank;
using Homework_13.Model.bankModel;

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

        //// TODO
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

        private bool _creditEnabled;
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
                _dialogService.Edit(SelectedClient);
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
                _dialogService.Edit(new ClientInformationViewModel());
            }
        }

        #endregion

        public static BankAccount SelectedAccount
        {
            get;
            set;
        }
        private RelayCommand _makeDepositWindow;

        public RelayCommand MakeDepositWindow => _makeDepositWindow ??= new(MakeDepositOpenWindow);

        private void MakeDepositOpenWindow(object s)
        {
            SelectedAccount = SelectedClient.ClientsDebitAccount;
            _dialogService.Edit(SelectedAccount);
        }


        #region Команда для открытия дебетового счёта

        private RelayCommand _openDebit;

        public RelayCommand OpenDebit
        {
            get => _openDebit ??= new(OpenDebitCommand);
        }

        /// <summary>
        /// открывает дебетовый счёт для выбранного клиента
        /// </summary>
        /// <param name="s"></param>
        private void OpenDebitCommand(object s)
        {
            if (SelectedClient != null && SelectedClient.DebitIsActive == false)
            {
                SelectedClient.ClientsDebitAccount = new(SelectedClient);
                _informDialogService.ShowInformation(
                    "Дебетовый счёт успешно открыт!", 
                    "Операция завершена");
            }
            else if (SelectedClient == null)
            { _informDialogService.ShowError("Выберите клиента!"); }

            else if (SelectedClient != null && SelectedClient.DebitIsActive == true) 
            { _informDialogService.ShowError("Дебетовый счёт уже открыт"); }
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
                    $"{SelectedClient.ClientID} " +
                    $"{SelectedClient.CreditAccountID}");
            }
        }

        #endregion

        #region Команда повышения репутации

        private RelayCommand _parameterIncrease;

        public RelayCommand ParameterIncrease =>
            _parameterIncrease ??= new(ShowParameterChangerIncrease);

        public void ShowParameterChangerIncrease(object s)
        {
            if (SelectedClient != null)
            {
                incr = true;
                _dialogService.Edit(new ParameterChangingInputVM());
            }
            else
            {
                _informDialogService.ShowError("Выберите клиента!");
            }
        }

        #endregion

        #region Команда понижения репутации

        private RelayCommand _parameterDecrease;

        public RelayCommand ParameterDecrease =>
            _parameterDecrease ??= new(ShowParameterChangerDecrease);

        public void ShowParameterChangerDecrease(object s)
        {
            if (SelectedClient != null)
            {
                decr = true;
                _dialogService.Edit(new ParameterChangingInputVM());
            }
            else
            {
                _informDialogService.ShowError("Выберите клиента!");
            }
        }

        #endregion
    }
}

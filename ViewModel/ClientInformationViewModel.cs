using System.Diagnostics;
using System.Windows;
using Homework_13.Model;
using Homework_13.Model.bankModel;
using Homework_13.Service;
using Homework_13.Service.Command;
using static Homework_13.ViewModel.ClientListViewModel;

namespace Homework_13.ViewModel
{
    class ClientInformationViewModel : BaseViewModel
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ClientInformationViewModel()
        {
            if (SelectedClient != null)
            {
                ID = SelectedClient.ID.ToString();
                Reputation = SelectedClient.Reputation.ToString();
            }
            else Debug.WriteLine("SelectedClient было null");
        }

        #region Поля
        /// <summary>
        /// прокси для открытия диалоговых окон во избежание нарушения концепции mvvm 
        /// </summary>
        private UserDialogService _dialogService = new();

        /// <summary>
        /// Сумма на депозитном счёте клиента
        /// </summary>
        private string _depositAmount;
        #endregion

        #region Свойства

        /// <summary>
        /// Состояние депозитного счёта выбранного клиента
        /// </summary>
        public string DepositAmount
        {
            get => _depositAmount;
            set
            {
                _depositAmount = SelectedClient.ClientsDepositAccount.AccountAmount.ToString();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Идентификатор клиента
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Рейтинг клиента
        /// </summary>
        public string Reputation { get; set; }

        /// <summary>
        /// Возвращает индентификатор дебетового счёта в string
        /// </summary>
        public string DebitID
        { get => SelectedClient.DebitIsActive ? SelectedClient.DebitAccountID.ToString() : string.Empty; }

        /// <summary>
        /// Возвращает индентификатор депозитного счёта в string
        /// </summary>
        public string DepositID
        { get => SelectedClient.DepositIsActive ? SelectedClient.DepositAccountID.ToString() : string.Empty; }

        /// <summary>
        /// Возвращает индентификатор кредитного счёта в string
        /// </summary>
        public string CreditID
        { get => SelectedClient.CreditIsActive ? SelectedClient.CreditAccountID.ToString() : string.Empty; }

        /// <summary>
        /// Конвертирует процент депозита и возвращает строку
        /// </summary>
        public string DepositPercent
        {
            get
            {
                if (SelectedClient.DepositIsActive)
                    return string.Format("{0:f1}%", SelectedClient.ClientsDepositAccount.Percent);
                return string.Empty;
            }
        }

        /// <summary>
        /// Конвертирует процент кредита и возвращает строку
        /// </summary>
        public string CreditPercent
        {
            get
            {
                if (SelectedClient.CreditIsActive)
                    return string.Format("{0:f1}%", SelectedClient.ClientsCreditAccount.Percent);
                return string.Empty;
            }
        }

        /// <summary>
        /// Активность дебетового счёта в строковом формате
        /// </summary>
        public string DebitActivity
        {
            get
            {
                if (SelectedClient.DebitIsActive) return "Активен";
                return "Не активен";
            }
        }

        /// <summary>
        /// Активность кредитного счёта в строковом формате
        /// </summary>
        public string CreditActivity
        {
            get
            {
                if (SelectedClient.CreditIsActive) return "Активен";
                return "Не активен";
            }
        }

        /// <summary>
        /// Активность депозитного счёта в строковом формате
        /// </summary>
        public string DepositActivity
        {
            get
            {
                if (SelectedClient.DepositIsActive) return "Активен";
                return "Не активен";
            }
        }

        /// <summary>
        /// предоставляет доступ к SelectedClient из ClientListViewModel
        /// </summary>
        public static Client Selected 
        { 
            get => SelectedClient;
            set => Selected = value;
        }

        #endregion

        #region Команда копирования в буфер обмена

        private RelayCommand _copy;

        /// <summary>
        /// Копирует значение string в буфер обмена
        /// </summary>
        public RelayCommand Copy => _copy ??= new(CopyText);

        /// <summary>
        /// Преобразует обёект в сроку и помещает её в буфер обмена
        /// </summary>
        /// <param name="text"></param>
        private void CopyText(object text)
        {
            Clipboard.Clear();
            Clipboard.SetText((string)text);
            MessageBox.Show(
                "Значение скопировано в буфер обмена.",
                "Выполнено",
                MessageBoxButton.OK);
        }

        #endregion

        #region Команда отрывает окно пополнения счёта
        private RelayCommand _makeDepositWindow;

        /// <summary>
        /// Запускает метод открытия окна пополнения счёта
        /// </summary>
        public RelayCommand MakeDepositWindow => 
            _makeDepositWindow ??= new(MakeDepositOpenWindow);

        /// <summary>
        /// Открывает окно пополнения счёта
        /// </summary>
        /// <param name="s"></param>
        private void MakeDepositOpenWindow(object s)
        {
            _dialogService.StartDialogScenario(s as BankAccount);
        }
        #endregion

        #region Команда открытия окна транзакции
        private RelayCommand _showSenderWindow;
        /// <summary>
        /// Запускает метод открывающий окно транзакции
        /// </summary>
        public RelayCommand ShowSenderWindow =>  
            _showSenderWindow ??= new(SendMoneyOpenWindow);

        /// <summary>
        /// Открывает окно транзакции
        /// </summary>
        /// <param name="s"></param>
        private void SendMoneyOpenWindow(object s)
        {
            _dialogService.SendMoney(s as BankAccount);
        }
        #endregion

        #region Команда открытия окна расширения кредитного счёта
        private RelayCommand _extendCreditWindow;
        /// <summary>
        /// Запускает метод открывающий окно расширения кредита
        /// </summary>
        public RelayCommand ExtendCreditWindow => 
            _extendCreditWindow ??= new(OpenExtendWindow);

        /// <summary>
        /// открывает окно расширения кредита
        /// </summary>
        /// <param name="s"></param>
        private void OpenExtendWindow(object s)
        {
            _dialogService.ExtendCredit(s as BankCreditAccount);
        }
        #endregion
    }
}

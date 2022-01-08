using System.Diagnostics;
using Homework_13.Model;
using static Homework_13.Model.bankModel.CreditPercentSetter;
using static Homework_13.Model.bankModel.DepositPercentSetter;

namespace Homework_13.ViewModel
{
    class AccountOpeningViewModel : BaseViewModel
    {
        #region Конструкторы
        /// <summary>
        /// Основной конструктор
        /// </summary>
        /// <param name="client"></param>
        /// <param name="Deposit"></param>
        public AccountOpeningViewModel(Client client, bool Deposit)
        {
            AccountOpeningViewModel.Deposit = Deposit;
            if (client != null)
            {
                BankClient = client;
                if (!Deposit) PersonalPercent = SetCreditPercent(client);
                else PersonalPercent = SetDepositPercent(client);
            }
            else Debug.WriteLine("client == null");
        }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public AccountOpeningViewModel() { }
        #endregion

        #region Поля
        private static bool _deposit = false;
        private Client _client;
        private static double _personalPercent;
        #endregion

        #region Свойства

        /// <summary>
        /// Клиент для которого открывается счёт
        /// </summary>
        public Client BankClient
        {
            get => _client;
            set => _client = value;
        }

        /// <summary>
        /// Флаг сигнализирующий о том что открывается депозитный счёт.
        /// в случае false будет запущен сценарий кредитного счёта
        /// </summary>
        public static bool Deposit
        {
            get => _deposit;
            set
            {
                _deposit = value;
            }
        }

        /// <summary>
        /// Флаг кредитного счёта
        /// </summary>
        public bool Credit
        {
            get => !Deposit;
        }

        /// <summary>
        /// Процент для конкретного счёта
        /// </summary>
        public static double PersonalPercent
        {
            get => _personalPercent;
            set => _personalPercent = value;
        }

        #endregion
    }
}

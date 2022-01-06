using System.Diagnostics;
using Homework_13.Model;
using Homework_13.Model.bankModel;
using static Homework_13.Model.bankModel.CreditPercentSetter;
using static Homework_13.Model.bankModel.DepositPercentSetter;

namespace Homework_13.ViewModel
{
    class AccountOpeningViewModel : BaseViewModel
    {
        #region Конструкторы
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
        public AccountOpeningViewModel() { }
        #endregion

        #region Поля
        private static bool _deposit = false;
        private Client _client;
        private static double _personalPercent;
        #endregion

        #region Свойства

        public Client BankClient
        {
            get => _client;
            set => _client = value;
        }
        public static bool Deposit
        {
            get => _deposit;
            set
            {
                _deposit = value;
            }
        }

        public bool Credit
        {
            get => !Deposit;
        }

        public static double PersonalPercent
        {
            get => _personalPercent;
            set => _personalPercent = value;
        }

        #endregion
    }
}

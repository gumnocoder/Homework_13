using System.Diagnostics;
using Homework_13.Model.Interfaces;
using static Homework_13.Model.bankModel.Bank;

namespace Homework_13.Model.bankModel
{
    class AccountCreditHandler : ICommandAction
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="client">Клиент, владелец кредитного счёта</param>
        public AccountCreditHandler(Client client, BankCreditAccount account)
        {
            _client = client;
            _account = account;
            onPayment += PaymentSignal;
        }

        #region Поля
        private Client _client;
        private BankCreditAccount _account;
        private bool _executed = false;
        #endregion

        #region Свойства
        public bool Executed
        {
            get => _executed;
            set => _executed = value;
        }
        #endregion

        #region Методы
        public void PaymentSignal()
        {
            Debug.WriteLine("Была совершена оплата");
        }

        /// <summary>
        /// расширение кредитного счёта
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool ExtendCreditAmount(long amount)
        {
            Executed = false;
            if (_client.Reputation > 7)
            {
                _account.AccountAmount -= amount;
                new ReputationDecreaser(_client, 2).Execute();
                Execute();
                return true;
            }
            Debug.WriteLine("не удалось расширить кредит, низкая " +
                "репутация клиента. Требуемый показатель - от 8");
            return false;
        }

        /// <summary>
        /// Погасить долг и закрыть счёт
        /// </summary>
        public void PayOff()
        {
            Executed = false;
            _account.AccountAmount = 0;
            foreach (var e in ThisBank.Credits)
            {
                if (e == _client.ClientsCreditAccount)
                {
                    ThisBank.Credits.Remove(e);
                    break;
                }
            }
            _client.CreditIsActive = false;
            _client.ClientsCreditAccount = null;
            new ReputationIncreaser(_client, 2).Execute();
            Execute();
        }

        /// <summary>
        /// Выполняет очередной платёж
        /// </summary>
        /// <param name="PayAmount"></param>
        public void Pay(long PayAmount)
        {
            Executed = false;
            _account.AccountAmount += PayAmount;
            onPayment();
            Execute();
        }
        public delegate void PaymentEvent();
        public event PaymentEvent onPayment;

        /// <summary>
        /// Выполнить если кредит не был погашен, но срок вышел
        /// </summary>
        public void OnExpiredWithoutPayment()
        {
            if (_account.Expired() && _account.AccountAmount < 0)
            {
                new ReputationDecreaser(_client, _client.Reputation).Execute();
                _client.AccountsFreezed = true;
            }
        }

        /// <summary>
        /// Выполнить если кредит был погашен, но не закрыт
        /// </summary>
        public void OnExpiredWithCompletePayment()
        {
            if (_account.Expired() && _account.AccountAmount >= 0)
            {
                PayOff();
            }
        }
        public void Execute() =>
            Executed = true;
        #endregion
    }
}

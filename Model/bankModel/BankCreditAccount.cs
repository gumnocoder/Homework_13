using System;
using System.Diagnostics;
using Homework_13.Model.bankModel.interfaces;
using Homework_13.Service;
using static Homework_13.Model.bankModel.Bank;
using static Homework_13.Model.bankModel.TimeChecker;

namespace Homework_13.Model.bankModel
{
    /// <summary>
    /// Кредитный банковский счёт
    /// </summary>
    class BankCreditAccount : BankPercentableAccount
    {
        #region Конструктор
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="client"></param>
        /// <param name="PersonalPercent"></param>
        /// <param name="CreditAmount"></param>
        /// <param name="Expiration"></param>
        public BankCreditAccount(
            Client client, 
            double PersonalPercent,
            long CreditAmount,
            int Expiration)
        {
            if (!client.CreditIsActive && client.Reputation > 5)
            {
                TimeCheck.OnTimerSignal += this.DateComparer;

                AccountAmount = -CreditAmount;
                SetId();
                client.CreditIsActive = true;

                Percent = PersonalPercent;
                if (Percent < CreditPercentSetter.minPercent) 
                { Percent = CreditPercentSetter.minPercent; }

                ActivationDate = DateTime.UtcNow;
                NextPaymentDay = ActivationDate.AddDays(
                    DateTime.DaysInMonth(
                        ActivationDate.Year,
                        ActivationDate.Month));

                this.Expiration = Expiration;
                AddLinkToAccountInBank();
                client.CreditAccountID = ID;
                client.ClientsCreditAccount = (BankCreditAccount)client.ba<BankCreditAccount>(
                    ref ThisBank.credits, 
                    client.CreditAccountID);
                _clientID = client.ID;
            }
            else Debug.WriteLine("кредит недоступен для этой персоны");
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public BankCreditAccount() { }

        #endregion

        #region Поля

        private bool _paymentFlag = false;
        private long _clientID;
        public const int minExpiration = 6;

        public bool PaymentFlag
        {
            get => _paymentFlag;
            set
            {
                _paymentFlag = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Методы

        /// <summary>
        /// выполняет ряд методов по начислению процентов,
        /// или закрывает счёт и переводит средства на дебетовый 
        /// счёт если срок депозита вышел
        /// </summary>
        public override void TurnMonth()
        {
            if (!Expired())
            {
                AddPercents();
                --Expiration;
                CalculateNextPaymentDay();
                if (_paymentFlag) _paymentFlag = false;
                else new AccountCreditHandler(
                    SearchEngine.SearchByID(
                        ClientList<Client>.ClientsList,
                        _clientID), this).OnExpiredMonthlyPayment();
            }
            else
            {
                if (AccountAmount < 0)
                {
                    new AccountCreditHandler(
                        SearchEngine.SearchByID(
                            ClientList<Client>.ClientsList,
                            _clientID), this).OnExpiredWithoutPayment();
                }
                else
                {
                    new AccountCreditHandler(
                        SearchEngine.SearchByID(
                            ClientList<Client>.ClientsList,
                            _clientID), this).OnExpiredWithCompletePayment();
                }
            }
        }

        /// <summary>
        /// Назначает новый ID
        /// </summary>
        public override void SetId()
        {
            ID = ++ThisBank.CurrentCreditID;
            Debug.WriteLine($"account ID - {ID}");
        }

        /// <summary>
        /// Добавляет экземпляр кредитного счёта в 
        /// соответствующий список в синглтоне ThisBank
        /// </summary>
        public override void AddLinkToAccountInBank()
        {
            ThisBank.Credits.Add(this);
            Debug.WriteLine($"{this} added to bank credits");
        }

        /// <summary>
        /// Возвращает информацию о кредитном счёте
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"кредитный счёт " +
                $"{AccountAmount}$ " +
                $"ID - {ID} " +
                $"{ActivationDate} " +
                $"{Percent}%";
        }

        ~BankCreditAccount() { Debug.WriteLine("вызван деструтор класса BankCreditAccount"); }
        #endregion
    }
}

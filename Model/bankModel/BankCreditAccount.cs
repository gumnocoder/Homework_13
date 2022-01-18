using System;
using System.Diagnostics;
using Homework_13.Model.bankModel.interfaces;
using static Homework_13.Model.bankModel.Bank;
using static Homework_13.Model.bankModel.TimeChecker;

namespace Homework_13.Model.bankModel
{
    /// <summary>
    /// Кредитный банковский счёт
    /// </summary>
    class BankCreditAccount : BankAccount, IPercentContainer, IExpiring
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
                AccountAmount = -CreditAmount;
                SetId();
                client.CreditIsActive = true;

                Percent = PersonalPercent;
                if (Percent < CreditPercentSetter.minPercent) 
                { Percent = CreditPercentSetter.minPercent; }

                _activationDate = DateTime.UtcNow;
                NextPaymentDay = _activationDate.AddDays(
                    DateTime.DaysInMonth(
                        _activationDate.Year,
                        _activationDate.Month));

                this.Expiration = Expiration;
                AddLinkToAccountInBank();
                client.CreditAccountID = ID;
                client.ClientsCreditAccount = (BankCreditAccount)client.ba<BankCreditAccount>(
                    ref ThisBank.credits, 
                    client.CreditAccountID);
            }
            else Debug.WriteLine("кредит недоступен для этой персоны");
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public BankCreditAccount() { }

        #endregion

        #region Поля

        private double _percent;
        private int _expiration;
        public const int minExpiration = 6;
        private DateTime _activationDate;
        private DateTime _nextPaymentDay;


        #endregion

        #region Свойства
        /// <summary>
        /// Проентная ставка кредита
        /// </summary>
        public double Percent 
        {
            get => _percent; 
            set =>_percent = value;
        }

        /// <summary>
        /// Срок кредита
        /// </summary>
        public int Expiration
        { 
            get => _expiration;
            set => _expiration = value;
        }

        /// <summary>
        /// Дата выдачи кредита
        /// </summary>
        public DateTime ActivationDate 
        { 
            get => _activationDate; 
            set => _activationDate = value; 
        }

        /// <summary>
        /// Дата следующего платежа по кредиту
        /// </summary>
        public DateTime NextPaymentDay
        {
            get => _nextPaymentDay;
            set
            {
                _nextPaymentDay = value;
                OnPropertyChanged();
            }
         }

        /// <summary>
        /// Расчитывает дату следующего платежа
        /// </summary>
        public void CalculateNextPaymentDay()
        {
            NextPaymentDay = NextPaymentDay.AddDays(
                DateTime.DaysInMonth(
                    NextPaymentDay.Year,
                    NextPaymentDay.Month));
        }


        #endregion

        #region Методы
        /// <summary>
        /// Назначает новый ID
        /// </summary>
        public override void SetId()
        {
            Debug.WriteLine($"ID - {ThisBank.CurrentCreditID}");
            ThisBank.CurrentCreditID++;
            Debug.WriteLine($"ID - {ThisBank.CurrentCreditID}");
            ID = ThisBank.CurrentCreditID;
            Debug.WriteLine($"account ID - {ID}");
        }

        /// <summary>
        /// Проверяет истёк ли срок кредита
        /// </summary>
        /// <returns></returns>
        public bool Expired() => Expiration == 0;

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
            return $"{(typeof(BankCreditAccount).ToString())} " +
                $"{AccountAmount}$ " +
                $"ID - {ID} " +
                $"{ActivationDate} " +
                $"{Percent}%";
        }
        #endregion
    }
}

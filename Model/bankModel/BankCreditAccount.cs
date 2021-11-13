using System;
using System.Diagnostics;
using Homework_13.Model.bankModel.interfaces;
using static Homework_13.Model.bankModel.Bank;

namespace Homework_13.Model.bankModel
{
    class BankCreditAccount : BankAccount, IPercentContainer, IExpiring
    {
        #region Конструктор
        public BankCreditAccount(long CreditAmount, Client client)
        {
            if (!client.CreditIsActive && client.Reputation > 5)
            {
                AccountAmount = -CreditAmount;
                SetId();
                client.CreditIsActive = true;
                client.ClientsCreditAccount = this;
                Percent = SetPercent(client);
                _activationDate = DateTime.Now;
            }
            else Debug.WriteLine("кредит недоступен для этой персоны");
        }

        #endregion

        #region Поля

        private double _percent;
        private int _expiration;
        private const double _minPercent = 10;
        private const int _minExpiration = 6;
        private DateTime _activationDate;

        #endregion

        #region Свойства
        public double Percent 
        {
            get => _percent; 
            set =>_percent = value;
        }
        public int Expiration
        { 
            get => _expiration; 
            set 
            { 
                if (value > _minExpiration) _expiration = value;
            } 
        }
        public DateTime ActivationDate { get => _activationDate; }
        public int ExpirationDuration { get => GetTotalMonthsCount(); }

        #endregion

        /// <summary>
        /// Назначает новый ID
        /// </summary>
        public override void SetId()
        {
            ID = ++ThisBank.CurrentCreditID;
        }

        public bool Expired()
        {
            if (Expiration == 0) return true;
            else return false;
        }

        public double SetPercent(Client client)
        {
            if (client.Reputation == 10) return _minPercent;
            int reputation = client.Reputation - 5;
            return 16.0 - (double)(reputation * 2);
        }

        public int GetTotalMonthsCount()
        {
            DateTime now = DateTime.Now;
            DateTime start = ActivationDate;

            return (int)(((now.Year - start.Year) * 12) +
                now.Month - start.Month +
                (start.Day >= now.Day - 1 ? 0 : -1) +
                ((start.Day == 1 && DateTime.DaysInMonth(now.Year, now.Month) == now.Day) ? 1 : 0));
        }
    }
}

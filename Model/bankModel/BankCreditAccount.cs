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
            }
            else Debug.WriteLine("кредит недоступен для этой персоны");
        }

        #endregion

        #region Поля

        private double _percent;
        private int _expiration;
        private const double _minPercent = 10;
        private const int _minExpiration = 6;

        #endregion

        #region Свойства
        public double Percent 
        {
            get => _percent; 
            set =>_percent = value;
        }
        public int Expiration { get => _expiration; set { if (value > _minExpiration) _expiration = value; } }

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
            int reputation = client.Reputation - 5;
            return 16.0 - (double)(reputation * 2);
        }
    }
}

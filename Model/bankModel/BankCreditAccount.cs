using System;
using System.Diagnostics;
using Homework_13.Model.bankModel.interfaces;
using static Homework_13.Model.bankModel.Bank;

namespace Homework_13.Model.bankModel
{
    class BankCreditAccount : BankAccount, IPercentContainer
    {
        #region Конструктор
        public BankCreditAccount(long CreditAmount, Client client)
        {
            if (!client.CreditIsActive)
            {
                AccountAmount = -CreditAmount;
                SetId();
                client.CreditIsActive = true;
                client.ClientsCreditAccount = this;
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
        public double Percent { get => _percent; set { if (value > _minPercent) _percent = value; } }
        public int Expiration { get => _expiration; set { if (value > _minExpiration) _expiration = value; } }

        #endregion

        /// <summary>
        /// Назначает новый ID
        /// </summary>
        public override void SetId()
        {
            ID = ++ThisBank.CurrentCreditID;
        }
    }
}

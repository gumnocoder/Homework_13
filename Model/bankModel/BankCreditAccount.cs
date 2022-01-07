using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Homework_13.Model.bankModel.interfaces;
using static Homework_13.Model.bankModel.Bank;
using static Homework_13.ViewModel.ClientListViewModel;

namespace Homework_13.Model.bankModel
{
    class BankCreditAccount : BankAccount, IPercentContainer, IExpiring
    {
        #region Конструктор

        public BankCreditAccount(Client client, double PersonalPercent, long CreditAmount)
        {
            if (!client.CreditIsActive && client.Reputation > 5)
            {
                AccountAmount = -CreditAmount;
                SetId();
                client.CreditIsActive = true;
                Percent = PersonalPercent;
                _activationDate = DateTime.Now;
                AddLinkToAccountInBank();
                client.CreditAccountID = ID;
                client.ClientsCreditAccount = (BankCreditAccount)client.ba<BankCreditAccount>(ref ThisBank.credits, client.CreditAccountID);
            }
            else Debug.WriteLine("кредит недоступен для этой персоны");
        }

        public BankCreditAccount() { }

        #endregion

        #region Поля

        private double _percent;
        private int _expiration;
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
            Debug.WriteLine($"ID - {ThisBank.CurrentCreditID}");
            ThisBank.CurrentCreditID++;
            Debug.WriteLine($"ID - {ThisBank.CurrentCreditID}");
            ID = ThisBank.CurrentCreditID;
            Debug.WriteLine($"account ID - {ID}");
        }

        public bool Expired() => GetTotalMonthsCount() == 0;

        public int GetTotalMonthsCount()
        {
            DateTime now = DateTime.Now;
            DateTime start = ActivationDate;

            return (int)(((now.Year - start.Year) * 12) +
                now.Month - start.Month +
                (start.Day >= now.Day - 1 ? 0 : -1) +
                ((start.Day == 1 && DateTime.DaysInMonth(now.Year, now.Month) == now.Day) ? 1 : 0));
        }

        public override void AddLinkToAccountInBank()
        {
            ThisBank.Credits.Add(this);
            Debug.WriteLine($"{this} added to bank credits");
        }

        public override string ToString()
        {
            return $"{(typeof(BankCreditAccount).ToString())} {AccountAmount}$ ID - {ID} {ActivationDate} {Percent}%";
        }
    }
}

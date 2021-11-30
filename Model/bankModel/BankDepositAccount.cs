using System;
using Homework_13.Model.bankModel.interfaces;
using static Homework_13.Model.bankModel.Bank;

namespace Homework_13.Model.bankModel
{
    class BankDepositAccount : BankAccount, IPercentContainer, IExpiring
    {
        public BankDepositAccount(Client client, long DepositStartAmount)
        {
            if (!client.DepositIsActive)
            {
                new ReputationIncreaser(client, 3);
                AccountAmount += DepositStartAmount;
                SetId();
                client.DepositIsActive = true;
                client.ClientsDepositAccount = this;
                Percent = SetPercent(client);
                _activationDate = DateTime.Now;
            }
        }

        public BankDepositAccount() { }

        private double _percent;
        private int _expiration;
        private const double  _maxPercent = 12;
        private const int _minExpiration = 12;
        private DateTime _activationDate;
        public double Percent 
        { 
            get => _percent;
            set { if (value <= _maxPercent) _percent = value;
                else _percent = 5; 
            } 
        }
        public int Expiration 
        { 
            get => _expiration; 
            set 
            {
                if (value < _minExpiration) _expiration = 12;
                else
                {
                    if (_expiration == 0) { _expiration = 0; return; }
                    _expiration = value;
                }
            }
        }
        public DateTime ActivationDate { get => _activationDate; }

        public int ExpirationDuration { get => GetTotalMonthsCount(); }

        public override void SetId() => ID = ++ThisBank.CurrentDepositID;

        public bool Expired()
        {
            if (Expiration == 0) return true;
            else return false;
        }

        public double SetPercent(Client client)
        {
            if (client.Reputation == 10) return _maxPercent;
            double clientReputation = (double)client.Reputation;
            return _maxPercent / 10.0 * clientReputation;
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

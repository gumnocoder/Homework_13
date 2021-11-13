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
            }
        }

        private double _percent;
        private int _expiration;
        private const double  _maxPercent = 12;
        private const int _minExpiration = 12;
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
    }
}

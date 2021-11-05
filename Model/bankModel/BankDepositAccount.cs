using Homework_13.Model.bankModel.interfaces;
using static Homework_13.Model.bankModel.Bank;

namespace Homework_13.Model.bankModel
{
    class BankDepositAccount : BankAccount, IPercentContainer, IExpiring
    {
        private double _percent;
        private int _expiration;
        private const double  _maxPercent = 5;
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

        public BankDepositAccount(Client client, long DepositStartAmount)
        {
            if (!client.DepositIsActive)
            {
                new ReputationIncreaser(client, 3);
                AccountAmount += DepositStartAmount;
                SetId();
                client.DepositIsActive = true;
                client.ClientsDepositAccount = this;
            }
        }
        public override void SetId() => ID = ++ThisBank.CurrentDepositID;

        public bool Expired()
        {
            if (Expiration == 0) return true;
            else return false;
        }
    }
}

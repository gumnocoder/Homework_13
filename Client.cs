using Homework_13.Model;
using Homework_13.Model.bankModel;

namespace Homework_13
{
    class Client : Person
    {
        private bool _creditIsActive;
        private BankCreditAccount _clientsCreditAccount;

        public bool CreditIsActive
        {
            get => _creditIsActive; 
            set 
            { 
                _creditIsActive = value; 
                OnPropertyChanged(); 
            }
        }
        public BankCreditAccount ClientsCreditAccount
        {
            get => _clientsCreditAccount;
            set
            {
                _clientsCreditAccount = value;
                OnPropertyChanged();
            }
        }
    }
}

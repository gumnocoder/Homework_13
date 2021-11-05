using Homework_13.Model.bankModel;
using static Homework_13.Model.bankModel.Bank;

namespace Homework_13.Model
{
    class Client : Person
    {
        private bool _creditIsActive;
        private bool _debitIsActive;
        private bool _depositIsActive;
        private bool _accountFreezed = false;
        private BankCreditAccount _clientsCreditAccount;
        private BankDebitAccount _clientsDebitAccount;
        private BankDepositAccount _clientsDepositAccount;
        private string _type;
        private int _reputation;
        private long _clientId;



        public bool CreditIsActive
        {
            get => _creditIsActive; 
            set 
            { 
                _creditIsActive = value; 
                OnPropertyChanged(); 
            }
        }

        public bool DebitIsActive
        {
            get => _debitIsActive;
            set
            {
                _debitIsActive = value;
                OnPropertyChanged();
            }
        }

        public bool DepositIsActive
        {
            get => _depositIsActive;
            set
            {
                _depositIsActive = value;
                OnPropertyChanged();
            }
        }

        public bool AccountFreezed
        {
            get => _accountFreezed;
            set
            {
                _accountFreezed = value;
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

        public BankDebitAccount ClientsDebitAccount
        {
            get => _clientsDebitAccount;
            set
            {
                _clientsDebitAccount = value;
                OnPropertyChanged();
            }
        }

        public BankDepositAccount ClientsDepositAccount
        {
            get => _clientsDepositAccount;
            set
            {
                _clientsDepositAccount = value;
                OnPropertyChanged();
            }
        }
        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged();
            }
        }

        public int Reputation
        {
            get => _reputation;
            set
            {
                if (value < 0) _reputation = 0;
                else if (value > 10) _reputation = 10;
                else _reputation = value;
                OnPropertyChanged();
            }
        }

        public long ClientID
        {
            get => _clientId;
            protected set
            {
                _clientId = value;
                OnPropertyChanged();
            }
        }
        public void SetID()
        {
            ClientID = ++ThisBank.currentClientID;
        }
        public Client(string Name, string Type = "Частный клиент", int Reputation = 6)
        {
            SetID();
            this.Name = Name;
            this.Type = Type;
            this.Reputation = Reputation;
        }

        #region TODO
        //добавить депозитный счёт
        #endregion

        public override string ToString()
        {
            return $"{ClientID} {Name} {Reputation}";
        }
    }
}

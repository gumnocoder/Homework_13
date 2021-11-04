using Homework_13.Model;
using Homework_13.Model.bankModel;
using static Homework_13.Model.bankModel.Bank;

namespace Homework_13
{
    class Client : Person
    {
        private bool _creditIsActive;
        private bool _debitIsActive = false;
        private BankCreditAccount _clientsCreditAccount;
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
        public BankCreditAccount ClientsCreditAccount
        {
            get => _clientsCreditAccount;
            set
            {
                _clientsCreditAccount = value;
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
                _reputation = value;
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
        public Client(string Name, string Type = "Частный клиент", int Reputation = 5)
        {
            SetID();
            this.Name = Name;
            this.Type = Type;
            this.Reputation = Reputation;
        }

        #region TODO
        //добавить дебетовый счёт и депозитный счёт
        #endregion

        public override string ToString()
        {
            return $"{ClientID} {Name} {Reputation}";
        }

    }
}

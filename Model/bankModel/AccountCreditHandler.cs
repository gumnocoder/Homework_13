using Homework_13.Model.Interfaces;

namespace Homework_13.Model.bankModel
{
    class AccountCreditHandler<T> : ICommandAction 
        where T : BankCreditAccount
    {
        private Client _client;
        private T _account;
        private bool _executed = false;


        public bool Executed
        {
            get => _executed;
            set => _executed = value;
        }

        public AccountCreditHandler(Client client)
        {
            _client = client;
            _account = (T)client.ClientsCreditAccount;
        }
        public void Execute() =>Executed = true;
    }
}

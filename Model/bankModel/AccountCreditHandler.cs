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

        public void ExtendCreditAmount(long amount)
        {
            Executed = false;
            if (_client.Reputation > 7)
            {
                _account.AccountAmount -= amount;
                new ReputationDecreaser(_client, 2);
                Execute();
            }
        }
        public void PayOff()
        {
            Executed = false;
            _account.AccountAmount = 0;
            _client.CreditIsActive = false;
            _client.ClientsCreditAccount = null;
            new ReputationIncreaser(_client, 2);
            Execute();
        }

        public void Pay(long PayAmount)
        {
            Executed = false;
            _account.AccountAmount += PayAmount;
            Execute();
        }

        public void OnExpiredWithoutPayment()
        {
            if (_account.Expired() && _account.AccountAmount < 0)
            {
                _client.Reputation -= _client.Reputation;
                _client.AccountsFreezed = true;
            }
        }

        public void OnExpiredWithCompletePayment()
        {
            if (_account.Expired() && _account.AccountAmount >= 0)
            {
                PayOff();
            }
        }
        public void Execute() =>Executed = true;
    }
}

namespace Homework_13.Model.bankModel
{
    /// <summary>
    /// повышает репутацию клиента
    /// </summary>
    class ReputationIncreaser
    {
        private readonly Client _client;
        private readonly int _value;
        private bool _executed;
        public bool Executed => _executed;

        public ReputationIncreaser(Client client, int value = 1)
        {
            this._client = client;
            this._value = value;
            _executed = false;
        }
        public void Execute()
        {
            if (_client.Reputation >= 10)
            { _client.Reputation = 10; }

            else
            { _client.Reputation += _value; }

            _executed = true;
        }
    }
}

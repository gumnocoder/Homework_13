namespace Homework_13.Model.bankModel
{
    /// <summary>
    /// Понижает репутацию клиента
    /// </summary>
    class ReputationDecreaser
    {
        private readonly Client _client;
        private readonly int _value;
        private bool _executed;
        public bool Executed => _executed;

        public ReputationDecreaser(Client client, int value = 1)
        {
            this._client = client;
            this._value = value;
            _executed = false;
        }

        public void Execute()
        {
            if (_client.Reputation <= 0) { _client.Reputation = 0; }
            else { _client.Reputation -= _value; }

            _executed = true;
        }
    }
}

namespace Homework_13.Model.bankModel
{
    class CreditPercentSetter
    {
        private const double _minPercent = 10;

        public static double SetCreditPercent(Client client)
        {
            if (client.Reputation == 10) return _minPercent;
            int reputation = client.Reputation - 5;
            return 16.0 - (double)(reputation * 2);
        }
    }
}

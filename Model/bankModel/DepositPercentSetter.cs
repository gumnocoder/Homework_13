namespace Homework_13.Model.bankModel
{
    /// <summary>
    /// Назначает процент для накопительного счёта
    /// </summary>
    class DepositPercentSetter
    {
        /// <summary>
        /// Максимальный процент для накопительных счетов
        /// </summary>
        public const double maxPercent = 12;

        /// <summary>
        /// Назначает процент по вкладу
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static double SetDepositPercent(Client client)
        {
            if (client.Reputation == 10) return maxPercent;
            double clientReputation = (double)client.Reputation;
            return maxPercent / 10.0 * clientReputation;
        }
    }
}

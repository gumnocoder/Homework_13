namespace Homework_13.Model.bankModel
{
    /// <summary>
    /// Пополняет счёт
    /// </summary>
    class DepositMaker<T> where T : BankAccount
    {
        private static bool CanMakeDeposit(Client client, T account)
        {
            if (client.AccountsFreezed) return false;
            else if (account.GetType() == typeof(BankCreditAccount) && !client.CreditIsActive) return false;
            else if (account.GetType() == typeof(BankDebitAccount) && !client.DebitIsActive) return false;
            else if (account.GetType() == typeof(BankDepositAccount) && !client.DepositIsActive) return false;
            return true;
        }
        public static void MakeDeposit(Client client, T account, long Amount)
        {
            if (CanMakeDeposit(client, account))
            {
                account.AccountAmount += Amount;
            }
        }
    }
}

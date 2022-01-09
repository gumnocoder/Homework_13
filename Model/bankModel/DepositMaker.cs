using System.Diagnostics;
using static Homework_13.ViewModel.ClientListViewModel;

namespace Homework_13.Model.bankModel
{
    /// <summary>
    /// Пополняет счёт
    /// </summary>
    class DepositMaker
    {
        private static bool CanMakeDeposit(Client client)
        {
            if (client.AccountsFreezed) return false;
            return true;
        }
        public static void MakeDeposit(Client client, BankAccount account, long Amount)
        {
            if (CanMakeDeposit(client))
            {
                
                account.AccountAmount += Amount;
                Debug.WriteLine($" current account amount is: {account.AccountAmount}");
            }
        }
    }
}

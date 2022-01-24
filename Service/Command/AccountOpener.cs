using Homework_13.View.Windows;
using Homework_13.ViewModel;
using BankModelLibrary;
using BankModelLibrary.BankServices;
using BankModelLibrary.BaseBankModels;

namespace Homework_13.Service.Command
{
    /// <summary>
    /// Запускает окно открытия для депозитного или кредитного счёта
    /// </summary>
    class AccountOpener : Command
    {
        public bool? DialogResult { get; set; }
        public bool Deposit { get; set; } = false;
        public double Percent { get; set; }

        public override bool CanExecute(object parameter) 
        {
            if ((parameter as Client) == null) return false;
            if ((parameter as Client).AccountsFreezed) { return false; }
            if ((parameter as Client).DepositIsActive && Deposit) { return false; }
            if ((parameter as Client).CreditIsActive && Deposit) { return false; }
            if ((parameter as Client).CreditIsActive && !Deposit) { return false; }

            return true;
        }

        public override void Execute(object parameter)
        {
            AccountOpening dlg = new();

            dlg.DataContext = 
                new AccountOpeningViewModel(
                    (Client)parameter,
                    Deposit);

            dlg.ShowDialog();
        }
    }
}

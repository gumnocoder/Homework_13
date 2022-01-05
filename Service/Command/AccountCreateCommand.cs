using Homework_13.Model.bankModel;
using Homework_13.View.Windows;
using Homework_13.ViewModel;
using static Homework_13.Service.Command.AccountOpener;

namespace Homework_13.Service.Command
{
    class AccountCreateCommand : Command
    {
        public override bool CanExecute(object parameter)
        {
            return (parameter as AccountOpening) != null;
        }

        double percent;
        long amount;
        public override void Execute(object parameter)
        {
            AccountOpening window = parameter as AccountOpening;

            if (AccountOpeningViewModel.Deposit)
            {
                double.TryParse(window.depPercent.Text, out percent);
                long.TryParse(window.startDep.Text, out amount);
                BankDepositAccount a = new(SelectedClient, amount, percent);
            }
            else
            {
                double.TryParse(window.credPercent.Text, out percent);
                long.TryParse(window.credAmount.Text, out amount);
                BankCreditAccount a = new(SelectedClient, percent, amount);
            }
            SelectedClient = null;
            window.Close();
        }
    }
}

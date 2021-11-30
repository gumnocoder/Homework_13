using System.Diagnostics;
using System.Windows;
using Homework_13.Model;
using Homework_13.Model.bankModel;
using Homework_13.View.Windows;
using Homework_13.ViewModel;

namespace Homework_13.Service.Command
{
    class AccountOpener : Command
    {
        public bool? DialogResult { get; set; }
        public bool Deposit { get; set; } = false;
        public bool Credit { get; set; } = false;

        public double Percent { get; set; }

        public override bool CanExecute(object parameter) 
        {
            return (parameter as Client) != null; 
        }

        public override void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                Client client = parameter as Client;
                if (Deposit)
                {
                    Debug.WriteLine("deposit");
                    BankDepositAccount account = new();
                    AccountOpening dlg = new();

                    dlg.DataContext = new AccountOpeningViewModel(
                        account.SetPercent(ClientListViewModel.SelectedClient),
                        false,
                        true);
                    dlg.ShowDialog();
                }
                else if (Credit)
                {
                    Debug.WriteLine("credit");
                    BankCreditAccount account = new();
                    AccountOpening dlg = new();

                    dlg.DataContext = new AccountOpeningViewModel(
                        account.SetPercent(ClientListViewModel.SelectedClient),
                        true,
                        false);
                    dlg.ShowDialog();
                }
            }
        }
    }
}

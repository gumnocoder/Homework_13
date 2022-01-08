using Homework_13.Model;
using Homework_13.View.Windows;
using Homework_13.ViewModel;

namespace Homework_13.Service.Command
{
    class AccountOpener : Command
    {
        public static Client SelectedClient;
        public bool? DialogResult { get; set; }
        public bool Deposit { get; set; } = false;

        public double Percent { get; set; }

        public override bool CanExecute(object parameter) 
        {
            if ((parameter as Client) != null)
            {
                if (!Deposit && (parameter as Client).CreditIsActive) return false;
                else if (Deposit && (parameter as Client).DepositIsActive) return false;
            }
            return true;
        }

        public override void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                //Debug.WriteLine(parameter);
                SelectedClient = (Client)parameter;
                AccountOpening dlg = new();
                dlg.DataContext = new AccountOpeningViewModel((Client)parameter, Deposit);
                dlg.ShowDialog();
            }
        }
    }
}

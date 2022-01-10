using static Homework_13.ViewModel.ClientListViewModel;
using static Homework_13.Model.bankModel.DepositMaker;
using static Homework_13.Model.bankModel.AccountCreditHandler;
using Homework_13.View.Windows;
using Homework_13.Model.bankModel;
using System.Diagnostics;
using System.Windows;

namespace Homework_13.Service.Command
{
    class ExtendCreditCommand : Command
    {
        public override bool CanExecute(object parameter) =>
            parameter as CreditExtentionView != null;

        public override void Execute(object parameter)
        {
            CreditExtentionView window = parameter as CreditExtentionView;

            int amount = 0;

            if (int.TryParse(window.summField.Text, out int tmp))
            { amount = tmp; Debug.WriteLine("Парсинг успешно завершен"); }

            AccountCreditHandler creditHandler =
                new(SelectedClient, (BankCreditAccount)SelectedAccount);

            if (creditHandler.ExtendCreditAmount(amount))
            {
                string message = $"Кредит успешно расширен на сумму {amount}";
                MessageBox.Show(message, "Успешно", MessageBoxButton.OK);
            }

            else
            {
                string message = $"Не удалось расширить кредит, низкая репутация";
                MessageBox.Show(message, "Ошибка", MessageBoxButton.OK);
            }

            window.Close();
        }
    }
}

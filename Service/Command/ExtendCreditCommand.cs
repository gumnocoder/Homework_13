using System.Diagnostics;
using BankModelLibrary;
using BankModelLibrary.BankServices;
using Homework_13.View.Windows;
using static Homework_13.Service.InformationDialogService;
using static Homework_13.ViewModel.ClientListViewModel;

namespace Homework_13.Service.Command
{
    /// <summary>
    /// Класс выполняющий расширение кредита
    /// </summary>
    class ExtendCreditCommand : Command
    {
        public override bool CanExecute(object parameter) =>
            parameter as CreditExtentionView != null;

        public override void Execute(object parameter)
        {
            CreditExtentionView window = parameter as CreditExtentionView;

            int amount = 0;

            /// выполняется при успешном парсинге положительного числа
            if (int.TryParse(window.summField.Text, out int tmp) && tmp > 0)
            { 
                amount = tmp;

                AccountCreditHandler creditHandler =
                    new(SelectedClient, (BankCreditAccount)SelectedAccount);

                Debug.WriteLine("Парсинг успешно завершен");
                if (creditHandler.ExtendCreditAmount(amount))
                {
                    ShowInformation(
                        $"Кредит успешно расширен на сумму {amount}",
                        "Успешно");
                    window.Close();
                }

                else
                {
                    ShowError($"Не удалось расширить кредит, низкая репутация");
                    window.Close();
                }
            }
            else { ShowError("Недопустимое значение, введите " +
                "целочисленное, положительное значение"); }
        }
    }
}

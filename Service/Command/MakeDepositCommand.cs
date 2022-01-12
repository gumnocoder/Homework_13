using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Homework_13.ViewModel.ClientListViewModel;
using static Homework_13.Model.bankModel.DepositMaker;
using static Homework_13.Model.bankModel.AcccountDepositHandler;
using Homework_13.View.Windows;
using System.Diagnostics;
using Homework_13.Model.bankModel;
using System.Windows;

namespace Homework_13.Service.Command
{
    /// <summary>
    /// Команда для пополения счетов
    /// </summary>
    class MakeDepositCommand : Command
    {
        public override bool CanExecute(object parameter) => 
            parameter as DepositMakerView != null;

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK);
        }
        public override void Execute(object parameter)
        {
            DepositMakerView window = parameter as DepositMakerView;

            long amount = 0;

            if (long.TryParse(window.summField.Text, out long tmp)) 
            { amount = tmp; Debug.WriteLine("Парсинг успешно завершен"); }

            if (SelectedAccount.GetType() == typeof(BankDebitAccount)) 
            { MakeDeposit(SelectedClient, SelectedAccount, amount); }

            else if (SelectedAccount.GetType() == typeof(BankDepositAccount))
            {
                AcccountDepositHandler depositHandler = new(SelectedClient, (BankDepositAccount)SelectedAccount);
                if (amount >= minDepositExtension) 
                { depositHandler.DepositExtension(amount); }

                else { ShowError($"Минимальная сумма для расширения " +
                    $"депозитного счёта составляет: {minDepositExtension}"); }
            }
            else if (SelectedAccount.GetType() == typeof(BankCreditAccount))
            {
                AccountCreditHandler creditHandler = 
                    new(SelectedClient, (BankCreditAccount)SelectedAccount);

                if ((long)amount >= -((long)SelectedAccount.AccountAmount)) 
                { 
                    creditHandler.PayOff(); }

                else 
                { creditHandler.Pay(amount); }
            }
            window.Close();
        }
    }
}

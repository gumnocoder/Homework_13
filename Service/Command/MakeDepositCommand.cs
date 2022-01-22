using System.Diagnostics;
using Homework_13.Model.bankModel;
using Homework_13.View.Windows;
using static Homework_13.Model.bankModel.AcccountDepositHandler;
using static Homework_13.Model.bankModel.DepositMaker;
using static Homework_13.Service.InformationDialogService;
using static Homework_13.ViewModel.ClientListViewModel;

namespace Homework_13.Service.Command
{
    /// <summary>
    /// Команда для пополения счетов
    /// </summary>
    class MakeDepositCommand : Command
    {
        public override bool CanExecute(object parameter) => 
            parameter as DepositMakerView != null;

        public override void Execute(object parameter)
        {
            DepositMakerView window = parameter as DepositMakerView;
            EventAction += HudViewer.ShowHudWindow;
            HistoryEventAction += LogWriter.WriteToLog;

            long amount = 0;

            /// парсинг введенной суммы в int64
            if (long.TryParse(window.summField.Text, out long tmp)) 
            { 
                amount = tmp; Debug.WriteLine("Парсинг успешно завершен"); 
            }
            else 
            {
                //ShowError("Не удалось распознать число!"); 
                OnEventAction($"" +
                    $"Не удалось распознать число", false, true);
                OnHistoryEventAction(
                    $"Пополнен счёт {SelectedAccount.ID} " +
                    $"клиента {SelectedClient} " +
                    $"на сумму ${amount}. " +
                    $"Состояние: {SelectedAccount}");
            }

            /// выполняется если введенная сумма положительная и больше нуля
            if (amount > 0)
            {
                /// пополнение дебетового счёта
                if (SelectedAccount.GetType() == typeof(BankDebitAccount))
                { 
                    MakeDeposit(SelectedClient, SelectedAccount, amount); 
                    OnEventAction($"" +
                        $"Пополнен счёт {SelectedAccount.ID} на сумму ${amount}.", true, false);
                    OnHistoryEventAction(
                        $"Пополнен счёт {SelectedAccount.ID} " +
                        $"клиента {SelectedClient} " +
                        $"на сумму ${amount}. " +
                        $"Состояние: {SelectedAccount}");
                }

                /// пополнение депозитного счёта
                else if (SelectedAccount.GetType() == typeof(BankDepositAccount))
                {
                    AcccountDepositHandler depositHandler = new(SelectedClient, (BankDepositAccount)SelectedAccount);
                    if (amount >= minDepositExtension)
                    { depositHandler.DepositExtension(amount); }

                    else
                    {
                     //   ShowError($"Минимальная сумма для расширения " +
                     //$"депозитного счёта составляет: {minDepositExtension}");

                        OnEventAction($"Минимальная сумма для расширения " +
                     $"депозитного счёта составляет: ${minDepositExtension}", false, true);
                        OnHistoryEventAction(
                            $"Попытка расширения депозитного счёта {SelectedAccount.ID} " +
                            $"на сумму ${amount}, меньше минимальной - ${minDepositExtension}");
                    }
                }

                /// погашение кредита
                else if (SelectedAccount.GetType() == typeof(BankCreditAccount))
                {
                    AccountCreditHandler creditHandler =
                        new(SelectedClient, (BankCreditAccount)SelectedAccount);

                    if ((long)amount >= -((long)SelectedAccount.AccountAmount))
                    {
                        creditHandler.PayOff();
                        OnEventAction($"" +
                             $"Кредит {SelectedAccount.ID} погашен.", true, false);
                        OnHistoryEventAction(
                            $"Кредитный счёт {SelectedAccount.ID} " +
                            $"клиента {SelectedClient} погашен");
                    }

                    else
                    { 
                        creditHandler.Pay(amount);
                        OnEventAction($"" +
                             $"Выплата по кредиту {SelectedAccount.ID} на сумму {amount}", true, false);
                        OnHistoryEventAction(
                            $"Выплата на кредитный счёт {SelectedAccount.ID} " +
                            $"клиента {SelectedClient} " +
                            $"на сумму ${amount}. " +
                            $"Состояние: {SelectedAccount}");
                    }
                }
                window.Close();
            }
        }
    }
}

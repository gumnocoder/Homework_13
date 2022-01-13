using System;
using Homework_13.View.Windows;
using static Homework_13.ViewModel.ClientListViewModel;
using static Homework_13.Service.InformationDialogService;
using static Homework_13.Model.bankModel.MoneySender<
    Homework_13.Model.bankModel.BankAccount>;
using Homework_13.Model.bankModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Homework_13.Model;

namespace Homework_13.Service.Command
{
    class SendMoneyCommand : Command
    {
        long amount = 0, id = 0;
        private long ParseField(
            string Field,
            string ParsingErrorMessage,
            string TextEmptyError)
        {
            Debug.WriteLine($"Парсинг {Field}");
            if (Field != string.Empty)
            {
                if (long.TryParse(Field, out long tmp))
                {
                    Debug.WriteLine($"Парсинг выполнен, число - {tmp}");
                    return tmp;
                }
                else
                {
                    ShowError(ParsingErrorMessage);
                    return 0;
                }
            }
            else 
            { 
                ShowError(TextEmptyError);
                return 0;
            }
        }

        public override bool CanExecute(object parameter) =>
            parameter is MoneySenderView;

        private BankAccount FindAccount(long ID, ObservableCollection<BankAccount> List)
        {
            Debug.WriteLine($"Выполняется поиск в списке счетов {List}");
            BankAccount tmp = default;
            foreach (var e in List)
            {
                Debug.WriteLine(e);
                Debug.WriteLine(e.ID);
                Debug.WriteLine(ID);
                if (e.ID == ID) { tmp = e; Debug.WriteLine($"Найден {e}"); }
                break;
            }
            Debug.WriteLine($"будет возвращено {tmp}");
            return tmp;
        }

        private BankAccount SelectList(long ID)
        {
            if (ID < 10000) { return FindAccount(ID, Bank.ThisBank.Deposits); }
            else if (ID >= 10000000) { return FindAccount(ID, Bank.ThisBank.Debits); }
            else { return FindAccount(ID, Bank.ThisBank.Credits); }
        }
        public override void Execute(object parameter)
        {
            MoneySenderView window = (MoneySenderView)parameter;

            amount = ParseField(
                window.AmountField.Text,
                "Не удалось распознать сумму",
                "Заполните поле 'сумма транзакции'!");
            id = ParseField(
                    window.IdField.Text,
                    "Не удалось распознать введенный номер счёта",
                    "Введите номер счёта, на который хотите отправить деньги");

            Debug.WriteLine("Найденный счёт:");
            Debug.WriteLine(SelectList(id));

            if (amount > SelectedAccount.AccountAmount) ShowError("Недостаточно средств!");

            if (id != 0 && amount != 0)
            {
                BankAccount accountReciever = SelectList(id);
                if (accountReciever.GetType() != typeof(BankCreditAccount))
                {
                    SendMoney(SelectedClient, amount, SelectedAccount, accountReciever);
                }
                else
                {
                    Client tempClient = default;

                    foreach (Client client in ClientList<Client>.ClientsList)
                    {
                        if (client.CreditAccountID == id)
                        {
                            tempClient = client;
                            break;
                        }
                    }

                    AccountCreditHandler creditHandler =
                        new(tempClient, (BankCreditAccount)accountReciever);

                    if ((long)amount >= -((long)accountReciever.AccountAmount))
                    {
                        SelectedAccount.AccountAmount -= amount;
                        creditHandler.PayOff();
                    }

                    else
                    { 
                        SelectedAccount.AccountAmount -= amount; 
                        creditHandler.Pay(amount); 
                    }
                }
            }
            else ShowError("Не удалось распознать требуемые значения");
        }
    }
}

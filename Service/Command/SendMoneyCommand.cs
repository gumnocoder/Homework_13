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
    /// <summary>
    /// Команда для выполнения транзаций
    /// </summary>
    class SendMoneyCommand : Command
    {
        long amount = 0, id = 0;

        /// <summary>
        /// Выполняет парсинг строки в int64
        /// </summary>
        /// <param name="Field">Поле, содержащее текст</param>
        /// <param name="ParsingErrorMessage"></param>
        /// <param name="TextEmptyError"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Выполняет поиск счёта  по ID в соответвующей коллекции 
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="List">Коллекция счетов</param>
        /// <returns></returns>
        private BankAccount FindAccount(long ID, ObservableCollection<BankAccount> List)
        {

            Debug.WriteLine($"Выполняется поиск в списке счетов {List}");
            BankAccount tmp = default;
            foreach (var e in List)
            {
                if (e.ID == ID) 
                { 
                    tmp = e; 
                    Debug.WriteLine($"Найден {e}"); 
                }
                break;
            }
            Debug.WriteLine($"будет возвращено {tmp}");
            return tmp;
        }

        /// <summary>
        /// Выбирает подходящий к переданному ID тип счёта и выполняет поиск 
        /// по ID в коллекции подходящих типу счетов
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
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
            else
            {
                if (id != 0 && amount > 0)
                {
                    BankAccount accountReciever = SelectList(id);

                    /// проверяет что аккаунт получатель существует
                    if (accountReciever != null)
                    {
                        /// что счет отправитель и счет получатель не одно и то же
                        if (accountReciever.ID != SelectedAccount.ID)
                        {
                            /// в любом случае кроме отправки на кредитный счёт
                            if (accountReciever.GetType() != typeof(BankCreditAccount))
                            {
                                SendMoney(SelectedClient, amount, SelectedAccount, accountReciever);
                                ShowInformation(
                                    $"{SelectedClient} перевёл ${amount} на счёт {accountReciever.ID}",
                                    "Перевод выполнен");
                                window.Close();
                            }
                            /// в случае отправки на кредитынй счёт
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
                                    ShowInformation("Кредит успешно погашен!", "Успешно");
                                    window.Close();
                                }

                                else
                                {
                                    SelectedAccount.AccountAmount -= amount;
                                    creditHandler.Pay(amount);
                                    ShowInformation($"Совершена выплата по кредиту в размере ${amount}", "Успешно");
                                    window.Close();
                                }
                            }
                        }
                        else
                        {
                            ShowError(
                         "Невозможно отправить средства со счёта-отправителя " +
                         "на тот же самый счёт. Какой в этом смысл?");
                        }
                    }
                    else { ShowError("Указанного счёта не существует!"); }
                }
                else ShowError("Не удалось распознать требуемые значения");
            }
        }
    }
}

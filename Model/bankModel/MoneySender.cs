using System.Diagnostics;
using Homework_13.Model.bankModel.interfaces;

namespace Homework_13.Model.bankModel
{
    /// <summary>
    /// отвечает за перевод средств между счетами
    /// </summary>
    /// <typeparam name="T">BankAccount ковариантно</typeparam>
    class MoneySender<T> : IMoneySender<T> where T : BankAccount
    {
        /// <summary>
        /// проверяет возможно ли выполнить операцию
        /// </summary>
        /// <param name="client">Владелец счета</param>
        /// <param name="sum">Суммп отправления</param>
        /// <param name="sender">Счёт-отправитель</param>
        /// <param name="reciever">Счёт-получатель</param>
        /// <returns></returns>
        private static bool CanSendMoney(Client client, long sum, T sender, T reciever)
        {
            if (!client.AccountsFreezed)
            {
                if (sender != null)
                {
                    if (reciever != null)
                    {
                        if (sender.AccountAmount >= sum) 
                        {
                            if (sender != reciever) return true;
                            else { Debug.WriteLine("Нужно выбрать в качестве получателя другой счёт"); }
                        }
                        else { Debug.WriteLine("У получателя не хватает средств для выполнения операции"); }
                    }
                    else { Debug.WriteLine("У получателя не существует данного счета"); }
                }
                else  { Debug.WriteLine("У отправителя не существует данного счета"); }
            }
            else { Debug.WriteLine($"Счета отправителя {client} заморожены"); }

            return false;
        }

        /// <summary>
        /// выполняет отправку средств
        /// </summary>
        /// <param name="clientSender">клиент отправитель</param>
        /// <param name="sum">сумма отправления</param>
        /// <param name="sender">счёт-отправитель</param>
        /// <param name="reciever">счёт-получатель</param>
        public static void SendMoney(Client clientSender, long sum, T sender, T reciever)
        {
            if (CanSendMoney(clientSender, sum, sender, reciever))
            {
                sender.RemoveMoneyFromAccount(sum);
                reciever.AddMoneyToAccount(sum);
            }
        }
    }
}

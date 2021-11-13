using System.Diagnostics;
using Homework_13.Model.bankModel.interfaces;

namespace Homework_13.Model.bankModel
{
    class MoneySender<T> : IMoneySender<T> where T : BankAccount
    {
        private bool CanSendMoney(Client client, long sum, T sender, T reciever)
        {
            if (!client.AccountsFreezed)
            {
                if (sender != null)
                {
                    if (reciever != null) return true;
                    else
                    {
                        Debug.WriteLine("У получателя не существует данного счета");
                        return false;
                    }
                }
                else 
                { 
                    Debug.WriteLine("У отправителя не существует данного счета");
                    return false;
                }
            }
            else
            {
                Debug.WriteLine($"Счета отправителя {client} заморожены");
                return false;
            }
        }
        public void SendMoney(Client clientSender, long sum, T sender, T reciever)
        {
            if (CanSendMoney(clientSender, sum, sender, reciever))
            {
                sender.RemoveMoneyFromAccount(sum);
                reciever.AddMoneyToAccount(sum);
            }
        }
    }
}

using Homework_13.Model.bankModel.interfaces;

namespace Homework_13.Model.bankModel
{
    class MoneySender<T> : IMoneySender<T> where T : BankAccount
    {
        private bool CanSendMoney(Client client)
        {
            if (!client.AccountFreezed) return true;
            else return false;
        }
        public void SendMoney(Client clientSender, long sum, T sender, T reciever)
        {
            if (CanSendMoney(clientSender))
            {
                if (sender != null && reciever != null)
                {
                    if (sender.AccountAmount >= sum)
                    {
                        sender.AccountAmount -= sum;
                        reciever.AccountAmount += sum;
                    }
                }
            }
        }
    }
}

namespace Homework_13.Model.bankModel.interfaces
{
    interface IMoneySender<in T>
    {
        void SendMoney(Client clientSender, long sum, T sender, T reciever);
    }
}

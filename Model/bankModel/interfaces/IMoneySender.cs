namespace Homework_13.Model.bankModel.interfaces
{
    /// <summary>
    /// Для реализации в классе организующем перессылку средств между счетами
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface IMoneySender<in T>
    {
        /// <summary>
        /// Метод перессылки средств на другой аккаунт
        /// </summary>
        /// <param name="clientSender">Отправитель</param>
        /// <param name="sum">Сумма</param>
        /// <param name="sender">Счёт отправителя</param>
        /// <param name="reciever">Счёт получателя</param>
        void SendMoney(Client clientSender, long sum, T sender, T reciever);
    }
}

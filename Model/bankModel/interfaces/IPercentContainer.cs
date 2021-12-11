namespace Homework_13.Model.bankModel.interfaces
{
    /// <summary>
    /// Для реализации в классах, экземпляры которых должны иметь зависимость суммы счёта от установленного процента 
    /// </summary>
    interface IPercentContainer
    {
        /// <summary>
        /// установленный процент
        /// </summary>
        double Percent { get; set; }

        /// <summary>
        /// метод позволябщий установить процент
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        double SetPercent(Client client);

    }
}

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
    }
}

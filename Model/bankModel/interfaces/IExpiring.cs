using System;

namespace Homework_13.Model.bankModel.interfaces
{
    /// <summary>
    /// Для реализации в классах, экземпляры которых
    /// должны иметь срок истечения
    /// </summary>
    interface IExpiring
    {
        /// <summary>
        /// срок в месяцах
        /// </summary>
        int Expiration { get; set; }

        /// <summary>
        /// Дата активации
        /// </summary>
        DateTime ActivationDate { get; set; }

        /// <summary>
        /// Дата следующего начисления процентов
        /// </summary>
        DateTime NextPaymentDay { get; set; }

        /// <summary>
        /// Активность
        /// </summary>
        /// <returns></returns>
        bool Expired();

        /// <summary>
        /// Вычисление даты следующей выплаты
        /// </summary>
        void CalculateNextPaymentDay();
    }
}

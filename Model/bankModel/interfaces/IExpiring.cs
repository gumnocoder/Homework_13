using System;

namespace Homework_13.Model.bankModel.interfaces
{
    interface IExpiring
    {
        int Expiration { get; set; }

        int ExpirationDuration { get; }

        DateTime ActivationDate { get; }

        bool Expired();

        public int GetTotalMonthsCount();
    }
}

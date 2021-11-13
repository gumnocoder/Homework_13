using System;

namespace Homework_13.Model.bankModel.interfaces
{
    interface IExpiring
    {
        public int Expiration { get; set; }

        DateTime ActivationDate { get; }

        bool Expired();
    }
}

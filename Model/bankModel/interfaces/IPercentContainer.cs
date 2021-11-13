using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_13.Model.bankModel.interfaces
{
    interface IPercentContainer
    {
        double Percent { get; set; }
        double SetPercent(Client client);
        int Expiration { get; set; }
    }
}

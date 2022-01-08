using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_13.Model.bankModel
{
    class DepositPercentSetter
    {
        public const double maxPercent = 12;


        /// <summary>
        /// Назначает процент по вкладу
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static double SetDepositPercent(Client client)
        {
            if (client.Reputation == 10) return maxPercent;
            double clientReputation = (double)client.Reputation;
            return maxPercent / 10.0 * clientReputation;
        }
    }
}

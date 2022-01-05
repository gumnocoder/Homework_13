using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_13.Model.bankModel
{
    class DepositPercentSetter
    {
        private const double _maxPercent = 12;


        /// <summary>
        /// Назначает процент по вкладу
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static double SetDepositPercent(Client client)
        {
            if (client.Reputation == 10) return _maxPercent;
            double clientReputation = (double)client.Reputation;
            return _maxPercent / 10.0 * clientReputation;
        }
    }
}

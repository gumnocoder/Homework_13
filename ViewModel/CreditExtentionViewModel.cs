using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework_13.Service.Command;

namespace Homework_13.ViewModel
{
    class CreditExtentionViewModel
    {
        private int _amount = 0;
        public int Amount
        {
            get => _amount;
            set => _amount = value;
        }
    }
}

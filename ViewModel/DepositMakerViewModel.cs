﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework_13.Model.bankModel;
using Homework_13.Service.Command;
using static Homework_13.ViewModel.ClientListViewModel;
using static Homework_13.Model.bankModel.DepositMaker;
using System.Diagnostics;

namespace Homework_13.ViewModel
{
    class DepositMakerViewModel
    {
        private int _amount = 0;
        public int Amount 
        {
            get => _amount;
            set => _amount = value;
        }
    }
}

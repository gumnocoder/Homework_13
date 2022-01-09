using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Homework_13.ViewModel.ClientListViewModel;
using static Homework_13.Model.bankModel.DepositMaker;
using Homework_13.View.Windows;
using System.Diagnostics;
using Homework_13.Model.bankModel;

namespace Homework_13.Service.Command
{
    class MakeDepositCommand : Command
    {
        public override bool CanExecute(object parameter) => parameter as DepositMakerView != null;

        public override void Execute(object parameter)
        {
            DepositMakerView window = parameter as DepositMakerView;
            int amount = 0;
            if (int.TryParse(window.summField.Text, out int tmp)) { amount = tmp; Debug.WriteLine("Парсинг успешно завершен"); }

            MakeDeposit(SelectedClient, SelectedAccount, amount);
            window.Close();
        }
    }
}

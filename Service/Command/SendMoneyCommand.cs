using System;
using static Homework_13.ViewModel.ClientListViewModel;


namespace Homework_13.Service.Command
{
    class SendMoneyCommand : Command
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
        }
    }
}

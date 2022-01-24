using BankModelLibrary;
using BankModelLibrary.BankServices;
using BankModelLibrary.BaseBankModels;
using static BankModelLibrary.Bank;

namespace Homework_13.Service.Command
{
    class CreateDebitCommand : Command
    {
        public override bool CanExecute(object parameter) =>
            parameter as Client != null && !(parameter as Client).DebitIsActive;

        public override void Execute(object parameter)
        {
            BankDebitAccount a = new(parameter as Client);
            EventAction += HudViewer.ShowHudWindow;
            HistoryEventAction += LogWriter.WriteToLog;

            OnEventAction($"Дебетовый счёт успешно открыт", true, false);
            OnHistoryEventAction($"Открыт дебетовый счёт {a}");
            //ShowInformation(
             //   "Дебетовый счёт успешно открыт!",
             //   "Операция завершена");
        }
    }
}

using Homework_13.Model;
using Homework_13.Model.bankModel;
using static Homework_13.Service.InformationDialogService;

namespace Homework_13.Service.Command
{
    class CreateDebitCommand : Command
    {
        public override bool CanExecute(object parameter) =>
            parameter as Client != null && !(parameter as Client).DebitIsActive;

        public override void Execute(object parameter)
        {
            new BankDebitAccount(parameter as Client);

            ShowInformation(
                "Дебетовый счёт успешно открыт!",
                "Операция завершена");
        }
    }
}

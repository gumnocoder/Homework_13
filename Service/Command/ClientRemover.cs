using Homework_13.Model;
using Homework_13.Model.bankModel;
using static Homework_13.Model.bankModel.Bank;
using static Homework_13.Model.bankModel.TimeChecker;

namespace Homework_13.Service.Command
{
    /// <summary>
    /// Команда удаления клиента
    /// </summary>
    class ClientRemover : Command
    {
        public override bool CanExecute(object parameter) =>
            (parameter as Client) != null;

        public override void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                Client client = parameter as Client;

                foreach (var c in ClientList<Client>.ClientsList)
                {
                    if (c == (client)) 
                    {
                        // удаляет кредитный счёт при его наличии
                        if (client.ClientsCreditAccount != null)
                        {
                            foreach (var a in ThisBank.Credits)
                            {
                                if (a == client.ClientsCreditAccount) ThisBank.Credits.Remove(a);
                                break;
                            }
                        }
                        // удаляет дебетовый счёт при его наличии
                        if (client.ClientsDebitAccount != null)
                        {
                            foreach (var a in ThisBank.Debits)
                            {
                                if (a == client.ClientsDebitAccount) ThisBank.Debits.Remove(a);
                                break;
                            }
                        }
                        // удаляет депозитный счёт при его наличии
                        if (client.ClientsDepositAccount != null)
                        {
                            foreach (var a in ThisBank.Deposits)
                            {
                                if (a == client.ClientsDepositAccount) 
                                {
                                    TimeCheck.OnTimerSignal -= 
                                        (a as BankDepositAccount).DateComparer; 
                                    ThisBank.Deposits.Remove(a);
                                }
                                break;
                            }
                        }
                        // удаляет клиента из коллекции в синглтоне ThisBank
                        ClientList<Client>.ClientsList.Remove(c);
                        break; 
                    }
                }
            }
        }
    }
}

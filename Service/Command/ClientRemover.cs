using Homework_13.Model;
using static Homework_13.Model.bankModel.Bank;

namespace Homework_13.Service.Command
{
    class ClientRemover : Command
    {
        public override bool CanExecute(object parameter)
        {
            return (parameter as Client) != null;
        }

        public override void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                Client client = parameter as Client;
                foreach (var c in ClientList<Client>.ClientsList)
                {
                    if (c == (client)) 
                    {
                        if (client.ClientsCreditAccount != null)
                        {
                            foreach (var a in ThisBank.Credits)
                            {
                                if (a == client.ClientsCreditAccount) ThisBank.Credits.Remove(a);
                                break;
                            }
                        }

                        if (client.ClientsDebitAccount != null)
                        {
                            foreach (var a in ThisBank.Debits)
                            {
                                if (a == client.ClientsDebitAccount) ThisBank.Debits.Remove(a);
                                break;
                            }
                        }

                        if (client.ClientsDepositAccount != null)
                        {
                            foreach (var a in ThisBank.Deposits)
                            {
                                if (a == client.ClientsDepositAccount) ThisBank.Deposits.Remove(a);
                                break;
                            }
                        }

                        ClientList<Client>.ClientsList.Remove(c);
                        break; 
                    }
                }
            }
        }
    }
}

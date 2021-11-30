using Homework_13.Model;

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
                foreach (var c in ClientList<Client>.ClientsList)
                {
                    if (c == (parameter as Client)) 
                    { 
                        ClientList<Client>.ClientsList.Remove(c);
                        break; 
                    }
                }
            }
        }
    }
}

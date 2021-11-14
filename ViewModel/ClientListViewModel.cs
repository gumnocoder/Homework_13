using System.Collections.ObjectModel;
using Homework_13.Model;

namespace Homework_13.ViewModel
{
    class ClientListViewModel : BaseViewModel
    {
        public static ObservableCollection<Client> Clients
        { get => ClientList<Client>.ClientsList; }
        public ClientListViewModel() { }
    }
}

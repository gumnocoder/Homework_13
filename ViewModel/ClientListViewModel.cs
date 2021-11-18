using System.Collections.ObjectModel;
using System.Windows;

using Homework_13.Model;
using Homework_13.Service.Command;

namespace Homework_13.ViewModel
{
    class ClientListViewModel : WindowsBasicFuncs
    {
        public static ObservableCollection<Client> Clients
        { get => ClientList<Client>.ClientsList; }
        public ClientListViewModel() { }

        private string _findText = "поиск";

        public string FindText
        {
            get => _findText;
            set
            {
                _findText = value;
                OnPropertyChanged();
            }
        }
    }
}

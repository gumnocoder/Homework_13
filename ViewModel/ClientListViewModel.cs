using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

using Homework_13.Model;
using Homework_13.Service;
using Homework_13.Service.Command;

namespace Homework_13.ViewModel
{
    class ClientListViewModel : WindowsBasicFuncs
    {
        private static  Client _selectedClient;

        private UserDialogService _dialogService = new();

        private InformationDialogService _informDialogService = new();

        public static Client SelectedClient
        {
            get => _selectedClient;
            set
            {
                if (value != null && value.GetType() == typeof(Client))
                {
                    _selectedClient = value;
                }
                else Debug.WriteLine("Передаваемое значение должно иметь тип Client");
            }
        }
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

        private RelayCommand _editClient;

        public RelayCommand EditClient
        {
            get => _editClient ??= new(EditClientCommand);
        }

        private void EditClientCommand(object s)
        {
            if (SelectedClient != null)
            {
                _dialogService.Edit(SelectedClient);
            }
        }

        private RelayCommand _openDebit;

        public RelayCommand OpenDebit
        {
            get => _openDebit ??= new(OpenDebitCommand);
        }

        private void OpenDebitCommand(object s)
        {
            if (SelectedClient != null && SelectedClient.DebitIsActive == false)
            {
                SelectedClient.ClientsDebitAccount = new(SelectedClient);
                _informDialogService.ShowInformation("Дебетовый счёт успешно открыт!", "Операция завершена");
            }
            else if (SelectedClient == null) _informDialogService.ShowError("Выберите клиента!");
            else if (SelectedClient != null && SelectedClient.DebitIsActive == true) _informDialogService.ShowError("Дебетовый счёт уже открыт");
        }
    }
}

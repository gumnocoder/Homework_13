using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

using Homework_13.Model;
using Homework_13.Model.bankModel;
using Homework_13.Service;
using Homework_13.Service.Command;
using static Homework_13.ViewModel.ParameterChangingInputVM;

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

        #region Команда повышеия репутации
        private RelayCommand _increaseRep;

        public RelayCommand IncreaseRep
        {
            get => _increaseRep ??= new(IncreaseReputation);
        }

        private void IncreaseReputation(object s)
        {
            if (SelectedClient != null)
            {
                ReputationIncreaser increaser = new(SelectedClient);
                increaser.Execute();
            }
            else
            {
                _informDialogService.ShowError("Выберите клиента!");
            }
        }
        #endregion

        #region Команда понижения репутации
        private RelayCommand _decreaseRep;

        public RelayCommand DecreaseRep
        {
            get => _decreaseRep ??= new(DecreaseReputation);
        }

        private void DecreaseReputation(object s)
        {
            if (SelectedClient != null)
            {
                ReputationDecreaser decreaser = new(SelectedClient);
                decreaser.Execute();
            }
            else
            {
                _informDialogService.ShowError("Выберите клиента!");
            }
        }
        #endregion

        private RelayCommand _parameterIncrease;

        public RelayCommand ParameterIncrease =>
            _parameterIncrease ??= new(ShowParameterChangerIncrease);

        public void ShowParameterChangerIncrease(object s)
        {
            if (SelectedClient != null)
            {
                incr = true;
                _dialogService.Edit(new ParameterChangingInputVM());
            }
            else
            {
                _informDialogService.ShowError("Выберите клиента!");
            }
        }

        private RelayCommand _parameterDecrease;

        public RelayCommand ParameterDecrease =>
            _parameterDecrease ??= new(ShowParameterChangerDecrease);

        public void ShowParameterChangerDecrease(object s)
        {
            if (SelectedClient != null)
            {
                decr = true;
                _dialogService.Edit(new ParameterChangingInputVM());
            }
            else
            {
                _informDialogService.ShowError("Выберите клиента!");
            }
        }
    }
}

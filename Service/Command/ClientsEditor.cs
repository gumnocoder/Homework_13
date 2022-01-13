using Homework_13.View.Windows;
using Homework_13.ViewModel;

namespace Homework_13.Service.Command
{
    /// <summary>
    /// Позволяет редактировать клиентов
    /// </summary>
    class ClientsEditor : Command
    {

        public override bool CanExecute(object parameter) => 
            parameter is ClientEditingForm;

        public override void Execute(object parameter)
        {
            ClientEditingForm dlg = parameter as ClientEditingForm;

            if (dlg.NameField.Text != string.Empty)
                ClientListViewModel.SelectedClient.Name = dlg.NameField.Text;
            if (dlg.TypesList.Text != string.Empty)
                ClientListViewModel.SelectedClient.Type = dlg.TypesList.Text;

            dlg.Close();
        }
    }
}

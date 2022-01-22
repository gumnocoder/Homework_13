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
            EventAction += HudViewer.ShowHudWindow;

            if (dlg.NameField.Text != string.Empty && 
                ClientListViewModel.SelectedClient.Name != dlg.NameField.Text)
            {
                OnEventAction($"имя клиента " +
                    $"{ClientListViewModel.SelectedClient.Name} " +
                    $"изменено на " +
                    $"{dlg.NameField.Text}", true, false);
                ClientListViewModel.SelectedClient.Name = dlg.NameField.Text;
            }

            if (dlg.TypesList.Text != string.Empty && 
                ClientListViewModel.SelectedClient.Type != dlg.TypesList.Text)
            {
                OnEventAction($"тип клиента " +
                    $"{ClientListViewModel.SelectedClient.Name} " +
                    $"изменен на " +
                    $"{dlg.TypesList.Text}", true, false);
                ClientListViewModel.SelectedClient.Type = dlg.TypesList.Text;
            }

            dlg.Close();
        }
    }
}

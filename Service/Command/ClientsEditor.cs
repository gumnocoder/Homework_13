using System;
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
            HistoryEventAction += LogWriter.WriteToLog;

            if (dlg.NameField.Text != string.Empty && 
                ClientListViewModel.SelectedClient.Name != dlg.NameField.Text)
            {
                OnEventAction($"имя клиента " +
                    $"изменено на " +
                    $"{dlg.NameField.Text}", true, false);

                OnHistoryEventAction($"" +
                    $"{DateTime.UtcNow} : " +
                    $"Изменение имени клиента [" +
                    $"{ClientListViewModel.SelectedClient}] c " +
                    $"{ClientListViewModel.SelectedClient.Name} " +
                    $"на {dlg.NameField.Text} : " +
                    $"{MainWindowViewModel.CurrentUser}");

                ClientListViewModel.SelectedClient.Name = dlg.NameField.Text;
            }

            if (dlg.TypesList.Text != string.Empty && 
                ClientListViewModel.SelectedClient.Type != dlg.TypesList.Text)
            {
                OnEventAction($"тип клиента " +
                    $"изменен на " +
                    $"{dlg.TypesList.Text}", true, false);

                OnHistoryEventAction($"" +
                    $"{DateTime.UtcNow} : " +
                    $"Изменение типа клиента [" +
                    $"{ClientListViewModel.SelectedClient}] c " +
                    $"{ClientListViewModel.SelectedClient.Type} " +
                    $"на {dlg.TypesList.Text} : " +
                    $"{MainWindowViewModel.CurrentUser}");

                ClientListViewModel.SelectedClient.Type = dlg.TypesList.Text;
            }

            dlg.Close();
        }
    }
}

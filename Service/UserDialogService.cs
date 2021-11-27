using System.Linq;
using System.Windows;
using Homework_13.Model;
using Homework_13.Service.Interfaces;
using Homework_13.View;
using Homework_13.View.UserControls;
using Homework_13.View.Windows;
using Homework_13.ViewModel;
using static Homework_13.ViewModel.ClientEditingFormViewModel;
using static Homework_13.View.UserControls.UserCreationForm;
using System.Diagnostics;

namespace Homework_13.Service
{
    class UserDialogService : IUserDialogService
    {
        private static Window _owner = 
            Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);

        public bool Confirm(string Message, string Tittle, bool Choice = false) =>
            MessageBox.Show(Message, Tittle, MessageBoxButton.YesNo) == MessageBoxResult.Yes;

        public bool Edit(object o)
        {
            switch (o)
            {
                case Client client:
                    return EditSelectedClient(client);
                case User user:
                    return LoginFormOpen(user);
                case object window:
                    return GenericWindowOpenerMethod(window);
            }
            return true;
        }

        public bool EditUser(User user)
        {
            Debug.WriteLine("1");
            UserEditingForm dlg = new();
            Debug.WriteLine("2");
            dlg.DataContext = new UserEditingFormViewModel(user);
            dlg.Owner = _owner;
            dlg.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            if (dlg.ShowDialog() != true) return false;

            return true;
        }
        public void ShowError(string Message, string Tittle)
        {
            MessageBox.Show(
                Message, 
                Tittle,
                MessageBoxButton.OK);
        }
        public void ShowInformation(string Message, string Tittle)
        {
            MessageBox.Show(
                Message,
                Tittle,
                MessageBoxButton.OK);
        }

        private static bool EditSelectedClient(Client client)
        {
            ClientEditingForm dlg = new();
            dlg.DataContext = new ClientEditingFormViewModel(client);
            dlg.Owner = _owner;
            dlg.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            if (dlg.ShowDialog() != true) return false;

            return true;
        }

        private static bool LoginFormOpen(User u)
        {
            var dlg = new LoginFormWindow();

            dlg.Owner = _owner;
            dlg.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            if (dlg.ShowDialog() != true) return false;
            return true;

        }
        private static bool GenericWindowOpenerMethod(object o)
        {
            var dlg = new Window();

            switch (o)
            {

                case UserCreationFormViewModel:
                    dlg = new UserCreationForm();
                    break;

                case ClientCreationFormViewModel:
                    dlg = new ClientCreationForm();
                    break;

                case UserListViewModel:
                    dlg = new UserListView();
                    break;

                case ClientListViewModel:
                    dlg = new ClientListView();
                    break;
            }

            dlg.Owner = _owner;
            dlg.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            if (dlg.ShowDialog() != true) return false;
            return true;
        }
    }
}

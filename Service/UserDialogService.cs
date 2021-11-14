using System.Linq;
using System.Windows;
using Homework_13.Service.Interfaces;
using Homework_13.View;
using Homework_13.View.UserControls;

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
            if (o.GetType() == typeof(Window)) 
            { GenericWindowOpenerMethod(o as Window); }
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
        private static bool GenericWindowOpenerMethod(Window window)
        {
            var dlg = new Window();

            switch (window)
            {
                case LoginFormWindow:
                    dlg = new LoginFormWindow();
                    break;

                case UserCreationForm:
                    dlg = new UserCreationForm();
                    break;

                case UserListView:
                    dlg = new UserListView();
                    break;

                case ClientListView:
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

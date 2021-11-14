using System.Linq;
using System.Windows;
using Homework_13.Model;
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
            switch (o)
            {
                case User user:
                    return OpenLoginForm(user);

                case UserCreationForm window:
                    return OpenUserCreationForm(window);

                case UserListView window:
                    return OpenUserList(window);

                case ClientListView window:
                    return OpenClientList(window);
            }
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

        public static bool OpenLoginForm(User user)
        {
            var dlg = new LoginFormWindow()
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            if (dlg.ShowDialog() != true) return false;
            return true;
        }

        private static bool OpenUserCreationForm(UserCreationForm window)
        {
            var dlg = new UserCreationForm()
            {
                Owner = _owner,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            if (dlg.ShowDialog() != true) return false;
            return true;
        }

        private static bool OpenUserList(UserListView window)
        {
            var dlg = new UserListView()
            {
                Owner = _owner,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            if (dlg.ShowDialog() != true) return false;
            return true;
        }

        private static bool OpenClientList(ClientListView window)
        {
            var dlg = new ClientListView()
            {
                Owner = _owner,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            if (dlg.ShowDialog() != true) return false;
            return true;
        }
    }
}

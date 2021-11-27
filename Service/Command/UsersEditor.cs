using System.Windows;
using Homework_13.Model;
using Homework_13.View.Windows;
using Homework_13.ViewModel;

namespace Homework_13.Service.Command
{
    class UsersEditor : Command
    {
        public override bool CanExecute(object parameter) =>
            parameter is UserEditingForm;

        public override void Execute(object parameter)
        {
            UserEditingForm dlg = parameter as UserEditingForm;

            if (CanExecute(parameter))
            {
                User user = UserListViewModel.SelectedUser;
                user.Name = dlg.userNameField.Text;
                if (dlg.userPassFieldHelper.Text == dlg.userSecondPassFieldHelper.Text && 
                    dlg.userPassFieldHelper.Text != user.Pass)
                {
                    user.Pass = dlg.userPassFieldHelper.Text;
                    ShowInformation("Пароль успешно изменен!", "Пароль изменен");
                }
                else if (dlg.userPassFieldHelper.Text != dlg.userSecondPassFieldHelper.Text &&
                    dlg.userPassFieldHelper.Text != user.Pass ||
                    dlg.userSecondPassFieldHelper.Text != user.Pass)
                {
                    ShowError("Пароль не удалось изменить!", "Ошибка");
                }

                user.CanCreateUsers = (bool)dlg.createUsers.IsChecked;
                user.CanRemoveUsers = (bool)dlg.removeUsers.IsChecked;
                user.HaveUserEditRights = (bool)dlg.userEdit.IsChecked;

                user.CanCreateClients = (bool)dlg.createClients.IsChecked;
                user.CanRemoveClients = (bool)dlg.removeClients.IsChecked;
                user.HaveAccessToClientsDB = (bool)dlg.accessClientsDB.IsChecked;
                user.CanOpenDebitAccounts = (bool)dlg.debit.IsChecked;
                user.CanOpenDepositAccounts = (bool)dlg.deposit.IsChecked;
                user.CanOpenCreditAccounts = (bool)dlg.credit.IsChecked;
                user.CanCloseAccounts = (bool)dlg.closeAccs.IsChecked;

                user.HaveAccessToAppSettings = (bool)dlg.appSettings.IsChecked;

                dlg.Close();
            }
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

    }
}

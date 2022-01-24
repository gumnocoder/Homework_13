using BankModelLibrary;
using Homework_13.View.Windows;
using Homework_13.ViewModel;
using static Homework_13.Service.InformationDialogService;

namespace Homework_13.Service.Command
{
    /// <summary>
    /// Команда редактирования пользователя
    /// </summary>
    class UsersEditor : Command
    {
        public override bool CanExecute(object parameter) =>
            parameter is UserEditingForm;

        /// <summary>
        /// проверяет параметр на возможность изменения
        /// </summary>
        /// <param name="Field"></param>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        private bool CheckField(string Field, string Parameter) =>
            Field != string.Empty && Field != Parameter;

        public override void Execute(object parameter)
        {
            UserEditingForm dlg = parameter as UserEditingForm;

            User user = UserListViewModel.SelectedUser;

            /// Меняет имя пользователя
            if (CheckField(dlg.userNameField.Text, user.Name)) user.Name = dlg.userNameField.Text;
            /// Меняет пароль 
            if (CheckField(dlg.userPassFieldHelper.Text, user.Pass))
            {
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
                    ShowError("Пароль не удалось изменить!");
                }
            }

            /// привязывает права пользователей к checkBox`ам
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
}

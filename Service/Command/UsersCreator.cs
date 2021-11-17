using System;
using System.Diagnostics;
using System.Windows;
using Homework_13.Model;
using Homework_13.View.UserControls;

namespace Homework_13.Service.Command
{
    class UsersCreator : Command
    {
        private bool Check(string field)
        {
            if (!string.IsNullOrEmpty(field)) return true;
            else return false;
        }
        public override bool CanExecute(object parameter)
        {
            return (parameter as UserCreationForm) != null;
        }

        public void CreateUser(UserCreationForm window, string Name, string Pass, string Login, string Type)
        {
            User user = new(Name, Pass, Login, Type);

            user.CanCreateUsers = (bool)window.canCreateUsers.IsChecked;
            user.CanRemoveUsers = (bool)window.canRemoveUsers.IsChecked;
            user.HaveUserEditRights = (bool)window.haveUserEditRights.IsChecked;
            user.CanCreateClients = (bool)window.canCreateClients.IsChecked;
            user.CanRemoveClients = (bool)window.canRemoveClients.IsChecked;
            user.CanOpenDebitAccounts = (bool)window.canOpenDebitAccounts.IsChecked;
            user.CanOpenDepositAccounts = (bool)window.canOpenDepositAccounts.IsChecked;
            user.CanOpenCreditAccounts = (bool)window.canOpenCreditAccounts.IsChecked;
            user.CanCloseAccounts = (bool)window.canCloseAccounts.IsChecked;
            user.HaveAccessToClientsDB = (bool)window.haveAccessToClientsDB.IsChecked;
            user.HaveAccessToAppSettings = (bool)window.haveAccessToAppSettings.IsChecked;

            ListsOperator<User> oper = new();
            oper.AddToList(UserList<User>.UsersList, user);

            Debug.WriteLine(user);
            Debug.WriteLine(user.CanCreateUsers);
            Debug.WriteLine(user.HaveAccessToClientsDB);
            Debug.WriteLine(user.HaveAccessToAppSettings);
        }
        public override void Execute(object parameter)
        {
            var window = parameter as UserCreationForm;

            string _name = window.NameField.Text;
            string _pass = window.passHelperField.Text;
            string _login = window.LoginField.Text;
            string _type = window.TypesComboBox.Text;

            if (CanExecute(parameter))
            {
                if (Check(_name))
                {
                    if (Check(_pass) && _pass != "*****")
                    {
                        if (Check(_login))
                        {
                            if (Check(_type))
                            {
                                CreateUser(window, _name, _pass, _login, _type);
                                MessageBox.Show(
                                    "Пользователь успешно создан!",
                                    "Отчёт",
                                    MessageBoxButton.OK);
                                window.Close();
                            }
                            else
                            {
                                MessageBox.Show(
                                    "Выберите тип пользователя!",
                                    "отсутствие данных",
                                    MessageBoxButton.OK);
                            }
                        }
                        else MessageBox.Show(
                            "Укажите логин!",
                            "отсутствие данных",
                            MessageBoxButton.OK);
                    }
                    else MessageBox.Show(
                        "Введите пароль!",
                        "отсутствие данных",
                        MessageBoxButton.OK);
                }
                else MessageBox.Show(
                    "Введите имя!",
                    "отсутствие данных",
                    MessageBoxButton.OK);
            }
        }
    }
}

using System;
using System.Diagnostics;
using System.Windows;
using Homework_13.Model;
using Homework_13.Model.bankModel;
using Homework_13.View.UserControls;
using Homework_13.View.Windows;

namespace Homework_13.Service.Command
{
    class PersonsCreator : Command
    {
        private bool Check(string field)
        {
            if (!string.IsNullOrEmpty(field)) return true;
            else return false;
        }
        public override bool CanExecute(object parameter)
        {
            return (parameter as Window) != null;
        }

        private bool CreateClientFieldsCheck(ClientCreationForm window)
        {
            string _surname = window.surnameField.Text;
            string _name = window.nameField.Text;
            string _patronim = window.patronimField.Text;
            string _type = window.clientTypeField.Text;

            if (!Check(_surname))
            {
                MessageBox.Show("Введите фамилию!", "отсутствие данных", MessageBoxButton.OK);
                return false;
            }
            else if (!Check(_name))
            {
                MessageBox.Show("Введите имя!", "отсутствие данных", MessageBoxButton.OK);
                return false;
            }
            else if (!Check(_type))
            {
                MessageBox.Show("Выберите тип клиента!", "отсутствие данных", MessageBoxButton.OK);
                return false;
            }
            else
            {
                string name = $"{_surname.Trim()} {_name.Trim()}";
                if (Check(_patronim)) name += $" {_patronim.Trim()}";
                CreateClient(window, name, _type);
                return true;
            }
        }
        public void CreateClient(ClientCreationForm window, string name, string type)
        {
            bool createAccount = (bool)window.CreateDebitAccountFlag.IsChecked;
            Client client = new(name, type);

            if (createAccount) new BankDebitAccount(client);

            ListsOperator<Client> oper = new();
            oper.AddToList(ClientList<Client>.ClientsList, client);

            MessageBox.Show(
                "Клиент успешно создан!",
                "Отчёт",
                MessageBoxButton.OK);
            window.Close();
        }
        public bool CreateUserFieldsCheck(UserCreationForm window)
        {
            string _name =
                window.NameField.Text;
            string _pass =
                window.passHelperField.Text;
            string _login =
                window.LoginField.Text;
            string _type =
                window.TypesComboBox.Text;

            if (!Check(_name))
            {
                MessageBox.Show("Введите имя!", "отсутствие данных", MessageBoxButton.OK);
                return false;
            }
            else if (!Check(_pass) || _pass == "*****")
            {
                MessageBox.Show("Введите пароль!", "отсутствие данных", MessageBoxButton.OK);
                return false;
            }
            else if (!Check(_login))
            {
                MessageBox.Show("Укажите логин!", "отсутствие данных", MessageBoxButton.OK);
                return false;
            }
            else if (!Check(_type))
            {
                MessageBox.Show("Выберите тип пользователя!", "отсутствие данных", MessageBoxButton.OK);
                return false;
            }
            else { CreateUser(window, _name, _pass, _login, _type); return true; }
        }

        public void CreateUser(UserCreationForm window, string Name, string Pass, string Login, string Type)
        {

            User user = new(Name, Login, Pass, Type);

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

            MessageBox.Show(
                "Пользователь успешно создан!",
                "Отчёт",
                MessageBoxButton.OK);
            window.Close();
        }
        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter)) return;
            Window window = parameter as Window;

            switch (parameter)
            {
                case UserCreationForm:
                    window = parameter as UserCreationForm;
                    CreateUserFieldsCheck(window as UserCreationForm);
                    break;
                case ClientCreationForm:
                    window = parameter as ClientCreationForm;
                    CreateClientFieldsCheck(window as ClientCreationForm);
                    break;
            }
        }
    }
}

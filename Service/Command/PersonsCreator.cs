using System.Diagnostics;
using System.Windows;
using Homework_13.Model;
using Homework_13.Model.bankModel;
using Homework_13.View.UserControls;
using Homework_13.View.Windows;
using static Homework_13.Service.InformationDialogService;
using static Homework_13.Model.bankModel.Bank;
using static Homework_13.Service.HudViewer;

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

        /// <summary>
        /// Выполняет проверки полей запускает метод создания клиента
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        private bool CreateClientFieldsCheck(ClientCreationForm window)
        {
            string _surname = window.surnameField.Text;
            string _name = window.nameField.Text;
            string _patronim = window.patronimField.Text;
            string _type = window.clientTypeField.Text;

            if (!Check(_surname))
            {
                ShowError("Введите фамилию!");
                return false;
            }
            else if (!Check(_name))
            {
                ShowError("Введите имя!");
                return false;
            }
            else if (!Check(_type))
            {
                ShowError("Выберите тип клиента!");
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


        /// <summary>
        /// создаёт клиента и открывает на него дебетовый счёт при необходимости
        /// </summary>
        /// <param name="window"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public void CreateClient(ClientCreationForm window, string name, string type)
        {
            Client client = new(name, type);
            EventAction += ShowHudWindow;
            OnEventAction($"создан клиент {client}");
            /// проверяет потребность в открытии дебетового счёта и открывает его если true
            bool createAccount = (bool)window.CreateDebitAccountFlag.IsChecked;
            if (createAccount) new BankDebitAccount(client);

            ListsOperator<Client> oper = new();
            oper.AddToList(ClientList<Client>.ClientsList, client);

            ShowInformation(
                "Клиент успешно создан!",
                "Отчёт");
            Debug.WriteLine(client);

            foreach (var e in ClientList<Client>.ClientsList) Debug.WriteLine(e);
            window.Close();
        }

        /// <summary>
        /// Выполняет проверки полей и запускает метод создания пользователя
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
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

            /// выполняет проверки необходимых полей
            if (!Check(_name))
            {
                ShowError("Введите имя!");
                return false;
            }
            else if (!Check(_pass) || _pass == "*****")
            {
                ShowError("Введите пароль!");
                return false;
            }
            else if (!Check(_login))
            {
                ShowError("Укажите логин!");
                return false;
            }
            else if (!Check(_type))
            {
                ShowError("Выберите тип пользователя!");
                return false;
            }
            /// запускает метод создания пользователя в случае выполнения проверок
            else 
            { 
                CreateUser(window, _name, _pass, _login, _type); 
                return true; 
            }
        }

        /// <summary>
        /// Создаёт пользователя
        /// </summary>
        /// <param name="window">UserCreationForm</param>
        /// <param name="Name">Имя пользователя</param>
        /// <param name="Pass">пароль</param>
        /// <param name="Login">логин</param>
        /// <param name="Type">тип</param>
        public void CreateUser(
            UserCreationForm window, 
            string Name,
            string Pass, 
            string Login, 
            string Type)
        {

            User user = new(Name, Login, Pass, Type);

            /// выдает права пользователю в соответствии галочкам
            user.CanCreateUsers = 
                (bool)window.canCreateUsers.IsChecked;
            user.CanRemoveUsers =
                (bool)window.canRemoveUsers.IsChecked;
            user.HaveUserEditRights =
                (bool)window.haveUserEditRights.IsChecked;
            user.CanCreateClients = 
                (bool)window.canCreateClients.IsChecked;
            user.CanRemoveClients =
                (bool)window.canRemoveClients.IsChecked;
            user.CanOpenDebitAccounts =
                (bool)window.canOpenDebitAccounts.IsChecked;
            user.CanOpenDepositAccounts = 
                (bool)window.canOpenDepositAccounts.IsChecked;
            user.CanOpenCreditAccounts =
                (bool)window.canOpenCreditAccounts.IsChecked;
            user.CanCloseAccounts = 
                (bool)window.canCloseAccounts.IsChecked;
            user.HaveAccessToClientsDB =
                (bool)window.haveAccessToClientsDB.IsChecked;
            user.HaveAccessToAppSettings = 
                (bool)window.haveAccessToAppSettings.IsChecked;

            ListsOperator<User> oper = new();
            oper.AddToList(UserList<User>.UsersList, user);

            ShowInformation(
                "Пользователь успешно создан!",
                "Отчёт");

            window.Close();
        }

        /// <summary>
        /// Запускает нужный сценарий (создание клиента или пользователя)
        /// в соответствии с типом передаваемого параметра
        /// </summary>
        /// <param name="parameter">Client или User</param>
        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter)) return;
            Window window = parameter as Window;

            switch (parameter)
            {
                /// Создание пользователя
                case UserCreationForm:
                    window = parameter as UserCreationForm;
                    CreateUserFieldsCheck(window as UserCreationForm);
                    break;

                /// Создание клиента
                case ClientCreationForm:
                    window = parameter as ClientCreationForm;
                    CreateClientFieldsCheck(window as ClientCreationForm);
                    break;
            }
        }
    }
}

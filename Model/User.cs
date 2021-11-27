using Homework_13.Model.Interfaces;
using static Homework_13.Model.bankModel.Bank;

namespace Homework_13.Model
{
    class User : BasicUserWithPermissions, IPasswordHolder
    {
        private long _id;
        private string _login, _pass, _type;
        public long ID { get => _id; protected set { _id = value; OnPropertyChanged(); } }
        public string Login { get => _login; set { _login = value; OnPropertyChanged(); } }
        public string Pass { get => _pass; set { _pass = value; OnPropertyChanged(); } }
        public string Type { get => _type; set { _type = value; OnPropertyChanged(); } }

        public User() { }

        public User(string Name, string Login, string Pass, string Type)
        {
            ID = ++ThisBank.CurrentUserID;
            this.Name = Name;
            this.Login = Login;
            this.Pass = Pass;
            this.Type = Type;
        }

        public User(User user)
        {
            Name = user.Name;
            Login = user.Login;
            Pass = user.Pass;
            Type = user.Type;

            CanCreateUsers = user.CanCreateUsers;
            CanRemoveUsers = user.CanRemoveUsers;
            HaveUserEditRights = user.HaveUserEditRights;

            CanOpenDebitAccounts = user.CanOpenDebitAccounts;
            CanOpenDepositAccounts = user.CanOpenDepositAccounts;
            CanOpenCreditAccounts = user.CanOpenCreditAccounts;
            CanCloseAccounts = user.CanCloseAccounts;
            CanCreateClients = user.CanCreateClients;
            HaveAccessToClientsDB = user.HaveAccessToClientsDB;
            CanRemoveClients = user.CanRemoveClients;

            HaveAccessToAppSettings = user.HaveAccessToAppSettings;
        }

        public override string ToString()
        {
            return $"{ID}, {Name} [{Type}]";
        }
    }
}

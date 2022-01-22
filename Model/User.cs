using System.Diagnostics;
using Homework_13.Model.Interfaces;
using static Homework_13.Model.bankModel.Bank;

namespace Homework_13.Model
{
    class User : BasicUserWithPermissions, IPasswordHolder, IIdentificable
    {
        #region Конструкторы
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public User() { }

        /// <summary>
        /// Конструктор 4 параметра
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Login"></param>
        /// <param name="Pass"></param>
        /// <param name="Type"></param>
        public User(string Name, string Login, string Pass, string Type)
        {
            SetID();
            this.Name = Name;
            this.Login = Login;
            this.Pass = Pass;
            this.Type = Type;
        }

        /// <summary>
        /// конструктор копирования
        /// </summary>
        /// <param name="user">копируемый экземпляр</param>
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
        #endregion

        #region Поля
        private long _id;
        private string _login, _pass, _type;
        #endregion

        #region Свойства
        /// <summary>
        /// ID
        /// </summary>
        public long ID 
        { 
            get => _id; 
            set 
            { 
                _id = value; 
                OnPropertyChanged();
            } 
        }
        /// <summary>
        /// Логин
        /// </summary>
        public string Login
        { 
            get => _login; 
            set
            {
                _login = value; 
                OnPropertyChanged(); 
            } 
        }
        /// <summary>
        /// Пароль
        /// </summary>
        public string Pass
        { 
            get => _pass; 
            set
            { 
                _pass = value;
                OnPropertyChanged(); 
            } 
        }
        /// <summary>
        /// Тип пользователя
        /// </summary>
        public string Type
        {
            get => _type; 
            set
            { 
                _type = value; 
                OnPropertyChanged(); 
            }
        }
        #endregion

        #region Методы
        public override string ToString()
        {
            return $"{ID}, {Name} [{Type}]";
        }

        public void SetID()
        {
            ID = ++ThisBank.CurrentUserID;
        }

        ~User() { Debug.WriteLine("Вызван деструктор класса User"); }
        #endregion
    }
}

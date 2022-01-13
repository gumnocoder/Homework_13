namespace Homework_13.Model
{
    abstract class BasicUserWithPermissions : Person
    {
        #region Поля
        private bool
            _canCreateUsers = false,
            _canCreateClients = false,
            _canRemoveUsers = false,
            _canRemoveClients = false,
            _haveUserEditRights = false,
            _canCloseAccounts = false,
            _canOpenDebitAccounts = false,
            _canOpenCreditAccounts = false,
            _haveAccessToAppSettings = false,
            _haveAccessToClientsDB = false,
            _canOpenDepositAccounts = false;

        #endregion

        #region Свойства
        /// <summary>
        /// Права доступа к настрокам приложения
        /// </summary>
        public bool HaveAccessToAppSettings
        {
            get => _haveAccessToAppSettings;
            set { _haveAccessToAppSettings = value; OnPropertyChanged(); }
        }
        /// <summary>
        ///  Возможность выдавать кредиты
        /// </summary>
        public bool CanOpenCreditAccounts
        {
            get => _canOpenCreditAccounts;
            set { _canOpenCreditAccounts = value; OnPropertyChanged(); }
        }
        /// <summary>
        /// Возможность открывать дебетовые счета
        /// </summary>
        public bool CanOpenDebitAccounts
        {
            get => _canOpenDebitAccounts;
            set { _canOpenDebitAccounts = value; OnPropertyChanged(); }
        }
        /// <summary>
        /// Возможность закрывать счета
        /// </summary>
        public bool CanCloseAccounts
        {
            get => _canCloseAccounts;
            set { _canCloseAccounts = value; OnPropertyChanged(); }
        }
        /// <summary>
        /// Возможность редактировать пользователя
        /// </summary>
        public bool HaveUserEditRights
        {
            get => _haveUserEditRights;
            set { _haveUserEditRights = value; OnPropertyChanged(); }
        }
        /// <summary>
        /// Возможность создавать пользователей
        /// </summary>
        public bool CanCreateUsers
        {
            get => _canCreateUsers;
            set { _canCreateUsers = value; OnPropertyChanged(); }
        }
        /// <summary>
        /// Возможность создавать клиентов
        /// </summary>
        public bool CanCreateClients
        {
            get => _canCreateClients;
            set { _canCreateClients = value; OnPropertyChanged(); }
        }
        /// <summary>
        /// Возможность удалять пользователей
        /// </summary>
        public bool CanRemoveUsers
        {
            get => _canRemoveUsers;
            set { _canRemoveUsers = value; OnPropertyChanged(); }
        }
        /// <summary>
        /// Возможность удалять клиентов
        /// </summary>
        public bool CanRemoveClients
        {
            get => _canRemoveClients;
            set { _canRemoveClients = value; OnPropertyChanged(); }
        }
        /// <summary>
        /// Права доступа к клиентской базе
        /// </summary>
        public bool HaveAccessToClientsDB
        {
            get => _haveAccessToClientsDB;
            set { _haveAccessToClientsDB = value; OnPropertyChanged(); }
        }
        /// <summary>
        /// Возможность открывать депозитные счета
        /// </summary>
        public bool CanOpenDepositAccounts
        {
            get => _canOpenDepositAccounts;
            set
            {
                _canOpenDepositAccounts = value;
                OnPropertyChanged();
            }
        }
        #endregion
        /// <summary>
        /// Смена логического параметра на противоположный
        /// </summary>
        /// <param name="property"></param>
        public void Turn(bool property) => property = !property;
    }
}

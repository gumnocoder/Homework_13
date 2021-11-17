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
        public bool HaveAccessToAppSettings
        {
            get => _haveAccessToAppSettings;
            set { _haveAccessToAppSettings = value; OnPropertyChanged(); }
        }
        public bool CanOpenCreditAccounts
        {
            get => _canOpenCreditAccounts;
            set { _canOpenCreditAccounts = value; OnPropertyChanged(); }
        }
        public bool CanOpenDebitAccounts
        {
            get => _canOpenDebitAccounts;
            set { _canOpenDebitAccounts = value; OnPropertyChanged(); }
        }
        public bool CanCloseAccounts
        {
            get => _canCloseAccounts;
            set { _canCloseAccounts = value; OnPropertyChanged(); }
        }
        public bool HaveUserEditRights
        {
            get => _haveUserEditRights;
            set { _haveUserEditRights = value; OnPropertyChanged(); }
        }

        public bool CanCreateUsers
        {
            get => _canCreateUsers;
            set { _canCreateUsers = value; OnPropertyChanged(); }
        }

        public bool CanCreateClients
        {
            get => _canCreateClients;
            set { _canCreateClients = value; OnPropertyChanged(); }
        }

        public bool CanRemoveUsers
        {
            get => _canRemoveUsers;
            set { _canRemoveUsers = value; OnPropertyChanged(); }
        }

        public bool CanRemoveClients
        {
            get => _canRemoveClients;
            set { _canRemoveClients = value; OnPropertyChanged(); }
        }

        public bool HaveAccessToClientsDB
        {
            get => _haveAccessToClientsDB;
            set { _haveAccessToClientsDB = value; OnPropertyChanged(); }
        }

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
        public void Turn(bool property) => property = !property;
    }
}

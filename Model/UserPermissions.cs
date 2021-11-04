using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_13.Model
{
    abstract class UserPermissions : BaseModel
    {
        #region Поля
        private bool _canCreateUsers = false;
        private bool _canCreateClients = false;
        private bool _canRemoveUsers = false;
        private bool _canRemoveClients = false;
        private bool _haveUserEditRights = false;
        private bool _canCloseAccounts = false;
        private bool _canOpenDebitAccounts = false;
        private bool _canOpenCreditAccounts = false;
        private bool _haveAccessToAppSettings = false;
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
        #endregion
        public void Turn(bool property) => property = !property;
    }
}

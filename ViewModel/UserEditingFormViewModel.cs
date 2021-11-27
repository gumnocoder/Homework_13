using Homework_13.Model;

namespace Homework_13.ViewModel
{
    class UserEditingFormViewModel : BaseViewModel
    {
        private string _pass;
        private string _passConfirm;
        private static User _editableUser;
        public string Pass
        {
            get => _pass;
            set
            {
                _pass = value;
                OnPropertyChanged();
            }
        }

        public string PassConfirm
        {
            get => _passConfirm;
            set
            {
                _passConfirm = value;
                OnPropertyChanged();
            }
        }

        public static User EditableUser
        {
            get => _editableUser;
            set
            {
                _editableUser = value;
            }
        }

        public UserEditingFormViewModel(User user)
        {
            EditableUser = new(user);

            Pass = user.Pass;
            PassConfirm = Pass;
        }

        public UserEditingFormViewModel() { }
    }
}

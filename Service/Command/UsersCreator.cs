using System;
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
            var window = parameter as UserCreationForm;
            if (window != null)
            {
                if (Check(window.NameField.Text))
                {
                    if (Check(window.passHelperField.Text))
                    {
                        if (Check(window.LoginField.Text))
                        {
                            if (Check(window.TypesComboBox.Text))
                            {
                                return true;
                            }
                            else return false;
                        }
                        else return false;
                    }
                    else return false;
                }
                else return false;
            }
            return false;
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
                User user = new(_name, _login, _pass, _type);
                ListsOperator<User> oper = new();
                oper.AddToList(UserList<User>.UsersList, user);
            }
        }
    }
}

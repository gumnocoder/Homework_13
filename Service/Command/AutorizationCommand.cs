using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Homework_13.Model;
using Homework_13.View;
using Homework_13.ViewModel;

namespace Homework_13.Service.Command
{
    class AutorizationCommand : Command
    {
        public bool? DialogResult { get; set; }

        public override bool CanExecute(object parameter)
        {
            if (parameter is LoginFormWindow)
            {
                var login = (parameter as LoginFormWindow).loginFieldValue.Text;
                var pass = (parameter as LoginFormWindow).uuu.Text;

                foreach (User u in UserList<User>.UsersList)
                {
                    if (u.Login == login && u.Pass == pass)
                    {
                        MainWindowViewModel.CurrentUser = u;
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine("User not found!");
                    }
                }
            }
            return false;
        }

        public override void Execute(object parameter)
        {
            var window = (Window)parameter;
            window.DialogResult = DialogResult;

            if (CanExecute(parameter))
            {
                window.Close();
            }
        }
    }
}

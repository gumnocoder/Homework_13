using System;
using System.Windows;
using Homework_13.Model;
using Homework_13.Service.Interfaces;
using Homework_13.View;

namespace Homework_13.Service
{
    class UserDialogService : IUserDialogService
    {
        public bool Confirm(string Message, string Tittle, bool Choice = false) =>MessageBox.Show(Message, Tittle, MessageBoxButton.YesNo) == MessageBoxResult.Yes;

        public bool Edit(object o)
        {
            switch (o)
            {
                case (User user):
                    return OpenLoginForm(user);
            }

            return true;
        }

        public void ShowError(string Message, string Tittle)
        {
            MessageBox.Show(Message, Tittle, MessageBoxButton.OK);
        }

        public void ShowInformation(string Message, string Tittle)
        {
            MessageBox.Show(Message, Tittle, MessageBoxButton.OK);
        }

        public static bool OpenLoginForm(User user)
        {
            LoginFormWindow loginForm = new();
            loginForm.ShowDialog();
            loginForm.loginFieldValue.Text = user.Login;
            loginForm.uuu.Text = user.Pass;
            return true;
        }
    }
}

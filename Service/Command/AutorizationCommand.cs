using System;
using System.Diagnostics;
using System.Windows;
using Homework_13.Model;
using Homework_13.View;
using Homework_13.ViewModel;

namespace Homework_13.Service.Command
{
    /// <summary>
    /// Выполняет авторизацию по паре логин - пароль
    /// </summary>
    class AutorizationCommand : Command
    {
        public bool? DialogResult { get; set; }

        /// <summary>
        /// Проверяет возможность авторизации
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public override bool CanExecute(object parameter)
        {
            
            /// путём сличения значений в окне авторизации
            if (parameter is LoginFormWindow)
            {
                /// в поле логин
                var login = (parameter as LoginFormWindow).loginFieldValue.Text;
                /// и в поле пароль, передаваемый из passwordbox
                var pass = (parameter as LoginFormWindow).uuu.Text;

                /// сверяет параметры с каждой записью в листе
                /// и при соответствии записывает соответствующую запись
                /// в статическую переменную CurrentUser
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

        /// <summary>
        /// Выполнение цикла авторизации
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            var window = (Window)parameter;
            window.DialogResult = DialogResult;

            EventAction += HudViewer.ShowHudWindow;
            HistoryEventAction += LogWriter.WriteToLog;

            /// в случае успешной авторизации закрывает окно
            if (CanExecute(parameter))
            {
                OnEventAction($"Пользователь {MainWindowViewModel.CurrentUser} вошёл в систему", true, false);
                OnHistoryEventAction($"{DateTime.UtcNow} : Вход в систему : {MainWindowViewModel.CurrentUser}");
                window.Close();
            }
        }
    }
}

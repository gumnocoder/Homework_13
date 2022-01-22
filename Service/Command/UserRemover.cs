using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework_13.Model;
using Homework_13.ViewModel;

namespace Homework_13.Service.Command
{
    class UserRemover : Command
    {
        public override bool CanExecute(object parameter) => 
            parameter as User != null && parameter as User != MainWindowViewModel.CurrentUser;

        public override void Execute(object parameter)
        {
            EventAction += HudViewer.ShowHudWindow;
            HistoryEventAction += LogWriter.WriteToLog;

            foreach (User user in UserList<User>.UsersList)
                if (user == parameter as User)
                {
                    OnEventAction($"пользователь удалён", true, false);
                    OnHistoryEventAction($"пользователь {user} удалён");
                    UserList<User>.UsersList.Remove(user);
                    break;
                }
        }
    }
}

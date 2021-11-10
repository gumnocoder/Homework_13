using System;
using Homework_13.Service.Command;

namespace Homework_13.ViewModel
{
    class LoginFormWindowViewModel : BaseViewModel
    {
        private string _login = "логин";
        public string Login { get => _login; set => _login=value; }

        public string Pass { get; set; } = "12323";

        private RelayCommand _closeAppBtnClick;

        public RelayCommand CloseAppBtnClick =>
            _closeAppBtnClick ??= new(CloseAppCommand);

        private void CloseAppCommand(object s)
        {
            Environment.Exit(0);
        }
    }
}

namespace Homework_13.ViewModel
{
    class LoginFormWindowViewModel : BaseViewModel
    {
        #region Поля

        private string _login = "admin";

        private string _pass = "admin";

        #endregion

        #region Свойства
        public string Login { get => _login; set => _login=value; }

        public string Pass { get => _pass; set => _pass = value; }

        #endregion
    }
}

namespace Homework_13.ViewModel
{
    class ClientCreationFormViewModel : BaseViewModel
    {
        #region Поля
        string _type;

        string[] _clientTypes = new string[] { "обычный", "V.I.P.", "компания" };

        #endregion

        #region Свойства

        public string Type { get => _type; set { _type = value; OnPropertyChanged(); } }
        public string[] ClientTypes { get => _clientTypes; }

        #endregion
    }
}

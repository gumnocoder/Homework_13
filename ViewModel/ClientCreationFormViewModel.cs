namespace Homework_13.ViewModel
{
    class ClientCreationFormViewModel : BaseViewModel
    {
        string _surname, _name, _partonim, _type;

        string[] _clientTypes = new string[] { "обычный", "V.I.P.", "компания" };

        public string Surname { get => _surname; set { _surname = value; OnPropertyChanged(); } }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public string Patronim { get => _partonim; set { _partonim = value; OnPropertyChanged(); } }
        public string Type { get => _type; set { _type = value; OnPropertyChanged(); } }

        public string[] ClientTypes { get => _clientTypes; }
    }
}

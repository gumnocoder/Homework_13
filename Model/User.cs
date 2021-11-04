using Homework_13.Model.Interfaces;

namespace Homework_13.Model
{
    public class User : UserPermissions, INamedObject, IPasswordHolder
    {
        private long _id;
        private string _name;
        private string _login;
        private string _pass;
        private string _type;
        public long ID { get => _id; }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public string Login { get => _login; set { _login = value; OnPropertyChanged(); } }
        public string Pass { get => _pass; set { _pass = value; OnPropertyChanged(); } }
        public string Type { get => _type; set { _type = value; OnPropertyChanged(); } }

        public User() 
        { 
        }

        public User(string Name, string Login, string Pass, string Type)
        {
            this.Name = Name;
            this.Login = Login;
            this.Pass = Pass;
            this.Type = Type;
        }

        public override string ToString()
        {
            return $"{ID}, {Name} [{Type}]";
        }
    }
}

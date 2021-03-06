using Homework_13.Model.Interfaces;

namespace Homework_13.Model
{
    public abstract class Person : BaseModel, INamedObject
    {
        private string _name;
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
    }
}

using Homework_13.Model;
using Homework_13.Service.Command;

namespace Homework_13.ViewModel
{
    class ClientEditingFormViewModel : BaseViewModel
    {
        private string _name;
        private string _type;
        private string[] _clientTypesEdit = new string[] { "обычный", "V.I.P.", "компания" };
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
            }
        }

        public string Type
        {
            get => _type;
            set
            {
                _type = value;
            }
        }

        public string[] ClientTypesEdit { get => _clientTypesEdit; }

        public ClientEditingFormViewModel(Client client)
        {

            Name = client.Name;
            Type = client.Type;
        }

        public ClientEditingFormViewModel() { }
    }
}

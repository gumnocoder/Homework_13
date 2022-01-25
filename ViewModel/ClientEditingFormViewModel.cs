
using BankModelLibrary;
using BankModelLibrary.BankServices;
using BankModelLibrary.BaseBankModels;

namespace Homework_13.ViewModel
{
    class ClientEditingFormViewModel : BaseViewModel
    {
        #region Конструкторы
        public ClientEditingFormViewModel(Client client)
        {

            Name = client.Name;
            Type = client.Type;
        }

        public ClientEditingFormViewModel() { }
        #endregion

        #region Поля
        private string _name;
        private string _type;
        private string[] _clientTypesEdit = new string[] { "обычный", "V.I.P.", "компания" };
        #endregion

        #region Свойства
        /// <summary>
        /// Имя клиента
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        /// <summary>
        /// тип клиента
        /// </summary>
        public string Type
        {
            get => _type;
            set => _type = value;
        }
        /// <summary>
        /// список типов клиентов
        /// </summary>
        public string[] ClientTypesEdit { get => _clientTypesEdit; }
        #endregion
    }
}

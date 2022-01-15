using System.Collections.ObjectModel;
using Homework_13.Model.Interfaces;

namespace Homework_13.Model
{
    class ClientList<T> : ICollectionContainer<T> where T : Client
    {
        private static string _clientsPath = "clients.json";
        public static string ClientsPath 
        { 
            get => _clientsPath; 
            set 
            {
                _clientsPath = value; 
            } 
        }

        /// <summary>
        /// Список клиентов
        /// </summary>
        public static ObservableCollection<T> ClientsList = new();
    }
}

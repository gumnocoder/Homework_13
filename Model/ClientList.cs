using System.Collections.ObjectModel;
using Homework_13.Model.Interfaces;

namespace Homework_13.Model
{
    class ClientList<T> : ICollectionContainer<T> where T : Client
    {
        /// <summary>
        /// Список клиентов
        /// </summary>
        public static ObservableCollection<T> ClientsList = new();

        /// <summary>
        /// Поиск клиента по имени
        /// </summary>
        /// <param name="Name">Имя</param>
        /// <returns></returns>
        public static Client SearchByName(string Name)
        {
            Client t = default;

            if (ClientsList != null && ClientsList.Count > 0)
            {
                foreach (Client client in ClientsList)
                {
                    if (client.Name.ToLower() == Name.ToLower())
                    { t = client; break; }
                }
            }
            return t;
        }

        /// <summary>
        /// Поиск клиента по идентификатору
        /// </summary>
        /// <param name="ID">идентификатор</param>
        /// <returns></returns>
        public static Client SearchByID(long ID)
        {
            Client t = default;

            if (ClientsList != null && ClientsList.Count > 0)
            {
                foreach (Client client in ClientsList)
                {
                    if (client.ClientID == ID)
                    { t = client; break; }
                }
            }
            return t;
        }
    }
}

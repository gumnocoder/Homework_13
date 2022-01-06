using System.Collections.ObjectModel;
using Homework_13.Model.Interfaces;

namespace Homework_13.Model
{
    class ClientList<T> : ICollectionContainer<T> where T : Client
    {
        public static ObservableCollection<T> ClientsList = new();

        public Client SearchByName(string Name)
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
    }
}

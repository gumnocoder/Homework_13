using System.Collections.ObjectModel;
using Homework_13.Model.Interfaces;

namespace Homework_13.Model
{
    class ClientList<T> : ICollectionContainer<T> where T : Client
    {
        public static ObservableCollection<T> ClientsList = new();
        public T this[int ID]
        {
            get
            {
                T t = null;
                if (ClientsList != null && ClientsList.Count > 0)
                {
                    foreach (T c in ClientsList)
                    {
                        if (c.ClientID == ID)
                        { t = c; break; }
                    }
                }
                return t;
            }
        }

        public T this[string Name]
        {
            get
            {
                T u = default;

                if (ClientsList != null && ClientsList.Count > 0)
                {
                    foreach (T t in ClientsList)
                    {
                        if (t.Name.ToLower() == Name.ToLower())
                        { u = t; break; }
                    }
                }

                return u;
            }
        }
        public ClientList() { }
    }
}

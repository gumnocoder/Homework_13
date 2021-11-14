using System.Collections.ObjectModel;
using Homework_13.Model.Interfaces;

namespace Homework_13.Model
{
    class UserList<T> : ICollectionContainer<T> where T : User
    {
        public static ObservableCollection<T> UsersList = new();
        public T this[string Name]
        {
            get
            {
                T u = default;

                if (UsersList != null && UsersList.Count > 0)
                {
                    foreach (T t in UsersList)
                    {
                        if (t.Name.ToLower() == Name.ToLower())
                        { u = t; break; }
                    }
                }

                return u;
            }
        }
        public T this[int ID]
        {
            get
            {
                T u = default;

                if (UsersList != null && UsersList.Count > 0)
                {
                    foreach (T t in UsersList)
                    {
                        if (t.ID == ID)
                        { u = t; break; }
                    }
                }

                return u;
            }
        }
    }
}

using System.Collections.ObjectModel;

namespace Homework_13.Model.Interfaces
{
    interface ICollectionContainer<in T>
    {
        static ObservableCollection<T> ObjectsList;
    }
}

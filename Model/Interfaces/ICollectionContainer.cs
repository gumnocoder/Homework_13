using System.Collections.ObjectModel;

namespace Homework_13.Model.Interfaces
{
    interface ICollectionContainer<out T>
    {
        static ObservableCollection<T> ObjectsList;
    }
}

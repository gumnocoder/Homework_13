using System.Collections.ObjectModel;
using Homework_13.Model.Interfaces;

namespace Homework_13.Model
{
    public class ListsOperator<T> where T : INamedObject
    {
        private static bool CheckItemExistance(ObservableCollection<T> personsList, T person)
        {
            foreach (INamedObject e in personsList)
            {
                if (Equals(e, person)) { return true; }
                break;
            }
            return false;
        }

        public void AddToList(ObservableCollection<T> personsList, T person)
        {
            if (person != null && !CheckItemExistance(personsList, person))
            {
                personsList.Add(person);
            }
        }

        public void RemoveFromList(ObservableCollection<T> personsList, T person)
        {
            if (person != null && CheckItemExistance(personsList, person))
            {
                personsList.Remove(person);
            }
        }
    }
}

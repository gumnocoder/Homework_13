using Homework_13.Model.Interfaces;

namespace Homework_13.Model
{
    public class ObjectRenamer<T> where T : INamedObject
    {
        private readonly T _obj;
        private readonly string _newName;

        public ObjectRenamer(T obj, string newName)
        {
            this._obj = obj;
            this._newName = newName;
            ReName();
        }

        private void ReName() => _obj.Name = _newName;
    }
}

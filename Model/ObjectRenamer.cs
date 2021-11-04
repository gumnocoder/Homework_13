using Homework_13.Model.Interfaces;

namespace Homework_13.Model
{
    class ObjectRenamer<T> where T : INamedObject
    {
        readonly T obj;
        readonly string newName;

        public ObjectRenamer(T obj, string newName)
        {
            this.obj = obj;
            this.newName = newName;
            ReName();
        }

        private void ReName() => obj.Name = newName;
    }
}

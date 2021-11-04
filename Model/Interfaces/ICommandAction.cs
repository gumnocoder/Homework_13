namespace Homework_13.Model.Interfaces
{
    interface ICommandAction
    {
        void Execute();
        public bool Executed { get; }
    }
}

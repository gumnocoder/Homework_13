using System;
using System.Windows.Input;
using Homework_13.Model;

namespace Homework_13.Service.Command
{
    public abstract class Command : BaseEventSystem,  ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);
    }
}

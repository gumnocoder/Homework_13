using System;
using System.Windows.Input;

namespace Homework_13.Service.Command
{
    class SimpleCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action _canExecute;
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (_canExecute != null) _canExecute();
        }

        public SimpleCommand(Action action)
        {
            _canExecute = action;
        }
    }
}

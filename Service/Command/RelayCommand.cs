using System;

namespace Homework_13.Service.Command
{
    class RelayCommand : Command
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> Execute, Func<object, bool> CanExecute = null)
        {
            if (Execute != null) _execute = Execute;
            _canExecute = CanExecute;
        }
        public override bool CanExecute(object parameter) => 
            _canExecute?.Invoke(parameter) ?? true;

        public override void Execute(object parameter) => 
            _execute(parameter);  
    }
}

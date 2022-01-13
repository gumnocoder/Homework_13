using System;
using System.Windows;

namespace Homework_13.Service.Command
{
    /// <summary>
    /// Служит для закрытия открытых диалоговых окон
    /// </summary>
    class CloseWindowCommand : Command
    {
        public bool? DialogResult { get; set; }

        public override bool CanExecute(object parameter) => parameter is Window;

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter)) return;

            var window = (Window)parameter;
            window.DialogResult = DialogResult;
            window.Close();
            if (ExitApp) Environment.Exit(0);
        }

        public bool ExitApp { get; set; } = false;
    }
}

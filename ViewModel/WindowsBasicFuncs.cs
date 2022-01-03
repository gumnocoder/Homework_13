using System.Windows;
using static System.Windows.SystemParameters;
using Homework_13.Service.Command;

namespace Homework_13.ViewModel
{
    abstract class WindowsBasicFuncs : BaseViewModel
    {
        private bool _maximized = false;

        private double _width;
        private double _height;
        private double _top;
        private double _left;

        #region Команда для развёртывания окна на весь экран

        private RelayCommand _maximize;
        public RelayCommand Maximize
        {
            get => _maximize ??= new(MaximizeCommand);
        }

        /// <summary>
        /// Разворачивает окно на весь экран
        /// </summary>
        /// <param name="s"></param>
        private void MaximizeCommand(object s)
        {
            var window = (Window)s;

            if (!_maximized)
            {
                _width = window.Width;
                _height = window.Height;
                _top = window.Top;
                _left = window.Left;

                window.Top = 0;
                window.Left = 0;
                window.Width = PrimaryScreenWidth;
                window.Height = PrimaryScreenHeight;

                _maximized = true;
            }
            else
            {
                window.Width = _width;
                window.Height = _height;
                window.Top = _top;
                window.Left = _left;

                _maximized = false;
            }
        }

        #endregion
    }
}

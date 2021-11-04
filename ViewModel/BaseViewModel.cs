using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Homework_13.ViewModel
{
    /// <summary>
    /// базовая логика для viewmodels
    /// </summary>
    class BaseViewModel : DependencyObject, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "") => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        protected bool Set<T>(
            ref T field, 
            T value)
        {
            if (!Equals(field, value))
            {
                field = value;
                OnPropertyChanged();
                return true;
            }
            return false;
        }
    }
}

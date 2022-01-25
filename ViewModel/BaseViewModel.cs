using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Homework_13.ViewModel
{
    /// <summary>
    /// базовая логика для viewmodels
    /// </summary>
    public class BaseViewModel : DependencyObject, INotifyPropertyChanged
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

public abstract class BaseEventSystem : INotifyPropertyChanged
{
    public delegate void NotificatedEventHandler(string NotificationText, bool Positive, bool Negative);
    public event NotificatedEventHandler EventAction;

    /// <summary>
    /// Событие подлежащее обработке в системе оповещений
    /// </summary>
    /// <param name="NotificationText">Текст оповещения</param>
    /// <param name="Positive">Положительное оповещение</param>
    /// <param name="Negative">Отрицательное оповещение</param>
    protected void OnEventAction(string NotificationText, bool Positive, bool Negative) =>
        EventAction?.Invoke(NotificationText, Positive, Negative);


    public delegate void LogEventHandler(string LogText);
    public event LogEventHandler HistoryEventAction;

    protected void OnHistoryEventAction(string LogText) =>
        HistoryEventAction?.Invoke(LogText);

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

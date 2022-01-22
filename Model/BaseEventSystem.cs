using System.Windows;

namespace Homework_13.Model
{
    public abstract class BaseEventSystem : DependencyObject
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_13.Model
{
    public abstract class BaseEventSystem
    {
        public delegate void NotificatedEventHandler(string NotificationText);
        public event NotificatedEventHandler EventAction;

        protected void OnEventAction(string NotificationText) =>
            EventAction?.Invoke(NotificationText);
    }
}

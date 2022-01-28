using System.Collections.Generic;
using System.Linq;

namespace CustomerAccount.Domain.Notifications
{
    public class Notification : INotification
    {
        List<string> _notifications;
        public Notification() => _notifications = new List<string>();

        public void Add(string notification) => _notifications.Add(notification);

        public void Clear() => _notifications.Clear();

        public IEnumerable<string> GetAll() => _notifications;

        public bool Any() => _notifications.Any();
    }
}

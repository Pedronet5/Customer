using System.Collections.Generic;

namespace CustomerAccount.Domain.Notifications
{
    public interface INotification
    {
        IEnumerable<string> GetAll();

        void Clear();

        void Add(string notification);

        bool Any();
    }
}

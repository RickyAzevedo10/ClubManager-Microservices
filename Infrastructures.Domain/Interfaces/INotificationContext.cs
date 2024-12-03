using Infrastructures.Domain.Common;

namespace Infrastructures.Domain.Interfaces
{
    public interface INotificationContext
    {
        public void AddNotification(string error, string message);
        public bool HasNotifications();
        IReadOnlyCollection<Notification> Notifications { get; }
    }
}

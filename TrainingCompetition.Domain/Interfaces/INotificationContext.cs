using TrainingCompetition.Domain.Common;

namespace TrainingCompetition.Domain.Interfaces
{
    public interface INotificationContext
    {
        public void AddNotification(string error, string message);
        public bool HasNotifications();
        IReadOnlyCollection<Notification> Notifications { get; }
    }
}

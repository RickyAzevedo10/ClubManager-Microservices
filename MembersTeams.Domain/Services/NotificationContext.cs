﻿using MembersTeams.Domain.Common;
using MembersTeams.Domain.Interfaces;

namespace MembersTeams.Domain.Services
{
    public class NotificationContext : INotificationContext
    {
        private readonly List<Notification> _notifications;
        public NotificationContext()
        {
            _notifications = new List<Notification>();
        }

        public IReadOnlyCollection<Notification> Notifications => _notifications;

        public void AddNotification(string error, string message)
        {
            _notifications.Add(new Notification(error, message));
        }

        public bool HasNotifications()
        {
            return _notifications.Count > 0;
        }
    }
}

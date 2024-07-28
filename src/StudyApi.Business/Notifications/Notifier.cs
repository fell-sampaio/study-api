using StudyApi.Business.Interfaces;

namespace StudyApi.Business.Notifications;
public class Notifier : INotifier
{
    private readonly List<Notification> _notifications = [];

    public void Handle(Notification notification)
    {
        _notifications.Add(notification);
    }

    public List<Notification> GetNotifications()
    {
        return _notifications;
    }

    public bool HasNotification()
    {
        return _notifications.Count != 0;
    }
}

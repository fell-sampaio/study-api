using StudyApi.Business.Notifications;

namespace StudyApi.Business.Interfaces;
public interface INotifier
{
    bool HasNotification();
    List<Notification> GetNotifications();
    void Handle(Notification notification);
}

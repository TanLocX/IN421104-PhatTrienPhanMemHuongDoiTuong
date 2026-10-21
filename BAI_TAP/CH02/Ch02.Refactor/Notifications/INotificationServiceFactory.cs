namespace EduHub.Ch02.Notifications;

/// <summary>FACTORY: tạo service thông báo mà không để code gọi phải new class cụ thể.</summary>
public interface INotificationServiceFactory
{
    INotificationService Create(NotificationChannel channel);
}

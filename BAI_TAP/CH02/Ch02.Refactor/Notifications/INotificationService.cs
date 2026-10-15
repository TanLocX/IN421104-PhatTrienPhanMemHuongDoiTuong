namespace EduHub.Ch02.Notifications;

/// <summary>Abstraction cho việc gửi thông báo (OCP/DIP: không Console.WriteLine cứng).</summary>
public interface INotificationService
{
    void Send(string to, string message);
}

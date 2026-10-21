namespace EduHub.Ch02.Notifications;

public class EmailNotificationService : INotificationService
{
    public void Send(string to, string message) =>
        Console.WriteLine($"[EMAIL] To: {to} | Message: {message}");
}

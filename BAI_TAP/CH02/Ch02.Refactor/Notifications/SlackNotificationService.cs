namespace EduHub.Ch02.Notifications;

public class SlackNotificationService : INotificationService
{
    public void Send(string to, string message) =>
        Console.WriteLine($"[SLACK] To: {to} | Message: {message}");
}

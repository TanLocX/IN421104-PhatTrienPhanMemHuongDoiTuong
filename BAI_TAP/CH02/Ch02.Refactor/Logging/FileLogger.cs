namespace EduHub.Ch02.Logging;

public class FileLogger : IAppLogger
{
    private readonly string _path;

    public FileLogger(string path = "club-log.txt")
    {
        _path = path;
    }

    public void Info(string message)
    {
        // File.AppendAllText(_path, $"[INFO] {message}{Environment.NewLine}");
        File.AppendAllText(_path, $"[{DateTime.Now:s}] {message}{Environment.NewLine}");
    }
}
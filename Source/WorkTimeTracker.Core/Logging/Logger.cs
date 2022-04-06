using WorkTimeTracker.Core.Configuration;

namespace WorkTimeTracker.Core.Logging;

public class Logger : ILogger
{
    readonly Paths _path;

    public Logger(Paths path)
    {
        _path = path ?? throw new ArgumentNullException(nameof(path));
    }

    public void Error(Exception exception)
    {
        try
        {
            File.AppendAllText(_path.Log, exception.ToString());
        }
        catch { }
    }
}
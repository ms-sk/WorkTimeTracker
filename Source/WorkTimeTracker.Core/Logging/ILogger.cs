namespace WorkTimeTracker.Core.Logging;

public interface ILogger
{
    void Error(Exception exception);
}
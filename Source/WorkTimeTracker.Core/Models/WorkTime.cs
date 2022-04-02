namespace WorkTimeTracker.Core.Models;

[Obsolete]
public sealed class WorkTime
{
    public WorkTime()
    {
        Days = new List<Day>();
    }

    public List<Day> Days { get; set; }
}
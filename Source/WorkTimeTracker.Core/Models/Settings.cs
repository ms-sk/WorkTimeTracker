namespace WorkTimeTracker.Core.Models;

public sealed class Settings
{
    public Settings()
    {
        Filter = Filter.None;
        HoursPerDay = 8.0;
        DefaultUpdateInterval = new TimeSpan(0, 15, 0);
    }

    public Filter Filter { get; set; }

    public double HoursPerDay { get; set; }

    public TimeSpan DefaultUpdateInterval { get; set; }
}
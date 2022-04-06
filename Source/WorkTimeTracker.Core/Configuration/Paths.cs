namespace WorkTimeTracker.Core.Configuration;

public sealed class Paths
{
    public Paths()
    {
        Root = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WorkTimeTracker");
        WorkTime = Path.Combine(Root, "WorkTimes.json");
        Log = Path.Combine(Root, "logs.log");
        Settings = Path.Combine(Root, "Settings.json");
        Strings = Path.Combine(Root, "Strings.json");
    }

    public string Root { get; }

    public string WorkTime { get; }

    public string Log { get; }

    public string Settings { get; }

    public string Strings { get; }
}
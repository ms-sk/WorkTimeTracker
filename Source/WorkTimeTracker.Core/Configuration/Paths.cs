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

    public string Root { get; set; }

    public string WorkTime { get; set; }

    public string Log { get; set; }

    public string Settings { get; set; }

    public string Strings { get; set; }
}
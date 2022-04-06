namespace WorkTimeTracker.Core.Models;

public sealed class Day
{
    public Day()
    {
        Start = DateTime.Today;
    }

    public Guid? Id { get; set; }

    public DateTime Start { get; set; }

    public DateTime? End { get; set; }

    public double? Time { get; set; }

    public double? Break { get; set; }

    public List<TaskDto>? Tasks { get; set; }

    public WorkType Type { get; set; }
}
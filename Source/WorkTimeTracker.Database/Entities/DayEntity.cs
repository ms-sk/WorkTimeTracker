using WorkTimeTracker.Interfaces.Entities;

namespace WorkTimeTracker.Database.Entities
{
    public class DayEntity : IEntity
    {
        public int Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime? End { get; set; }

        public double? Time { get; set; }

        public double? Break { get; set; }

        public List<TaskEntity>? Tasks { get; set; }

        public WorkType Type { get; set; }
    }
}

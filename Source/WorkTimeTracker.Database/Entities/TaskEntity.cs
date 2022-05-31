using WorkTimeTracker.Interfaces.Entities;

namespace WorkTimeTracker.Database.Entities
{
    public class TaskEntity : IEntity
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public double Worktime { get; set; }

        public WorkType Type { get; set; }
    }
}

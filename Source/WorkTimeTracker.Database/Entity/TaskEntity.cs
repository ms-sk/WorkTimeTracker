using WorkTimeTracker.Interfaces.Entity;

namespace WorkTimeTracker.Database.Entity
{
    public class TaskEntity : IEntity
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public double Worktime { get; set; }

        public WorkType Type { get; set; }
    }
}

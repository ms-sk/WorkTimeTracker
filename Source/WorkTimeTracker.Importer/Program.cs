using System.Text.Json;
using WorkTimeTracker.Core;
using WorkTimeTracker.Core.Configuration;
using WorkTimeTracker.Core.ModelsDeprecated;
using WorkTimeTracker.Database;
using WorkTimeTracker.Database.Entity;
using WorkTimeTracker.Database.Repository;

namespace WorkTimeTracker.Importer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var json = File.ReadAllText(new Paths().WorkTime);
            var days = JsonSerializer.Deserialize<List<Day>>(json);

            using(var repository = new DayRepository(new WttContext()))
            {
                var dbdays = repository.GetAll();

                //foreach (var day in days)
                //{
                //    var entity = new DayEntity();
                //    entity.Start = day.Start;
                //    entity.End = day.End;
                //    entity.Break = day.Break;
                //    entity.Type = MapToWorkType(day.Type);

                //    repository.Insert(entity);
                //}
                //repository.Save();
            }
        }

        static WorkType MapToWorkType(Core.Models.WorkType workType)
        {
            switch(workType)
            {
                case Core.Models.WorkType.Work:
                    return WorkType.Work;
                case Core.Models.WorkType.Education:
                    return WorkType.Education;
                case Core.Models.WorkType.Illness:
                    return WorkType.Illness;
                case Core.Models.WorkType.Holiday:
                    return WorkType.Holiday;
                default:
                    throw new NotSupportedException(nameof(workType));
            }
        }
    }
}
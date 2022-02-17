using Core.Dtos;
using Core.Extensions;

namespace Core.Storage
{
    public sealed class DayStorage : IStorage<List<Day>>
    {
        readonly IStorage<WorkTime> _storage;

        public DayStorage(IStorage<WorkTime> storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public async Task Save(List<Day> ds)
        {
            var guids = ds.Select(t => t.Id);

            var workTime = await _storage.Load();
            var days = workTime.Days.Where(d => guids.Contains(d.Id)).ToList();

            foreach (var day in days)
            {
                foreach (var updatedDay in ds)
                {
                    if (day.Id == updatedDay.Id)
                    {
                        day.Start = updatedDay.Start;
                        day.End = updatedDay.End;
                        day.Break = updatedDay.Break;
                        day.Time = updatedDay.Time;
                        if (day.Tasks != null)
                        {
                            day.Tasks.Replace(updatedDay.Tasks ?? new List<TaskDto>());
                        }
                    }
                }
            }

            await _storage.Save(workTime);
        }

        public async Task<List<Day>> Load()
        {
            var workTime = await _storage.Load();
            return workTime.Days;
        }
    }
}

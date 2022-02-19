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
            var workTime = await _storage.Load();

            foreach (var day in ds)
            {
                var existingDay = workTime.Days.FirstOrDefault(d => d.Id == day.Id);
                if (existingDay != null)
                {
                    existingDay.Start = day.Start;
                    existingDay.End = day.End;
                    existingDay.Break = day.Break;
                    existingDay.Time = day.Time;

                    if (existingDay.Tasks == null)
                    {
                        existingDay.Tasks = new List<TaskDto>();
                    }

                    existingDay.Tasks.Replace(day.Tasks ?? new List<TaskDto>());
                }
                else
                {
                    workTime.Days.Add(day);
                }
            }
            await _storage.Save(workTime);
        }

        public async Task<List<Day>> Load()
        {
            var workTime = await _storage.Load();
            return workTime.Days;
        }

        public async Task Delete(List<Day> t)
        {
            var workTime = await _storage.Load();

            foreach (var day in t)
            {
                var delete = workTime.Days.FirstOrDefault(d => day.Id == d.Id);
                if (delete != null)
                {
                    workTime.Days.Remove(delete);
                }
            }

            _storage.Save(workTime);
        }
    }
}

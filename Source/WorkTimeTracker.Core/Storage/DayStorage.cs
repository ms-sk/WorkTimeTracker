using WorkTimeTracker.Core.Extensions;
using WorkTimeTracker.Core.Models;

namespace WorkTimeTracker.Core.Storage;

public sealed class DayStorage : IStorage<List<Day>>
{
    readonly IStorage<WorkTime> _workTimeStorage;

    public DayStorage(IStorage<WorkTime> workTimeStorage)
    {
        _workTimeStorage = workTimeStorage ?? throw new ArgumentNullException(nameof(workTimeStorage));
    }

    public async Task Save(List<Day> days)
    {
        var workTime = await _workTimeStorage.Load();

        foreach (var day in days)
        {
            var existingDay = workTime.Days.FirstOrDefault(d => d.Id == day.Id);
            if (existingDay != null)
            {
                existingDay.Start = day.Start;
                existingDay.End = day.End;
                existingDay.Break = day.Break;
                existingDay.Time = day.Time;
                existingDay.Type = day.Type;

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
        await _workTimeStorage.Save(workTime);
    }

    public async Task<List<Day>> Load()
    {
        var workTime = await _workTimeStorage.Load();
        return workTime.Days;
    }

    public async Task Delete(List<Day> t)
    {
        var workTime = await _workTimeStorage.Load();

        foreach (var day in t)
        {
            var delete = workTime.Days.FirstOrDefault(d => day.Id == d.Id);
            if (delete != null)
            {
                workTime.Days.Remove(delete);
            }
        }

        await _workTimeStorage.Save(workTime);
    }
}
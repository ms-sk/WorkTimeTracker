using System.Text.Json;
using WorkTimeTracker.Core.Configuration;
using WorkTimeTracker.Core.Extensions;
using WorkTimeTracker.Core.Models;

namespace WorkTimeTracker.Core.Storage;

public sealed class DayStorage : IDayStorage
{
    private readonly Paths _paths;

    public DayStorage(Paths paths)
    {
        _paths = paths ?? throw new ArgumentNullException(nameof(paths));
    }

    public async Task Save(List<Day> daysToSave)
    {
        var days = await Load();

        foreach (var day in daysToSave)
        {
            var existingDay = days.FirstOrDefault(d => d.Id == day.Id);
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
                days.Add(day);
            }
        }

        var json = JsonSerializer.Serialize(days, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_paths.WorkTime, json);
    }

    public async Task<List<Day>> Load()
    {
        CreateRootFolder();

        if (File.Exists(_paths.WorkTime))
        {
            var json = await File.ReadAllTextAsync(_paths.WorkTime) ?? string.Empty;

            if (json == string.Empty)
            {
                return new List<Day>();
            }

            return JsonSerializer.Deserialize<List<Day>>(json) ?? new List<Day>();
        }

        return new List<Day>();
    }

    public async Task Delete(List<Day> daysToDelete)
    {
        var days = await Load();

        foreach (var day in daysToDelete)
        {
            var delete = days.FirstOrDefault(d => day.Id == d.Id);
            if (delete != null)
            {
                days.Remove(delete);
            }
        }

        await Save(days);
    }

    void CreateRootFolder()
    {
        if (Directory.Exists(_paths.Root))
        {
            return;
        }

        Directory.CreateDirectory(_paths.Root);
    }
}
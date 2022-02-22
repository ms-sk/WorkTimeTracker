using Core.Configuration;
using Core.Dtos;
using System.Text.Json;

namespace Core.Storage
{
    public sealed class WorkTimeStorage : IStorage<WorkTime>
    {
        readonly Paths _paths;

        public WorkTimeStorage(Paths paths)
        {
            _paths = paths;
        }

        public Task Delete(WorkTime t)
        {
            throw new InvalidOperationException();
        }

        public async Task<WorkTime> Load()
        {
            CreateRootFolder();

            if (File.Exists(_paths.WorkTime))
            {
                var json = await File.ReadAllTextAsync(_paths.WorkTime) ?? string.Empty;

                if (json == string.Empty)
                {
                    return new WorkTime { Days = new List<Day>() };
                }

                var workTime = JsonSerializer.Deserialize<WorkTime>(json) ?? new WorkTime() { Days = new List<Day>() };

                var shouldSave = false;
                foreach (var day in workTime.Days)
                {
                    if (day.Id == null)
                    {
                        day.Id = Guid.NewGuid();
                        shouldSave = true;
                    }

                    if (day.Tasks?.Any() == true)
                    {
                        foreach (var task in day.Tasks)
                        {
                            if (task.Id == null)
                            {
                                task.Id = Guid.NewGuid();
                                shouldSave = true;
                            }
                        }
                    }
                }

                if (shouldSave)
                {
                    await Save(workTime);
                }

                return workTime;
            }

            return new WorkTime { Days = new List<Day>() };
        }

        public async Task Save(WorkTime dto)
        {
            CreateRootFolder();

            var json = JsonSerializer.Serialize(dto, new JsonSerializerOptions() { WriteIndented = true });
            await File.WriteAllTextAsync(_paths.WorkTime, json);
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
}

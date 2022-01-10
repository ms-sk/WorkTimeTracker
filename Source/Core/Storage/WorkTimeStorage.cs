using Core.Configuration;
using Dtos;
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

                return JsonSerializer.Deserialize<WorkTime>(json) ?? new WorkTime() { Days = new List<Day>() };
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

using Core.Configuration;
using Core.Models;
using System.Text.Json;

namespace Core.Storage
{
    public sealed class SettingsStorage : IStorage<Settings>
    {
        readonly Paths _paths;

        public SettingsStorage(Paths paths)
        {
            _paths = paths;
        }

        public Task Delete(Settings t)
        {
            throw new NotImplementedException();    
        }

        public async Task<Settings> Load()
        {
            CreateRootFolder();

            if (File.Exists(_paths.Settings))
            {
                var json = await File.ReadAllTextAsync(_paths.Settings) ?? string.Empty;

                if (json == string.Empty)
                {
                    return new Settings();
                }

                return JsonSerializer.Deserialize<Settings>(json) ?? new Settings();
            }

            return new Settings();
        }

        public async Task Save(Settings dto)
        {
            CreateRootFolder();

            var json = JsonSerializer.Serialize(dto, new JsonSerializerOptions() { WriteIndented = true });
            await File.WriteAllTextAsync(_paths.Settings, json);
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

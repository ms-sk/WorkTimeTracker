using System.Text.Json;

using WorkTimeTracker.Core.Configuration;

namespace WorkTimeTracker.Core.Storage;

public sealed class StringStorage : IStorage<List<string>>
{
    private readonly Paths _paths;

    public StringStorage(Paths paths)
    {
        _paths = paths ?? throw new ArgumentNullException(nameof(paths));
    }

    public Task Delete(List<string> t)
    {
        throw new NotImplementedException();
    }

    public async Task<List<string>> Load()
    {
        CreateRootFolder();

        if (File.Exists(_paths.Strings))
        {
            var json = await File.ReadAllTextAsync(_paths.Strings) ?? string.Empty;

            if (json == string.Empty)
            {
                return new List<string>();
            }

            return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
        }

        return new List<string>();
    }

    public async Task Save(List<string> t)
    {
        CreateRootFolder();

        var json = JsonSerializer.Serialize(t, new JsonSerializerOptions() { WriteIndented = true });
        await File.WriteAllTextAsync(_paths.Strings, json);
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

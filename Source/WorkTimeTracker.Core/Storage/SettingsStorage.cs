﻿using System.Text.Json;
using WorkTimeTracker.Core.Configuration;
using WorkTimeTracker.Core.Models;

namespace WorkTimeTracker.Core.Storage;

public sealed class SettingsStorage : ISettingsStorage
{
    readonly Paths _paths;

    public SettingsStorage(Paths paths)
    {
        _paths = paths ?? throw new ArgumentNullException(nameof(paths));
    }

    public Task Delete(Settings t)
    {
        throw new NotSupportedException();    
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
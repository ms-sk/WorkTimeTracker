using System;
using System.Timers;
using WorkTimeTracker.Core.Logging;
using WorkTimeTracker.Core.Models;
using WorkTimeTracker.Core.Storage;

namespace WorkTimeTracker.UI.ViewModels
{
    public sealed class WorkTimeUpdater
    {
        readonly Timer _timer = new(60000);
        readonly IStorage<WorkTime> _storage;
        readonly ILogger _logger;

        public WorkTimeUpdater(IStorage<WorkTime> storage, ILogger logger)
        {
            _timer.Elapsed += UpdateStorage;
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _logger = logger;
        }

        public Func<WorkTime>? GetWorkTime { get; set; }

        public void Start() => _timer.Start();

        public void Stop() => _timer.Stop();

        async void UpdateStorage(object? sender, ElapsedEventArgs e)
        {
            try
            {
                if (GetWorkTime != null)
                {
                    var workTime = GetWorkTime();
                    await _storage.Save(workTime);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }
    }
}

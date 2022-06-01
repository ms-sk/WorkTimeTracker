using WorkTimeTracker.Core.ModelsDeprecated;

namespace WorkTimeTracker.Core.Storage
{
    public sealed class TaskStorage : ITaskStorage
    {
        readonly IDayStorage _dayStorage;

        public TaskStorage(IDayStorage dayStorage)
        {
            _dayStorage = dayStorage ?? throw new ArgumentNullException(nameof(dayStorage));
        }

        public Task Delete(List<string> t)
        {
            throw new NotSupportedException();
        }

        public async Task<List<string>> Load()
        {
            var days = await _dayStorage.Load();

            if (days == null)
            {
                return new List<string>();
            }

            return days.SelectMany(d => d?.Tasks ?? new List<TaskDto>()).Select(t => t?.Description ?? string.Empty).Distinct().OrderBy(x => x).ToList();
        }

        public Task Save(List<string> t)
        {
            throw new NotSupportedException();
        }
    }
}

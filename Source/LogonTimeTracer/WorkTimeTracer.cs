using Core.Dtos;
using Core.Storage;

namespace LogonTimeTracer
{
    internal sealed class WorkTimeTracer
    {
        readonly IStorage<WorkTime> _workTimeStorage;

        public WorkTimeTracer(IStorage<WorkTime> workTimeStorage)
        {
            _workTimeStorage = workTimeStorage ?? throw new ArgumentNullException(nameof(workTimeStorage));
        }

        internal async Task Logon()
        {
            var workTime = await _workTimeStorage.Load();

            var today = workTime.Days.FirstOrDefault(day => day.Start.HasValue && day.Start.Value.Date == DateTime.Today);

            if (today == null)
            {
                workTime.Days.Add(new Day() { Start = DateTime.Now });
            }
            else
            {
                return;
            }

            workTime.Days = workTime.Days.OrderBy(day => day.Start).ToList();
            await _workTimeStorage.Save(workTime);
        }

        internal async Task Logoff()
        {
            var workTime = await _workTimeStorage.Load();
            var today = workTime.Days.First(day => day.Start.HasValue && day.Start.Value.Date == DateTime.Today);
            today.End = DateTime.Now;

            if (today.End.HasValue && today.Start.HasValue)
            {
                var hours = (today.End - today.Start).Value.TotalHours;
                var rounded = Math.Round(hours * 4, MidpointRounding.ToEven) / 4.0;
                today.Time = (decimal)rounded;
            }

            await _workTimeStorage.Save(workTime);
        }
    }
}

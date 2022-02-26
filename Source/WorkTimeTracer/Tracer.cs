using Core.Dtos;
using Core.Math;
using Core.Storage;

namespace WorkTimeTracer
{
    internal sealed class Tracer
    {
        readonly IStorage<WorkTime> _workTimeStorage;

        public Tracer(IStorage<WorkTime> workTimeStorage)
        {
            _workTimeStorage = workTimeStorage ?? throw new ArgumentNullException(nameof(workTimeStorage));
        }

        internal async Task Logon()
        {
            var workTime = await _workTimeStorage.Load();

            var today = workTime.Days.FirstOrDefault(day => day.Start.Date == DateTime.Today);

            if (today == null)
            {
                workTime.Days.Add(new Day { Start = DateTime.Now });
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
            var today = workTime.Days.First(day => day.Start.Date == DateTime.Today);
            today.End = DateTime.Now;

            if (today.End.HasValue)
            {
                var hours = (today.End - today.Start).Value.TotalHours;
                var rounded = CMath.RoundQuarter(hours);
                today.Time = rounded;
            }

            await _workTimeStorage.Save(workTime);
        }
    }
}

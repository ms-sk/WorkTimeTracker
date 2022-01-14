using Core.Math;
using Core.Storage;
using Dtos;

namespace LogonTimeTracer
{
    internal sealed class WorkTimeTracer
    {
        readonly IStorage<WorkTime> _workTimeStorage;
        readonly ICalculator _calculator;

        public WorkTimeTracer(IStorage<WorkTime> workTimeStorage, ICalculator calculator)
        {
            _workTimeStorage = workTimeStorage ?? throw new ArgumentNullException(nameof(workTimeStorage));
            _calculator = calculator ?? throw new ArgumentNullException(nameof(calculator));
        }

        internal async Task Logon()
        {
            var workTime = await _workTimeStorage.Load();

            var today = workTime.Days.FirstOrDefault(day => day.Start.GetValueOrDefault() == DateTime.Today);

            if (today != null)
            {
                return;
            }

            workTime.Days.Add(new Day() { Start = DateTime.Now });
            workTime.Days = workTime.Days.OrderBy(day => day.Start).ToList();
            await _workTimeStorage.Save(workTime);
        }

        internal async Task Logoff()
        {
            var workTime = await _workTimeStorage.Load();
            var today = workTime.Days.First(day => day.Start.GetValueOrDefault() == DateTime.Today);
            today.End = DateTime.Now;

            if (today.End.HasValue && today.Start.HasValue)
            {
                var hours = (today.End - today.Start).Value.TotalHours;
                today.Time = _calculator.RoundQuarter(hours);
            }

            await _workTimeStorage.Save(workTime);
        }
    }
}

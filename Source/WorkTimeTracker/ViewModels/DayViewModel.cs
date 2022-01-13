using Dtos;
using System;

namespace WorkTimeTracker
{
    internal class DayViewModel : ViewModel
    {
        string _date = string.Empty;
        string _startTime = string.Empty;
        string _endTime = string.Empty;
        string _workTime = string.Empty;
        string _break = string.Empty;

        EventHandler DayChanged;

        public DayViewModel()
        {
            DayChanged += OnDayChanged;
        }

        public string Date { get => _date; set => SetValue(ref _date, value); }

        public string StartTime { get => _startTime; set => SetValue(ref _startTime, value); }

        public string EndTime { get => _endTime; set => SetValue(ref _endTime, value); }

        public string WorkTime
        {
            get => _workTime;
            set
            {
                SetValue(ref _workTime, value);
                DayChanged.Invoke(this, EventArgs.Empty);
            }
        }

        public string Break
        {
            get => _break;
            set
            {
                SetValue(ref _break, value);
                DayChanged.Invoke(this, EventArgs.Empty);
            }
        }

        public Day? Dto { get; set; }

        void OnDayChanged(object? sender, EventArgs e)
        {
            if (Dto == null)
            {
                return;
            }

            if (decimal.TryParse(Break, out var result))
            {
                Dto.Break = result;
            }
            else
            {
                Break = Dto.Break.GetValueOrDefault().ToString("F");
            }

            if (decimal.TryParse(WorkTime, out var workTime))
            {
                Dto.Time = workTime + Dto.Break;
            }
            else
            {
                WorkTime = (Dto.Time.GetValueOrDefault() - Dto.Break.GetValueOrDefault()).ToString("F");
            }
        }
    }
}

using Dtos;

namespace WorkTimeTracker
{
    internal class DayViewModel : ViewModel
    {
        string _date = string.Empty;
        string _startTime = string.Empty;
        string _endTime = string.Empty;
        string _workTime = string.Empty;
        string _break = string.Empty;

        public string Date { get => _date; set => SetValue(ref _date, value); }

        public string StartTime { get => _startTime; set => SetValue(ref _startTime, value); }

        public string EndTime { get => _endTime; set => SetValue(ref _endTime, value); }

        public string WorkTime
        {
            get => _workTime;
            set => SetValue(ref _workTime, value);
        }

        public string Break
        {
            get => _break;
            set => SetValue(ref _break, value);
        }

        public Day? Dto { get; set; }
    }
}

using Core.Dtos;

namespace WorkTimeTracker.ViewModels
{
    internal class DayViewModel : ViewModel
    {
        public string Date { get => GetValue<string>(); set => SetValue(value); }

        public string StartTime { get => GetValue<string>(); set => SetValue(value); }

        public string EndTime { get => GetValue<string>(); set => SetValue(value); }

        public string WorkTime
        {
            get => GetValue<string>();
            set
            {
                SetValue(value);
                DayChanged();
            }
        }

        public string Break
        {
            get => GetValue<string>();
            set
            {
                SetValue(value);
                DayChanged();
            }
        }

        public Day? Dto { get; set; }

        void DayChanged()
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

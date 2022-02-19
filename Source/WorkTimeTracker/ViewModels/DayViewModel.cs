using System.Collections.ObjectModel;
using Core.Dtos;
using Core.Wpf.ViewModels;

namespace WorkTimeTracker.ViewModels
{
    public class DayViewModel : ViewModel
    {
        public string? Date
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string? StartTime
        {
            get => GetValue<string>(); 
            set => SetValue(value);
        }

        public string? EndTime
        {
            get => GetValue<string>(); 
            set => SetValue(value);
        }

        public string? WorkTime
        {
            get => GetValue<string>();
            set
            {
                SetValue(value);
                DayChanged();
            }
        }

        public string? Break
        {
            get => GetValue<string>();
            set
            {
                SetValue(value);
                DayChanged();
            }
        }

        public Day? Dto { get; set; }

        public ObservableCollection<TaskViewModel> Tasks { get; } = new();

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

        public void DeleteTask(TaskViewModel model)
        {
            if (!Tasks.Contains(model))
            {
                return;
            }

            Tasks.Remove(model);
        }
    }
}

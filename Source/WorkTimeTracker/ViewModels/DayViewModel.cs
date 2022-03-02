using System;
using System.Collections.ObjectModel;
using System.Windows;
using Core.Dtos;
using Core.Models;
using Core.Wpf.MessageBoxes;
using Core.Wpf.ViewModels;

namespace WorkTimeTracker.ViewModels
{
    public class DayViewModel : ViewModel
    {
        public DayViewModel()
        {
            Date = DateTime.Today;
            StartTime = TimeOnly.MinValue;
            EndTime = TimeOnly.MinValue;
        }

        public DateTime? Date
        {
            get => GetValue<DateTime?>();
            set => SetValue(value);
        }

        public TimeOnly StartTime
        {
            get => GetValue<TimeOnly>();
            set => SetValue(value);
        }

        public TimeOnly? EndTime
        {
            get => GetValue<TimeOnly?>();
            set => SetValue(value);
        }

        public double WorkTime
        {
            get => GetValue<double>();
            set
            {
                SetValue(value);
                DayChanged();
            }
        }

        public double Break
        {
            get => GetValue<double>();
            set
            {
                SetValue(value);
                DayChanged();
            }
        }

        public Day? Dto { get; set; }

        public ObservableCollection<TaskViewModel> Tasks { get; } = new();

        public WorkType Type
        {
            get => GetValue<WorkType>();
            set => SetValue(value);
        }

        void DayChanged()
        {
            if (Dto == null)
            {
                return;
            }

            if (Break > 0.0)
            {
                Dto.Break = Break;
            }

            if (WorkTime > 0.0)
            {
                Dto.Time = WorkTime + Dto.Break;
            }
        }

        public void DeleteTask(TaskViewModel model)
        {
            var result = CMessageBox.Delete();
            if (result == MessageBoxResult.No || result == MessageBoxResult.Cancel)
            {
                return;
            }

            if (!Tasks.Contains(model))
            {
                return;
            }

            Tasks.Remove(model);
        }
    }
}

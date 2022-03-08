using System;
using System.Collections.Generic;
using WorkTimeTracker.Core.Models;
using WorkTimeTracker.Core.Wpf.Commands;
using WorkTimeTracker.Core.Wpf.ViewModels;

namespace WorkTimeTracker.UI.ViewModels
{
    public sealed class DetailsViewModel : ViewModel
    {
        public DetailsViewModel()
        {
            CreateCommand = new Command(ExecuteCreateCommand, _ => true);
            DeleteCommand = new Command(ExecuteDeleteCommand, _ => true);

            Types = Enum.GetValues<WorkType>();
        }

        public DayViewModel? SelectedDay
        {
            get => GetValue<DayViewModel>();
            private set => SetValue(value);
        }

        public Command? CreateCommand
        {
            get => GetValue<Command>();
            set => SetValue(value);
        }

        public Command? DeleteCommand
        {
            get => GetValue<Command>();
            set => SetValue(value);
        }

        public IEnumerable<WorkType> Types { get; }

        public void Reinitialize(DayViewModel selectedDay)
        {
            SelectedDay = selectedDay ?? throw new ArgumentNullException(nameof(selectedDay));
        }

        public void Clear()
        {
            SelectedDay = null;
        }

        void ExecuteCreateCommand(object? parameter = null)
        {
            SelectedDay?.Tasks.Add(new TaskViewModel());
        }

        void ExecuteDeleteCommand(object? parameter = null)
        {
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));
            if (parameter is not TaskViewModel model) throw new InvalidOperationException(nameof(parameter));

            SelectedDay?.DeleteTask(model);
        }
    }
}
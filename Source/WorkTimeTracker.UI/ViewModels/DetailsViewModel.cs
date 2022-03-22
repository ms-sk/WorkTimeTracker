using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
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
            CopyCommand = new Command(ExecuteCopyCommand, _ => true);

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
        public Command CopyCommand { get; }

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

        void ExecuteCopyCommand(object? obj)
        {
            var builder = new StringBuilder();
            foreach (var task in SelectedDay?.Tasks)
            {
                builder.AppendLine(task.Description);
            }
            Clipboard.SetText(builder.ToString());
        }
    }
}
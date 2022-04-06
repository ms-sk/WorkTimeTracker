using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkTimeTracker.Core.Extensions;
using WorkTimeTracker.Core.Models;
using WorkTimeTracker.Core.Storage;
using WorkTimeTracker.Core.Wpf.Commands;
using WorkTimeTracker.Core.Wpf.ViewModels;

namespace WorkTimeTracker.UI.ViewModels
{
    public sealed class DetailsViewModel : ViewModel
    {
        readonly ITaskStorage _taskStorage;

        public DetailsViewModel(ITaskStorage taskStorage)
        {
            _taskStorage = taskStorage ?? throw new ArgumentNullException(nameof(taskStorage));

            CreateCommand = new Command(ExecuteCreateCommand);
            DeleteCommand = new Command(ExecuteDeleteCommand);
            CopyCommand = new Command(ExecuteCopyCommand);

            Types = Enum.GetValues<WorkType>();
        }

        public ObservableCollection<string> Descriptions { get; } = new ObservableCollection<string>();

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

        public async Task LoadTasks()
        {
            var tasks = await _taskStorage.Load();
            Descriptions.Replace(tasks);
        }

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
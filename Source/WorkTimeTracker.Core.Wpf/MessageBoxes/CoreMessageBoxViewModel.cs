using System;
using System.Windows;
using System.Windows.Input;
using WorkTimeTracker.Core.Wpf.Commands;
using WorkTimeTracker.Core.Wpf.ViewModels;

namespace WorkTimeTracker.Core.Wpf.MessageBoxes
{
    public sealed class CoreMessageBoxViewModel : ViewModel
    {
        public CoreMessageBoxViewModel()
        {
            Save = new Command(ExecuteSave, (_) => true);
            Cancel = new Command(ExecuteCancel, (_) => true);
        }

        public event EventHandler? Executed;

        public object Header
        {
            get => GetValue<string>() ?? string.Empty;
            set => SetValue(value);
        }

        public object Message
        {
            get => GetValue<object>() ?? string.Empty;
            set => SetValue(value);
        }

        public MessageBoxResult Result { get; set; } = MessageBoxResult.Cancel;

        public ICommand Save { get; }

        public ICommand Cancel { get; }

        void ExecuteSave(object? obj)
        {
            Result = MessageBoxResult.Yes;
            Executed?.Invoke(this, EventArgs.Empty);
        }

        void ExecuteCancel(object? obj)
        {
            Result = MessageBoxResult.No;
            Executed?.Invoke(this, EventArgs.Empty);
        }
    }
}

using WorkTimeTracker.Core.Wpf.ViewModels;

namespace WorkTimeTracker.UI.ViewModels
{
    public sealed class SettingsItemViewModel<T> : SettingsItemViewModel
    {
        public new T? Value
        {
            get => (T?)base.Value;
            set => base.Value = value;
        }
    }

    public class SettingsItemViewModel : ViewModel
    {

        public string? Title
        {
            get => GetValue<string?>();

            set => SetValue(value);
        }

        public object? Value
        {
            get => GetValue<object?>();
            set => SetValue(value);
        }
    }
}
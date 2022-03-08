using WorkTimeTracker.Core.Models;
using WorkTimeTracker.Core.Wpf.ViewModels;

namespace WorkTimeTracker.UI.ViewModels
{
    public sealed class SumViewModel : ViewModel
    {
        public string DisplayText
        {
            get => GetValue<string>() ?? string.Empty;
            set => SetValue(value);
        }
        public WorkType Type
        {
            get => GetValue<WorkType>();
            set => SetValue(value);
        }

        public double Sum
        {
            get => GetValue<double>();
            set => SetValue(value);
        }
    }
}

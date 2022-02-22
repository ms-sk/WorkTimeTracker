using Core.Models;
using Core.Wpf.ViewModels;

namespace WorkTimeTracker.ViewModels
{
    public sealed class SumViewModel : ViewModel
    {
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

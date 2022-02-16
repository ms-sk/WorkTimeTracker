using System;

namespace WorkTimeTracker.ViewModels
{
    public sealed class DetailsViewModel : ViewModel
    {
        public DayViewModel SelectedDay
        {
            get => GetValue<DayViewModel>();
            private set => SetValue(value);
        }

        public void Initialize(DayViewModel selectedDay)
        {
            SelectedDay = selectedDay ?? throw new ArgumentNullException(nameof(selectedDay));
        }
    }
}
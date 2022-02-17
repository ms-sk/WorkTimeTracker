using System;

namespace WorkTimeTracker.ViewModels
{
    public sealed class MainViewModel : ViewModel
    {
        public MainViewModel(MasterViewModel masterViewModel, DetailsViewModel detailsViewModel, ToolbarViewModel toolbarViewModel)
        {
            MasterViewModel = masterViewModel ?? throw new ArgumentNullException(nameof(masterViewModel));
            DetailsViewModel = detailsViewModel ?? throw new ArgumentNullException(nameof(detailsViewModel));
            ToolbarViewModel = toolbarViewModel ?? throw new ArgumentNullException(nameof(ToolbarViewModel));

            MasterViewModel.SelectedDayChanged += (_, _) =>
            {
                if (MasterViewModel.SelectedDay == null)
                {
                    return;
                }

                DetailsViewModel.Reinitialize(MasterViewModel.SelectedDay);
            };

            MasterViewModel.SelectedFilterChanged += (_, _) =>
            {
                DetailsViewModel?.Clear();
            };
        }

        public MasterViewModel MasterViewModel { get; }

        public DetailsViewModel? DetailsViewModel
        {
            get => GetValue<DetailsViewModel>();
            private set => SetValue(value);
        }

        public ToolbarViewModel ToolbarViewModel { get; }
    }
}

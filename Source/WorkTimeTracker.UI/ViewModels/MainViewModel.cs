using System;
using WorkTimeTracker.Core.Wpf.Loading;
using WorkTimeTracker.Core.Wpf.ViewModels;

namespace WorkTimeTracker.UI.ViewModels
{
    public sealed class MainViewModel : ViewModel
    {
        public MainViewModel(MasterViewModel masterViewModel, DetailsViewModel detailsViewModel, ToolbarViewModel toolbarViewModel, LoaderViewModel loaderViewModel, FooterViewModel footerViewModel)
        {
            MasterViewModel = masterViewModel ?? throw new ArgumentNullException(nameof(masterViewModel));
            DetailsViewModel = detailsViewModel ?? throw new ArgumentNullException(nameof(detailsViewModel));
            ToolbarViewModel = toolbarViewModel ?? throw new ArgumentNullException(nameof(toolbarViewModel));
            LoaderViewModel = loaderViewModel ?? throw new ArgumentNullException(nameof(loaderViewModel));
            FooterViewModel = footerViewModel ?? throw new ArgumentNullException(nameof(footerViewModel));

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

        public DetailsViewModel DetailsViewModel { get; }

        public MasterViewModel MasterViewModel { get; }

        public ToolbarViewModel ToolbarViewModel { get; }

        public LoaderViewModel LoaderViewModel { get; }

        public FooterViewModel FooterViewModel { get; }
    }
}

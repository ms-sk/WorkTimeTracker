﻿using System;
using WorkTimeTracker.Core.Wpf.Loading;
using WorkTimeTracker.Core.Wpf.ViewModels;

namespace WorkTimeTracker.UI.ViewModels
{
    public sealed class MainViewModel : ViewModel
    {
        public MainViewModel(MasterViewModel masterViewModel, DetailsViewModel detailsViewModel, ToolbarViewModel toolbarViewModel, LoaderViewModel loaderViewModel, FooterViewModel footer, SettingsViewModel settings)
        {
            MasterViewModel = masterViewModel ?? throw new ArgumentNullException(nameof(masterViewModel));
            DetailsViewModel = detailsViewModel ?? throw new ArgumentNullException(nameof(detailsViewModel));
            ToolbarViewModel = toolbarViewModel ?? throw new ArgumentNullException(nameof(toolbarViewModel));
            LoaderViewModel = loaderViewModel ?? throw new ArgumentNullException(nameof(loaderViewModel));
            Footer = footer ?? throw new ArgumentNullException(nameof(footer));
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));

            MasterViewModel.SelectedDayChanged += (_, _) =>
            {
                if(ToolbarViewModel.SaveAll.CanExecute(MasterViewModel.WorkTimes))
                {
                    ToolbarViewModel.SaveAll.Execute(MasterViewModel.WorkTimes);
                }

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

        public DetailsViewModel? DetailsViewModel
        {
            get => GetValue<DetailsViewModel>();
            private set => SetValue(value);
        }

        public MasterViewModel MasterViewModel { get; }

        public ToolbarViewModel ToolbarViewModel { get; }

        public LoaderViewModel LoaderViewModel { get; }

        public FooterViewModel Footer { get; }

        public SettingsViewModel Settings { get; }
    }
}
using Core.Dtos;
using Core.Storage;
using Core.Wpf.Commands;
using Core.Wpf.Loading;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WorkTimeTracker.Factories;
using WorkTimeTracker.UI;

namespace WorkTimeTracker.ViewModels
{
    public sealed class ToolbarViewModel
    {
        readonly IStorage<List<Day>> storage;
        readonly DayFactory dayFactory;
        readonly SettingsViewModel settingsViewModel;

        public ToolbarViewModel(IStorage<List<Day>> storage, DayFactory dayFactory, LoaderViewModel loaderViewModel, SettingsViewModel settingsViewModel)
        {
            this.storage = storage ?? throw new ArgumentNullException(nameof(storage));
            this.dayFactory = dayFactory ?? throw new ArgumentNullException(nameof(dayFactory));
            LoaderViewModel = loaderViewModel ?? throw new ArgumentNullException(nameof(loaderViewModel));
            this.settingsViewModel = settingsViewModel;
            Add = new AsyncCommand(ExecuteAdd, _ => true);
            Save = new AsyncCommand(ExecuteSave, _ => true);
            SaveAll = new AsyncCommand(ExecuteSaveAll, _ => true);
            Settings = new Command(ExecuteSettings, _ => true);
            Delete = new AsyncCommand(ExecuteDelete, _ => true);
        }

        public LoaderViewModel LoaderViewModel { get; }

        public ICommand Add { get; }

        public ICommand Save { get; }

        public ICommand SaveAll { get; }

        public ICommand Settings { get; }

        public ICommand Delete { get; }

        async Task ExecuteSave(object? parameter)
        {
            if (parameter is not DayViewModel dayViewModel)
            {
                return;
            }

            using (LoaderViewModel.Load())
            {
                var dto = dayFactory.CreateDay(dayViewModel);
                await storage.Save(new List<Day>() { dto });
            }
        }

        bool CanExecuteAll(object? arg)
        {
            if (arg is ICollection<DayViewModel> list)
            {
                return list.Count > 0;
            }

            return false;
        }

        async Task ExecuteSaveAll(object? arg)
        {
            if (arg is not ICollection<DayViewModel> list) { return; }

            using (LoaderViewModel.Load())
            {
                var dtos = new List<Day>();
                foreach (var viewModel in list)
                {
                    var dto = dayFactory.CreateDay(viewModel);
                    dtos.Add(dto);
                }

                await storage.Save(dtos);
            }
        }

        async Task ExecuteAdd(object? arg)
        {
            using (LoaderViewModel.Load())
            {
                var dto = new Day();

                await storage.Save(new List<Day> { dto });

                if (arg is MasterViewModel masterViewModel)
                {
                    await masterViewModel.LoadWorkTimes();
                }
            }
        }

        void ExecuteSettings(object? obj)
        {
            new SettingsWindow(settingsViewModel).ShowDialog();
        }

        async Task ExecuteDelete(object? arg)
        {
            var result = MessageBox.Show("Are you sure?", "Delete", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No || result == MessageBoxResult.Cancel)
            {
                return;
            }

            if (arg is MasterViewModel masterViewModel)
            {
                if (masterViewModel.SelectedDay?.Dto == null)
                {
                    throw new(nameof(masterViewModel.SelectedDay.Dto));
                }

                using (LoaderViewModel.Load())
                {
                    await storage.Delete(new List<Day>() { masterViewModel.SelectedDay.Dto });

                    await masterViewModel.LoadWorkTimes();
                }
            }

        }
    }
}
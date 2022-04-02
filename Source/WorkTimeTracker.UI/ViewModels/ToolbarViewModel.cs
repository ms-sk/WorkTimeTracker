using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WorkTimeTracker.Core.Models;
using WorkTimeTracker.Core.Storage;
using WorkTimeTracker.Core.Wpf.Commands;
using WorkTimeTracker.Core.Wpf.Loading;
using WorkTimeTracker.Core.Wpf.MessageBoxes;
using WorkTimeTracker.UI.Factories;
using WorkTimeTracker.UI.UI;

namespace WorkTimeTracker.UI.ViewModels
{
    public sealed class ToolbarViewModel
    {
        readonly IDayStorage _storage;
        readonly DayFactory _dayFactory;
        readonly SettingsViewModel _settingsViewModel;

        public ToolbarViewModel(IDayStorage storage, DayFactory dayFactory, LoaderViewModel loaderViewModel, SettingsViewModel settingsViewModel)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _dayFactory = dayFactory ?? throw new ArgumentNullException(nameof(dayFactory));
            _settingsViewModel = settingsViewModel;
            LoaderViewModel = loaderViewModel ?? throw new ArgumentNullException(nameof(loaderViewModel));

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
                var dto = _dayFactory.CreateDay(dayViewModel);
                await _storage.Save(new List<Day>() { dto });
            }
        }

        async Task ExecuteSaveAll(object? arg)
        {
            if (arg is not ICollection<DayViewModel> list) { return; }

            using (LoaderViewModel.Load())
            {
                var dtos = new List<Day>();
                foreach (var viewModel in list)
                {
                    var dto = _dayFactory.CreateDay(viewModel);
                    dtos.Add(dto);
                }

                await _storage.Save(dtos);
            }
        }

        async Task ExecuteAdd(object? arg)
        {
            using (LoaderViewModel.Load())
            {
                var dto = new Day();

                await _storage.Save(new List<Day> { dto });

                if (arg is MasterViewModel masterViewModel)
                {
                    await masterViewModel.LoadWorkTimes();
                }
            }
        }

        void ExecuteSettings(object? obj)
        {
            var window = new SettingsWindow(_settingsViewModel);
            window.ShowDialog();
        }

        async Task ExecuteDelete(object? arg)
        {
            var result = CMessageBox.Delete();
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
                    await _storage.Delete(new List<Day>() { masterViewModel.SelectedDay.Dto });

                    await masterViewModel.LoadWorkTimes();
                }
            }

        }
    }
}
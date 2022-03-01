using Core.Dtos;
using Core.Logging;
using Core.Models;
using Core.Storage;
using Core.Wpf.Commands;
using Core.Wpf.Loading;
using Core.Wpf.ViewModels;
using Resources;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WorkTimeTracker.ViewModels
{
    public sealed class SettingsViewModel : ViewModel
    {
        readonly SettingsStorage settingsStorage;
        readonly LoaderViewModel loaderViewModel;
        readonly ILogger logger;
        Settings? settings;

        public SettingsViewModel(SettingsStorage settingsStorage, LoaderViewModel loaderViewModel, ILogger logger)
        {
            this.settingsStorage = settingsStorage ?? throw new System.ArgumentNullException(nameof(settingsStorage));
            this.loaderViewModel = loaderViewModel ?? throw new ArgumentNullException(nameof(loaderViewModel));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            Save = new AsyncCommand(SaveSettings, (_) => true);
        }

        public ObservableCollection<SettingsItemViewModel> Items { get; } = new ObservableCollection<SettingsItemViewModel>();

        public ICommand Save { get; }

        public async Task LoadSettings()
        {
            using (loaderViewModel.Load())
            {
                try
                {
                    settings = await settingsStorage.Load();

                    Items.Clear();
                    Items.Add(new SettingsItemViewModel<Filter>()
                    {
                        Title = Translations.Filter,
                        Value = settings.Filter
                    });
                    Items.Add(new SettingsItemViewModel<double>()
                    {
                        Title = Translations.HoursPerDay,
                        Value = settings.HoursPerDay
                    });
                }
                catch (Exception e)
                {
                    logger.Error(e);
                }
            }
        }

        public async Task SaveSettings(object? parameter)
        {
            using (loaderViewModel.Load())
            {
                var settings = new Settings();
                foreach (var item in Items)
                {
                    if (string.Equals(item.Title, Translations.Filter))
                    {
                        settings.Filter = (Filter)(item.Value ?? Filter.None);
                    }

                    if (string.Equals(item.Title, Translations.HoursPerDay))
                    {
                        settings.HoursPerDay = (double)(item.Value ?? 8);
                    }
                }

                await settingsStorage.Save(settings);
            }
        }
    }
}

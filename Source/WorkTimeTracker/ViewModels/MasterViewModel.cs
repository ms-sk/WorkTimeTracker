using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Extensions;
using Core.Storage;
using WorkTimeTracker.Builder;

namespace WorkTimeTracker.ViewModels;

public sealed class MasterViewModel : ViewModel
{
    private readonly IStorage<WorkTime> _workTimeStorage;
    private readonly SettingsStorage _settingsStorage;
    private readonly WorkTimeViewModelFactory _factory;
    private readonly WorkTimeTodayUpdater _updater;

    private readonly List<DayViewModel> _allWorkTimes = new();

    Settings? _settings;

    public MasterViewModel(IStorage<WorkTime> workTimeStorage, SettingsStorage settingsStorage, WorkTimeViewModelFactory factory, WorkTimeTodayUpdater updater)
    {
        _workTimeStorage = workTimeStorage ?? throw new ArgumentNullException(nameof(workTimeStorage));
        _settingsStorage = settingsStorage ?? throw new ArgumentNullException(nameof(settingsStorage));
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        _updater = updater ?? throw new ArgumentNullException(nameof(updater));

        Filters.Replace(factory.CreateFilterViewModels());
        SelectedFilter = Filters.FirstOrDefault();

        SelectedFilterChanged += (_, _) =>
        {
            if(_settings != null && SelectedFilter != null)
            {
                _settings.Filter = SelectedFilter.Filter;
            }
            _settingsStorage.Save(_settings ?? new Settings());

            Filter();
        };
    }

    public event EventHandler SelectedFilterChanged;

    public event EventHandler? SelectedDayChanged;

    public FilterViewModel? SelectedFilter
    {
        get => GetValue<FilterViewModel?>();
        set
        {
            SetValue(value);
            SelectedFilterChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public ObservableCollection<FilterViewModel> Filters { get; } = new ObservableCollection<FilterViewModel>();
    public ObservableCollection<DayViewModel> WorkTimes { get; } = new ObservableCollection<DayViewModel>();

    public DayViewModel? SelectedDay
    {
        get => GetValue<DayViewModel>();
        set
        {
            SetValue(value);
            SelectedDayChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Filter()
    {
        if (SelectedFilter != null)
        {
            var filtered = new List<DayViewModel>();
            foreach (var day in _allWorkTimes)
            {
                if (SelectedFilter.Matches(day))
                {
                    filtered.Add(day);
                }
            }
            WorkTimes.Replace(filtered);
        }
    }

    internal async Task LoadWorkTimes()
    {
        WorkTimes.Clear();
        
        var workTime = await _workTimeStorage.Load();
        workTime.Days = workTime.Days.OrderByDescending(x => x.Start).ToList();

        var today = DateTime.Today;
        foreach (var day in workTime.Days)
        {
            var vm = _factory.CreateWorkTimeViewModel(day);

            if (day.Start.HasValue && day.Start.Value.Date == today)
            {
                _updater.DayViewModel = vm;
                _updater.Start();
            }

            _allWorkTimes.Add(vm);
        }

        WorkTimes.Replace(_allWorkTimes);
    }

    internal async Task LoadSettings()
    {
        _settings = await _settingsStorage.Load();

        if (_settings != null)
        {
            SelectedFilter = Filters.FirstOrDefault(x => x.Filter == _settings.Filter);
            return;
        }
        SelectedFilter = Filters.FirstOrDefault();
    }
}
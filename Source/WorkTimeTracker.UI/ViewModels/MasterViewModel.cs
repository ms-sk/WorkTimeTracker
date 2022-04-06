using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WorkTimeTracker.Core.Extensions;
using WorkTimeTracker.Core.Models;
using WorkTimeTracker.Core.Storage;
using WorkTimeTracker.Core.Wpf.ViewModels;
using WorkTimeTracker.UI.Factories;

namespace WorkTimeTracker.UI.ViewModels;

public sealed class MasterViewModel : ViewModel
{
    private readonly IDayStorage _dayStorage;
    private readonly SettingsStorage _settingsStorage;
    private readonly ViewModelFactory _factory;
    private readonly WorkTimeTodayUpdater _updater;
    private readonly DayFactory _dayFactory;
    private readonly List<DayViewModel> _allWorkTimes = new();

    Settings? _settings;

    public MasterViewModel(
        IDayStorage dayStorage,
        SettingsStorage settingsStorage,
        ViewModelFactory factory,
        WorkTimeTodayUpdater updater,
        FooterViewModel footer,
        DayFactory dayFactory)
    {
        _dayStorage = dayStorage ?? throw new ArgumentNullException(nameof(dayStorage));
        _settingsStorage = settingsStorage ?? throw new ArgumentNullException(nameof(settingsStorage));
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        _updater = updater ?? throw new ArgumentNullException(nameof(updater));
        _dayFactory = dayFactory ?? throw new ArgumentNullException(nameof(dayFactory));
        Footer = footer ?? throw new ArgumentNullException(nameof(footer));
        Filters.Replace(factory.CreateFilterViewModels());
        SelectedFilter = Filters.FirstOrDefault();

        SelectedFilterChanged += async (_, _) =>
        {
            if (_settings != null && SelectedFilter != null)
            {
                _settings.Filter = SelectedFilter.Filter;
            }

            await _settingsStorage.Save(_settings ?? new Settings());

            Filter();

            await Footer.Update(WorkTimes.ToList());
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

    public FooterViewModel Footer { get; }

    public DayViewModel? SelectedDay
    {
        get => GetValue<DayViewModel>();
        set
        {
            SetValue(value);
            SelectedDayChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public async Task Init()
    {
        await _updater.Init();
    }

    public async Task InitDay()
    {
        var todayViewModel = WorkTimes.FirstOrDefault(d => d.Date?.Date == DateTime.Today);
        if (todayViewModel != null)
        {
            return;
        }

        todayViewModel = new DayViewModel
        {
            Date = DateTime.Today,
            StartTime = TimeOnly.FromDateTime(DateTime.Now)
        };
        WorkTimes.Add(todayViewModel);
        await _dayStorage.Save(new List<Day> { _dayFactory.CreateDay(todayViewModel) });
        await LoadWorkTimes();
    }

    void Filter()
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
            SelectedDay = WorkTimes.FirstOrDefault();
        }
    }

    internal async Task LoadWorkTimes()
    {
        _updater.Stop();

        WorkTimes.Clear();
        _allWorkTimes.Clear();

        var days = await _dayStorage.Load();
        days = days.OrderByDescending(x => x.Start).ToList();

        var today = DateTime.Today;
        foreach (var day in days)
        {
            var vm = _factory.CreateWorkTimeViewModel(day);

            if (day.Start.Date == today)
            {
                _updater.DayViewModel = vm;
            }

            _allWorkTimes.Add(vm);

            SelectedDay = WorkTimes.FirstOrDefault();
        }

        WorkTimes.Replace(_allWorkTimes);
        await Footer.Update(WorkTimes.ToList());

        if(_updater?.DayViewModel != null)
        {
            _updater.Start();
        }
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
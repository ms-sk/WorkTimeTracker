using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Core.Extensions;
using Core.Models;
using Core.Storage;
using Core.Wpf.ViewModels;
using WorkTimeTracker.Factories;

namespace WorkTimeTracker.ViewModels;

public sealed class MasterViewModel : ViewModel
{
    private readonly IStorage<WorkTime> _workTimeStorage;
    private readonly IStorage<List<Day>> _dayStorage;
    private readonly SettingsStorage _settingsStorage;
    private readonly WorkTimeViewModelFactory _factory;
    private readonly WorkTimeTodayUpdater _updater;
    private readonly DayFactory _dayFactory;
    private readonly List<DayViewModel> _allWorkTimes = new();

    Settings? _settings;

    public MasterViewModel(
        IStorage<WorkTime> workTimeStorage,
        IStorage<List<Day>> dayStorage,
        SettingsStorage settingsStorage,
        WorkTimeViewModelFactory factory,
        WorkTimeTodayUpdater updater,
        FooterViewModel footer,
        DayFactory dayFactory)
    {
        _workTimeStorage = workTimeStorage ?? throw new ArgumentNullException(nameof(workTimeStorage));
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
        }
    }

    internal async Task LoadWorkTimes()
    {
        _updater.Stop();

        WorkTimes.Clear();
        _allWorkTimes.Clear();

        var workTime = await _workTimeStorage.Load();
        workTime.Days = workTime.Days.OrderByDescending(x => x.Start).ToList();
        
        var today = DateTime.Today;
        foreach (var day in workTime.Days)
        {
            var vm = _factory.CreateWorkTimeViewModel(day);

            if (day.Start.Date == today)
            {
                _updater.DayViewModel = vm;
                _updater.Start();
            }

            _allWorkTimes.Add(vm);
        }

        WorkTimes.Replace(_allWorkTimes);
        await Footer.Update(WorkTimes.ToList());
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
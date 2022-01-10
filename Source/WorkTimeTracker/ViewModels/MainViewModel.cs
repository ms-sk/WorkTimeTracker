using Core.Extensions;
using Core.Storage;
using Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using WorkTimeTracker.Builder;
using WorkTimeTracker.Factories;
using WorkTimeTracker.ViewModels;

namespace WorkTimeTracker
{
    internal sealed class MainViewModel : ViewModel
    {
        readonly IStorage<WorkTime> _storage;
        readonly WorkTimeTodayUpdater _updater;
        readonly WorkTimeUpdater _workTimeUpdater;
        readonly List<DayViewModel> _workTimes = new();
        readonly WorkTimeViewModelFactory _factory;

        FilterViewModel? _filter;

        public MainViewModel(IStorage<WorkTime> storage, WorkTimeViewModelFactory factory, WorkTimeDtoFactory dtoFactory, WorkTimeTodayUpdater updater, WorkTimeUpdater workTimeUpdater, SumViewModel sum)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _updater = updater ?? throw new ArgumentNullException(nameof(updater));
            _workTimeUpdater = workTimeUpdater ?? throw new ArgumentNullException(nameof(workTimeUpdater));
            Sum = sum ?? throw new ArgumentNullException(nameof(storage));

            Filters.Replace(factory.CreateFilterViewModels());
            SelectedFilter = Filters.FirstOrDefault();

            _workTimeUpdater.GetWorkTime = () =>
            {
                List<Day> days = new();
                foreach (var day in _workTimes)
                {
                    if (day.Dto != null)
                    {
                        days.Add(day.Dto);
                    }
                }

                return dtoFactory.CreateWorkTime(days);
            };
            _workTimeUpdater.Start();

            WorkTimes.CollectionChanged += UpdateSumOnCollectionChanged;
        }

        public void Filter()
        {
            if (SelectedFilter != null)
            {
                var filtered = _workTimes.Where(wt => SelectedFilter.Matches(wt));
                WorkTimes.Replace(filtered);
            }
        }

        public ObservableCollection<DayViewModel> WorkTimes { get; } = new ObservableCollection<DayViewModel>();

        public ObservableCollection<FilterViewModel> Filters { get; } = new ObservableCollection<FilterViewModel>();

        public SumViewModel Sum { get; }

        public FilterViewModel? SelectedFilter
        {
            get => _filter;
            set
            {
                SetValue(ref _filter, value);
            }
        }

        internal async Task LoadWorkTimes()
        {
            _workTimes.Clear();
            var workTime = await _storage.Load();
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

                _workTimes.Add(vm);
            }

            WorkTimes.Replace(_workTimes);
        }

        void UpdateSumOnCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Sum.Sum = WorkTimes.Sum(d =>
            {
                if (d?.Dto?.Time != null)
                {
                    return d.Dto.Time.Value;
                }
                else
                {
                    return 0.0M;
                }
            });

            Sum.BreakSum = WorkTimes.Sum(d =>
            {
                if (d?.Dto?.Break != null)
                {
                    return d.Dto.Break.Value;
                }
                else
                {
                    return 0.0M;
                }
            });

            Sum.DisplayText = $"{Sum.Sum} / {Sum.BreakSum + Sum.Sum}";
        }

    }
}

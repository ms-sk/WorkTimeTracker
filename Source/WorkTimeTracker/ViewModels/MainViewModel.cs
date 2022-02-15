using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Core.Extensions;
using Core.Storage;
using Dtos;
using WorkTimeTracker.Builder;
using WorkTimeTracker.Factories;

namespace WorkTimeTracker.ViewModels
{
    internal sealed class MainViewModel : ViewModel
    {
        readonly IStorage<WorkTime> _workTimeStorage;
        private readonly IStorage<Settings> _settingsStorage;
        readonly WorkTimeTodayUpdater _updater;
        readonly WorkTimeUpdater _workTimeUpdater;
        private readonly MasterViewModel _masterViewModel;
        private readonly DetailsViewModel _detailsViewModel;
        readonly List<DayViewModel> _workTimes = new();
        readonly WorkTimeViewModelFactory _factory;
        
        Settings? _settings;

        public MainViewModel(IStorage<WorkTime> workTimeStorage, IStorage<Settings> settingsStorage, WorkTimeViewModelFactory factory, WorkTimeDtoFactory dtoFactory, WorkTimeTodayUpdater updater, WorkTimeUpdater workTimeUpdater, SumViewModel sum, MasterViewModel masterViewModel, DetailsViewModel detailsViewModel)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _workTimeStorage = workTimeStorage ?? throw new ArgumentNullException(nameof(workTimeStorage));
            _settingsStorage = settingsStorage ?? throw new ArgumentNullException(nameof(settingsStorage));
            _updater = updater ?? throw new ArgumentNullException(nameof(updater));
            _workTimeUpdater = workTimeUpdater ?? throw new ArgumentNullException(nameof(workTimeUpdater));
            MasterViewModel = masterViewModel ?? throw new ArgumentNullException(nameof(masterViewModel));
            DetailsViewModel = detailsViewModel ?? throw new ArgumentNullException(nameof(detailsViewModel));
            Sum = sum ?? throw new ArgumentNullException(nameof(workTimeStorage));

            // Filters.Replace(factory.CreateFilterViewModels());
            // SelectedFilter = Filters.FirstOrDefault();

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
            //_workTimeUpdater.Start();

            //WorkTimes.CollectionChanged += UpdateSumOnCollectionChanged;

            SelectedFilterChanged += (_, __) =>
            {
                _settings.Filter = SelectedFilter.Filter;
                _settingsStorage.Save(_settings);
            };
        }

        public event EventHandler SelectedFilterChanged;

        // public void Filter()
        // {
        //     if (SelectedFilter != null)
        //     {
        //         var filtered = _workTimes.Where(wt => SelectedFilter.Matches(wt));
        //         WorkTimes.Replace(filtered);
        //     }
        // }

        public MasterViewModel MasterViewModel { get; }

        public DetailsViewModel DetailsViewModel { get; }

        // public ObservableCollection<FilterViewModel> Filters { get; } = new ObservableCollection<FilterViewModel>();

        public SumViewModel Sum { get; }

        public FilterViewModel? SelectedFilter
        {
            get => GetValue<FilterViewModel?>();
            set
            {
                SetValue(value);
                OnSelectedFilterChanged();
            }
        }

        internal async Task LoadSettings()
        {
            _settings = await _settingsStorage.Load();

            //SelectedFilter = Filters.FirstOrDefault(x => x.Filter == _settings.Filter);
        }

        // void UpdateSumOnCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        // {
        //     Sum.Sum = WorkTimes.Sum(d =>
        //     {
        //         if (d?.Dto?.Time != null)
        //         {
        //             return d.Dto.Time.Value;
        //         }
        //         else
        //         {
        //             return 0.0M;
        //         }
        //     });
        //
        //     Sum.BreakSum = WorkTimes.Sum(d =>
        //     {
        //         if (d?.Dto?.Break != null)
        //         {
        //             return d.Dto.Break.Value;
        //         }
        //         else
        //         {
        //             return 0.0M;
        //         }
        //     });
        //
        //     Sum.DisplayText = $"{Sum.Sum} / {Sum.BreakSum + Sum.Sum}";
        // }

        void OnSelectedFilterChanged()
        {
            SelectedFilterChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}

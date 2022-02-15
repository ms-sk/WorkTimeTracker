using System;
using System.Collections.Generic;
using Core.Dtos;
using Core.Storage;
using WorkTimeTracker.Builder;
using WorkTimeTracker.Factories;

namespace WorkTimeTracker.ViewModels
{
    public sealed class MainViewModel : ViewModel
    {
        readonly IStorage<WorkTime> _workTimeStorage;
        private readonly IStorage<Settings> _settingsStorage;
        readonly WorkTimeTodayUpdater _updater;
        readonly WorkTimeUpdater _workTimeUpdater;
        readonly List<DayViewModel> _workTimes = new();
        readonly WorkTimeViewModelFactory _factory;

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
        }        



        public MasterViewModel MasterViewModel { get; }

        public DetailsViewModel DetailsViewModel { get; }

        public SumViewModel Sum { get; }

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
    }
}

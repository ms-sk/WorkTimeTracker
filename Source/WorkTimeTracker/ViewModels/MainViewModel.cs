using System;

namespace WorkTimeTracker.ViewModels
{
    public sealed class MainViewModel : ViewModel
    {
        // readonly IStorage<WorkTime> _workTimeStorage;
        // private readonly IStorage<Settings> _settingsStorage;
        //readonly WorkTimeTodayUpdater _updater;
        // readonly WorkTimeViewModelFactory _factory;

        public MainViewModel(
            MasterViewModel masterViewModel,
            DetailsViewModel detailsViewModel,
            ToolbarViewModel toolbarViewModel
            //WorkTimeTodayUpdater updater
            // IStorage<WorkTime> workTimeStorage,
            // IStorage<Settings> settingsStorage,
            // WorkTimeViewModelFactory factory,
            //WorkTimeDtoFactory dtoFactory,
            //WorkTimeUpdater workTimeUpdater,
            //SumViewModel sum
            )
        {
            MasterViewModel = masterViewModel ?? throw new ArgumentNullException(nameof(masterViewModel));
            DetailsViewModel = detailsViewModel ?? throw new ArgumentNullException(nameof(detailsViewModel));
            ToolbarViewModel = toolbarViewModel ?? throw new ArgumentNullException(nameof(ToolbarViewModel));

            // _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            // _workTimeStorage = workTimeStorage ?? throw new ArgumentNullException(nameof(workTimeStorage));
            // _settingsStorage = settingsStorage ?? throw new ArgumentNullException(nameof(settingsStorage));
            // _updater = updater ?? throw new ArgumentNullException(nameof(updater));
            //Sum = sum ?? throw new ArgumentNullException(nameof(sum));

            // if (workTimeUpdater == null) throw new ArgumentNullException(nameof(workTimeUpdater));
            // workTimeUpdater.GetWorkTime = () =>
            // {
            //     List<Day> days = new();
            //     foreach (var day in MasterViewModel.WorkTimes)
            //     {
            //         if (day.Dto != null)
            //         {
            //             days.Add(day.Dto);
            //         }
            //     }
            //
            //     return dtoFactory.CreateWorkTime(days);
            // };
            //_workTimeUpdater.Start();

            MasterViewModel.SelectedDayChanged += (_, _) =>
            {
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

            //WorkTimes.CollectionChanged += UpdateSumOnCollectionChanged;
        }

        public MasterViewModel MasterViewModel { get; }

        public DetailsViewModel? DetailsViewModel
        {
            get => GetValue<DetailsViewModel>();
            private set => SetValue(value);
        }

        public ToolbarViewModel ToolbarViewModel { get; }

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

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Core.Extensions;
using Core.Storage;
using Dtos;
using WorkTimeTracker.Builder;

namespace WorkTimeTracker.ViewModels;

internal sealed class MasterViewModel : ViewModel
{
    private readonly IStorage<WorkTime> _workTimeStorage;
    private readonly WorkTimeViewModelFactory _factory;
    private readonly WorkTimeTodayUpdater _updater;

    public MasterViewModel(IStorage<WorkTime> workTimeStorage, WorkTimeViewModelFactory factory, WorkTimeTodayUpdater updater)
    {
        _workTimeStorage = workTimeStorage ?? throw new ArgumentNullException(nameof(workTimeStorage));
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        _updater = updater ?? throw new ArgumentNullException(nameof(updater));
    }
    
    public ObservableCollection<DayViewModel> WorkTimes { get; } = new ObservableCollection<DayViewModel>();
    
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

            WorkTimes.Add(vm);
        }
    }
}
using Core.Math;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using Core.Storage;
using WorkTimeTracker.Factories;
using Core.Models;

namespace WorkTimeTracker.ViewModels
{
    public sealed class WorkTimeTodayUpdater
    {
        readonly WorkTimeViewModelFactory _factory;
        readonly IStorage<List<Day>> _dayStorage;
        readonly IStorage<Settings> _settingsStorage;
        
        Timer? _timer;

        public WorkTimeTodayUpdater(WorkTimeViewModelFactory factory, IStorage<List<Day>> dayStorage, IStorage<Settings> settingsStorage)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _dayStorage = dayStorage ?? throw new ArgumentNullException(nameof(dayStorage));
            _settingsStorage = settingsStorage;
        }

        public async Task Init()
        {
            var settings = await _settingsStorage.Load();
            
            _timer = new(settings.DefaultUpdateInterval.TotalMilliseconds);
            _timer.Elapsed += UpdateDayViewModel;
        }

        void UpdateDayViewModel(object? sender, ElapsedEventArgs e)
        {
            if (DayViewModel?.Dto != null)
            {
                DayViewModel.Dto.End = DateTime.Now;

                var time = (DayViewModel.Dto.End - DayViewModel.Dto.Start).GetValueOrDefault().TotalHours;
                DayViewModel.Dto.Time = CMath.RoundQuarter(time);

                _factory.UpdateDayViewModel(DayViewModel, DayViewModel.Dto);
                _dayStorage.Save(new List<Day> {DayViewModel.Dto});
            }
        }

        public DayViewModel? DayViewModel { get; set; }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            if(_timer is {Enabled: true})
            {
                _timer.Stop();
            }
        }
    }
}
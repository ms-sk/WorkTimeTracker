﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using WorkTimeTracker.Core.Math;
using WorkTimeTracker.Core.Models;
using WorkTimeTracker.Core.Storage;
using WorkTimeTracker.UI.Factories;

namespace WorkTimeTracker.UI.ViewModels
{
    public sealed class WorkTimeTodayUpdater
    {
        readonly ViewModelFactory _factory;
        readonly IDayStorage _dayStorage;
        readonly ISettingsStorage _settingsStorage;

        Timer? _timer;

        public WorkTimeTodayUpdater(ViewModelFactory factory, IDayStorage dayStorage, ISettingsStorage settingsStorage)
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
            if(_timer == null)
            {
                throw new NullReferenceException(nameof(_timer));
            }

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
﻿using System;
using System.Timers;
using WorkTimeTracker.Builder;

namespace WorkTimeTracker
{
    internal sealed class WorkTimeTodayUpdater
    {
        readonly WorkTimeViewModelFactory _factory;
        readonly Timer _timer;

        public WorkTimeTodayUpdater(WorkTimeViewModelFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _timer = new(10000);
            _timer.Elapsed += UpdateDayViewModel;
        }

        void UpdateDayViewModel(object? sender, ElapsedEventArgs e)
        {
            if (DayViewModel?.Dto != null)
            {
                DayViewModel.Dto.End = DateTime.Now;

                var time = (DayViewModel.Dto.End - DayViewModel.Dto.Start).GetValueOrDefault().TotalHours;
                var rounded = Math.Round(time * 4, MidpointRounding.ToEven) / 4.0;
                DayViewModel.Dto.Time = (decimal)rounded;

                _factory.UpdateDayViewModel(DayViewModel, DayViewModel.Dto);
            }
        }

        public DayViewModel? DayViewModel { get; set; }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}
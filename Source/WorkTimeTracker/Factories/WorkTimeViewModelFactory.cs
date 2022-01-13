using Dtos;
using System;
using System.Collections.Generic;
using WorkTimeTracker.ViewModels;
using Core.Models;

namespace WorkTimeTracker.Builder
{
    internal sealed class WorkTimeViewModelFactory
    {
        public DayViewModel CreateWorkTimeViewModel(Day day)
        {
            var dayViewModel = new DayViewModel();
            return UpdateDayViewModel(dayViewModel, day);
        }

        public DayViewModel UpdateDayViewModel(DayViewModel viewModel, Day day)
        {
            viewModel.Dto = day;
            viewModel.Date = day.Start.HasValue ? day.Start.Value.Date.ToShortDateString() : string.Empty;
            viewModel.StartTime = day.Start.HasValue ? day.Start.Value.ToShortTimeString() : string.Empty;
            viewModel.EndTime = day.End.HasValue ? day.End.Value.ToShortTimeString() : string.Empty;

            if (day.Time == null)
            {
                var time = (day.End - day.Start).GetValueOrDefault().TotalHours;
                var rounded = Math.Round(time * 4, MidpointRounding.ToEven) / 4.0;
                day.Time = (decimal)rounded;
            }

            if (day.Break == null)
            {
                if (day.Time <= 6.0M)
                {
                    day.Break = decimal.Zero;
                }
                else if (day.Time < 9.0M)
                {
                    day.Break = 0.5M;
                }
                else if (day.Time < 10.0M)
                {
                    day.Break = 0.75M;
                }
                else
                {
                    day.Break = 1M;
                }
            }

            var actualWorkTime = (day.Time - day.Break) ?? 0.0M;
            viewModel.WorkTime = actualWorkTime.ToString("F") ?? "0.00";
            viewModel.Break = day.Break?.ToString("F") ?? "0.00";

            return viewModel;
        }

        public List<FilterViewModel> CreateFilterViewModels()
        {
            return new List<FilterViewModel>()
            {
                new FilterViewModel{ Filter = Filter.None, DisplayText = "All" },
                new FilterViewModel{ Filter = Filter.Today, DisplayText = "Today"  },
                new FilterViewModel{ Filter = Filter.Week, DisplayText = "Current Week" },
                new FilterViewModel{ Filter = Filter.LastWeek, DisplayText = "Last Week" },
                new FilterViewModel{ Filter = Filter.Month , DisplayText = "Current Month"},
                new FilterViewModel{ Filter = Filter.LastMonth , DisplayText = "Last Month"},
                new FilterViewModel{ Filter = Filter.Year, DisplayText = "Current Year" },
                new FilterViewModel{ Filter = Filter.LastYear, DisplayText = "Last Year" },
            };
        }
    }
}

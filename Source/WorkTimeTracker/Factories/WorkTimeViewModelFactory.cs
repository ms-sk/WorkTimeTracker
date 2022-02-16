using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using WorkTimeTracker.ViewModels;
using Core.Models;
using Core.Math;
using Core.Dtos;
using Core.Extensions;

namespace WorkTimeTracker.Builder
{
    public sealed class WorkTimeViewModelFactory
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
                day.Time = CMath.RoundQuarter(time);
            }

            if (day.Break == null)
            {
                day.Break = CMath.CalculateBreak(day.Time.GetValueOrDefault());
            }

            var actualWorkTime = (day.Time - day.Break) ?? 0.0M;
            viewModel.WorkTime = actualWorkTime.ToString("F") ?? "0.00";
            viewModel.Break = day.Break?.ToString("F") ?? "0.00";

            if (day.Tasks?.Any() == true)
            {
                viewModel.Tasks.Replace(day.Tasks.Select(CreateTaskViewModel));
            }

            return viewModel;
        }

        public TaskViewModel CreateTaskViewModel(TaskDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            return new TaskViewModel
            {
                Description = dto.Description,
                WorkTime = dto.WorkTime
            };
        }

        public List<FilterViewModel> CreateFilterViewModels()
        {
            return new List<FilterViewModel>()
            {
                new FilterViewModel{ Filter = Filter.None, DisplayText = "All" },
                new FilterViewModel{ Filter = Filter.Today, DisplayText = "Today" },
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

using System;
using System.Collections.Generic;
using System.Linq;
using Core.Dtos;
using Core.Extensions;
using Core.Math;
using Core.Models;
using WorkTimeTracker.ViewModels;

namespace WorkTimeTracker.Factories
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class WorkTimeViewModelFactory
    {
        public DayViewModel CreateWorkTimeViewModel(Day dto)
        {
            return UpdateDayViewModel(new DayViewModel(), dto);
        }

        public DayViewModel UpdateDayViewModel(DayViewModel viewModel, Day dto)
        {
            viewModel.Dto = dto;
            viewModel.Date = dto.Start.Date;
            viewModel.StartTime = TimeOnly.FromDateTime(dto.Start);
            viewModel.EndTime = dto.End.HasValue ? TimeOnly.FromDateTime(dto.End.Value) : TimeOnly.MinValue;
            viewModel.Type = dto.Type;

            var time = dto.Time.GetValueOrDefault();
            if (time == 0.0 && viewModel.EndTime > TimeOnly.MinValue)
            {
                time = (viewModel.EndTime - viewModel.StartTime).Value.TotalHours;
                time = CMath.RoundQuarter(time);
            }

            var breakTime = dto.Break.GetValueOrDefault();
            if (breakTime == 0.0 && (viewModel.Type != WorkType.Illness && viewModel.Type != WorkType.Holiday))
            {
                breakTime = CMath.CalculateBreak(time);
            }

            var actualWorkTime = time - breakTime;
            actualWorkTime = actualWorkTime < 0.0 ? 0.0 : actualWorkTime;

            viewModel.WorkTime = actualWorkTime;
            viewModel.Break = breakTime;

            if (dto.Tasks?.Any() == true)
            {
                viewModel.Tasks.Replace(dto.Tasks.Select(CreateTaskViewModel));
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
            return new List<FilterViewModel>
            {
                new() { Filter = Filter.None, DisplayText = "All" },
                new() { Filter = Filter.Today, DisplayText = "Today" },
                new() { Filter = Filter.Week, DisplayText = "Current Week" },
                new() { Filter = Filter.LastWeek, DisplayText = "Last Week" },
                new() { Filter = Filter.Month , DisplayText = "Current Month"},
                new() { Filter = Filter.LastMonth , DisplayText = "Last Month"},
                new() { Filter = Filter.Year, DisplayText = "Current Year" },
                new() { Filter = Filter.LastYear, DisplayText = "Last Year" },
            };
        }
    }
}

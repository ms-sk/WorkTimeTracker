using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WorkTimeTracker.Core.Extensions;
using WorkTimeTracker.Core.Math;
using WorkTimeTracker.Core.Models;
using WorkTimeTracker.Core.Wpf;
using WorkTimeTracker.UI.ViewModels;

namespace WorkTimeTracker.UI.Factories
{
    public sealed class ViewModelFactory
    {
        public DayViewModel CreateWorkTimeViewModel(Day dto)
        {
            var viewModel = new DayViewModel
            {
                Dto = dto,
                Date = dto.Start.Date,
                StartTime = TimeOnly.FromDateTime(dto.Start),
                EndTime = dto.End.HasValue ? TimeOnly.FromDateTime(dto.End.Value) : TimeOnly.MinValue,
                Type = dto.Type
            };

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
                Application.Current.Dispatcher.Invoke(() => viewModel.Tasks.Replace(dto.Tasks.Select(CreateTaskViewModel)));
            }

            return viewModel;
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
                Application.Current.Dispatcher.Invoke(() => viewModel.Tasks.Replace(dto.Tasks.Select(CreateTaskViewModel)));
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
                new() { Filter = Filter.None, DisplayText = Translations.All },
                new() { Filter = Filter.Today, DisplayText = Translations.Today },
                new() { Filter = Filter.Week, DisplayText = Translations.CurrentWeek },
                new() { Filter = Filter.LastWeek, DisplayText = Translations.LastWeek },
                new() { Filter = Filter.Month , DisplayText = Translations.CurrentMonth},
                new() { Filter = Filter.LastMonth , DisplayText = Translations.LastMonth},
                new() { Filter = Filter.Year, DisplayText = Translations.CurrentYear},
                new() { Filter = Filter.LastYear, DisplayText = Translations.LastYear },
            };
        }
    }
}

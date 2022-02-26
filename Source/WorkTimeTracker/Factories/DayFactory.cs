using Core.Dtos;
using System;
using WorkTimeTracker.ViewModels;

namespace WorkTimeTracker.Factories
{
    public sealed class DayFactory
    {
        public Day CreateDay(DayViewModel viewModel)
        {
            if (viewModel is null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            var day = new Day
            {
                Id = viewModel.Dto?.Id ?? Guid.NewGuid(),
                Tasks = new(),
                Break = viewModel.Break,
                Type = viewModel.Type
            };

            if (viewModel.Date.HasValue)
            {
                day.Start = viewModel.Date.Value.Add(viewModel.StartTime.ToTimeSpan());
                day.End = viewModel.EndTime.HasValue ? viewModel.Date.Value.Add(viewModel.EndTime.Value.ToTimeSpan()) : null;
            }

            foreach (var task in viewModel.Tasks)
            {
                day.Tasks.Add(new TaskDto { Description = task.Description, WorkTime = task.WorkTime, Type = task.Type });
            }

            viewModel.Dto = day;

            return day;
        }
    }
}

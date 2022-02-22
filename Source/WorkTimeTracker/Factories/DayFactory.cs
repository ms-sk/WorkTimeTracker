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
                Tasks = new System.Collections.Generic.List<TaskDto>(),
                Break = viewModel.Break
            };

            if (viewModel.Date.HasValue)
            {
                    day.Start = viewModel.Date.Value.Add(viewModel.StartTime.ToTimeSpan());
                    day.End = viewModel.EndTime.HasValue ? viewModel.Date.Value.Add(viewModel.EndTime.Value.ToTimeSpan()) : null;
            }

            foreach (var task in viewModel.Tasks)
            {
                day.Tasks.Add(new TaskDto { Description = task.Description, WorkTime = task.WorkTime });
            }

            viewModel.Dto = day;

            return day;
        }
    }
}

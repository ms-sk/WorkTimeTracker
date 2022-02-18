using Core.Dtos;
using System;
using WorkTimeTracker.ViewModels;

namespace WorkTimeTracker.Factories
{
    public sealed class DayFactory
    {
        public Day CreateDay(DayViewModel dayViewModel)
        {
            if (dayViewModel is null)
            {
                throw new ArgumentNullException(nameof(dayViewModel));
            }

            var day = new Day
            {
                Id = dayViewModel.Dto.Id,
                Tasks = new System.Collections.Generic.List<TaskDto>()
            };

            if (decimal.TryParse(dayViewModel.Break, out var br))
            {
                day.Break = br;
            }

            if (DateTime.TryParse(dayViewModel.StartTime, out var start))
            {
                day.Start = start;
            }

            if (DateTime.TryParse(dayViewModel.EndTime, out var end))
            {
                day.End = end;
            }

            foreach (var task in dayViewModel.Tasks)
            {

                day.Tasks.Add(new TaskDto() { Description = task.Description, WorkTime = task.WorkTime });
            }

            dayViewModel.Dto = day;

            return day;
        }
    }
}

using Core.Dtos;
using System.Collections.Generic;

namespace WorkTimeTracker.Factories
{
    public sealed class WorkTimeDtoFactory
    {
        public WorkTime CreateWorkTime(List<Day> days)
        {
            return new WorkTime()
            {
                Days = days
            };
        }
    }
}

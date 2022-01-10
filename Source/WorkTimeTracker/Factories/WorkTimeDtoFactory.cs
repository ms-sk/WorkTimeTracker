using Dtos;
using System.Collections.Generic;

namespace WorkTimeTracker.Factories
{
    internal sealed class WorkTimeDtoFactory
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

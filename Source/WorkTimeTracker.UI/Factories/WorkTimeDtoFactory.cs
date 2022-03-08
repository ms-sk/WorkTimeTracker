using System.Collections.Generic;
using WorkTimeTracker.Core.Models;

namespace WorkTimeTracker.UI.Factories
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

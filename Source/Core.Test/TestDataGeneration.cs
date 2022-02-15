using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Dtos;
using Core.Storage;
using Core.Configuration;
using System;

namespace Core.Test
{
    [TestClass]
    public class TestDataGeneration
    {
        [TestMethod]
        public void GenerateWorkTimesTestData()
        {
            var worktimeStorage = new WorkTimeStorage(new Paths());

            var worktime = new WorkTime();
            var date = new DateTime(2021, 12, 1);
            var rnd = new Random();
            while (date <= DateTime.Today)
            {
                var start = date.AddMinutes(rnd.Next(360, 480));
                var end = start.AddMinutes(rnd.Next(360, 540));

                var hours = (end - start).TotalHours;
                var rounded = Math.Round(hours * 4, MidpointRounding.ToEven) / 4.0;

                // TODO Fix and optimize break calculation
                var @break = 0.0;
                if (hours <= 6.0)
                {
                    @break = 0.0;
                }
                else if (hours <= 9.0)
                {
                    @break = 0.5;
                }
                else
                {
                    @break = 1;
                }

                worktime.Days.Add(new Day { Start = start, End = end, Time = (decimal)rounded, Break = (decimal)@break });

                date = date.AddDays(1);
            }

            worktimeStorage.Save(worktime);
        }
    }
}
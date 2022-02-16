using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Dtos;
using Core.Storage;
using Core.Configuration;
using System;
using Core.Math;

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

                worktime.Days.Add(new Day { Start = start, End = end, Time = CMath.RoundQuarter(hours), Break = CMath.CalculateBreak((decimal)hours) });

                date = date.AddDays(1);
            }

            worktimeStorage.Save(worktime);
        }
    }
}
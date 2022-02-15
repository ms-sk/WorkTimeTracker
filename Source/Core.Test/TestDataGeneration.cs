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
        [Ignore]
        public void GenerateWorkTimesTestData()
        {
            var worktimeStorage = new WorkTimeStorage(new Paths());

            var worktime = new WorkTime();
            var date = new DateTime(2021, 12, 1);
            var rnd = new Random();
            while(date <= DateTime.Today)
            {
                var start = date.AddMinutes(rnd.Next(360, 480));

                worktime.Days.Add(new Day { Start = start, End = null, Break = 0, Time = 0 });

                date = date.AddDays(1);
            }

            worktimeStorage.Save(worktime);
        }
    }
}
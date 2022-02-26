using Core.Dtos;
using Core.Models;
using Core.Storage;
using Core.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace WorkTimeTracker.ViewModels
{
    public sealed class FooterViewModel : ViewModel
    {
        readonly SettingsStorage storage;

        public FooterViewModel(SettingsStorage storage)
        {
            foreach (var workType in Enum.GetValues<WorkType>())
            {
                Sums.Add(new SumViewModel { Type = workType, DisplayText = workType.ToString() });
            }

            OverTime = new SumViewModel() { Type = WorkType.Work, DisplayText = "Overtime", Sum = 0 };
            Sums.Add(OverTime);
            this.storage = storage;
        }

        public ObservableCollection<SumViewModel> Sums { get; } = new ObservableCollection<SumViewModel>();

        public SumViewModel OverTime { get; }

        public async Task Update(IEnumerable<DayViewModel> workTimes)
        {
            var groups = workTimes.GroupBy(g => g.Type);
            foreach (var group in groups)
            {
                var sum = Sums.FirstOrDefault(s => s.Type == group.Key);
                if (sum != null)
                {
                    sum.Sum = group.Sum(x => x.WorkTime);
                }

                if (group.Key == WorkType.Work)
                {
                    var sett = await storage.Load();

                    OverTime.Sum = sum.Sum - (sett.HoursPerDay * group.Count());
                }
            }

        }
    }
}

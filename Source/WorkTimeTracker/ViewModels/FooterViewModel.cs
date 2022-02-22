using Core.Models;
using Core.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace WorkTimeTracker.ViewModels
{
    public sealed class FooterViewModel : ViewModel
    {
        public FooterViewModel()
        {
            foreach (var workType in Enum.GetValues<WorkType>())
            {
                Sums.Add(new SumViewModel { Type = workType });
            }
        }

        public ObservableCollection<SumViewModel> Sums { get; } = new ObservableCollection<SumViewModel>();

        public void Update(IEnumerable<DayViewModel> workTimes)
        {
            var groups = workTimes.GroupBy(g => g.Type);
            foreach (var group in groups)
            {
                var sum = Sums.FirstOrDefault(s => s.Type == group.Key);
                if (sum != null)
                {
                    sum.Sum = group.Sum(x => x.WorkTime);
                }
            }
        }
    }
}

using Core.Dtos;
using Core.Models;
using Core.Storage;
using Core.Wpf.ViewModels;
using Resources;
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
                var translation = GetTranslation(workType);
                Sums.Add(new SumViewModel { Type = workType, DisplayText = translation });
            }

            OverTime = new SumViewModel() { Type = WorkType.Work, DisplayText = Translations.Overtime, Sum = 0 };
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

        string GetTranslation(WorkType workType)
        {
            switch (workType)
            {
                case WorkType.Work:
                    return Translations.Work;
                case WorkType.Illness:
                    return Translations.Illness;
                case WorkType.Holiday:
                    return Translations.Holiday;
                case WorkType.Education:
                    return Translations.Education;
                default:
                    return string.Empty;
            }
        }
    }
}

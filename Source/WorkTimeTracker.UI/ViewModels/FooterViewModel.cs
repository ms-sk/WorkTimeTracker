using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using WorkTimeTracker.Core.Models;
using WorkTimeTracker.Core.Storage;
using WorkTimeTracker.Core.Wpf;
using WorkTimeTracker.Core.Wpf.ViewModels;

namespace WorkTimeTracker.UI.ViewModels
{
    public sealed class FooterViewModel : ViewModel
    {
        readonly SettingsStorage _settingsStorage;

        public FooterViewModel(SettingsStorage settingsStorage)
        {
            _settingsStorage = settingsStorage;

            BuildSumViewModels();
        }

        void BuildSumViewModels()
        {
            foreach (var workType in Enum.GetValues<WorkType>())
            {
                var translation = GetTranslation(workType);
                Sums.Add(new SumViewModel { Type = workType, DisplayText = translation });
            }

            OverTime = new SumViewModel { Type = WorkType.Work, DisplayText = Translations.Overtime };
            Sums.Add(OverTime);
        }

        public ObservableCollection<SumViewModel> Sums { get; } = new ObservableCollection<SumViewModel>();

        public SumViewModel? OverTime { get; private set; }

        public async Task Update(IEnumerable<DayViewModel> workTimes)
        {
            var groups = workTimes.GroupBy(g => g.Type);
            foreach (var group in groups)
            {
                var sum = Sums.FirstOrDefault(s => s.Type == group.Key);
                if (sum == null)
                {
                    return;
                }

                sum.Sum = group.Sum(x => x.WorkTime);

                if (group.Key == WorkType.Work)
                {
                    var settings = await _settingsStorage.Load();

                    OverTime.Sum = sum.Sum - (settings.HoursPerDay * group.Count());
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

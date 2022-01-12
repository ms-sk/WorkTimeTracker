﻿using System;
using Core.Models;

namespace WorkTimeTracker.ViewModels
{
    internal sealed class FilterViewModel : ViewModel
    {
        public Filter Filter { get; set; }

        public string DisplayText { get; set; } = string.Empty;

        internal bool Matches(DayViewModel dayViewModel)
        {
            var today = DateTime.Today;

            var startDate = dayViewModel.Dto?.Start?.Date;

            switch (Filter)
            {
                case Filter.None:
                    return true;
                case Filter.Today:
                    return startDate == today;
                case Filter.Week:
                    var firstDayOfTheWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
                    return startDate >= firstDayOfTheWeek && startDate <= firstDayOfTheWeek.AddDays(6);
                case Filter.LastWeek:
                    var firstDayOfTheLastWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday).AddDays(-7);
                    return startDate >= firstDayOfTheLastWeek && startDate <= firstDayOfTheLastWeek.AddDays(6);
                case Filter.Month:
                    return dayViewModel.Dto?.Start?.Date.Month == today.Month && dayViewModel.Dto?.Start?.Date.Year == today.Year;
                case Filter.LastMonth:
                    return dayViewModel.Dto?.Start?.Date == today.AddMonths(-1);
                case Filter.Year:
                    return dayViewModel.Dto?.Start?.Date.Year == today.Year;
                case Filter.LastYear:
                    return dayViewModel.Dto?.Start?.Date.Year == today.AddYears(-1).Year;
            }

            return true;
        }
    }
}
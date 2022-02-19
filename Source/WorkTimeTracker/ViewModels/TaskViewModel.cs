using Core.Wpf.ViewModels;
using System;

namespace WorkTimeTracker.ViewModels;

public class TaskViewModel : ViewModel
{
    public string? Description
    {
        get => GetValue<string>();
        set => SetValue(value);
    }
        
    public decimal WorkTime
    {
        get => GetValue<decimal>();
        set => SetValue(value);
    }
}
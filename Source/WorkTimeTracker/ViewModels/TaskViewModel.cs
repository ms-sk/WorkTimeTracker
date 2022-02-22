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
        
    public double WorkTime
    {
        get => GetValue<double>();
        set => SetValue(value);
    }
}
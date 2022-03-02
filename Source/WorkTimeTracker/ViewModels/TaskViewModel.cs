using Core.Wpf.ViewModels;
using Core.Models;

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

    public WorkType Type
    {
        get => GetValue<WorkType>();
        set => SetValue(value);
    }
    public TaskDto? Dto { get; set; }
}
using System;
using System.Globalization;
using System.Windows.Data;

namespace WorkTimeTracker.Core.Wpf.Converter;

public sealed class TimeOnlyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is TimeOnly time)
        {
            return time.ToString();
        }

        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string str)
        {
            if (TimeOnly.TryParse(str, out var time))
            {
                return time;
            }
        }

        return TimeOnly.MinValue;
    }
}
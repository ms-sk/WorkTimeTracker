using System.Windows;
using System.Windows.Controls;

namespace Core.Wpf.Controls
{
    public sealed class CSeparator : Separator
    {
        static CSeparator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CSeparator), new FrameworkPropertyMetadata(typeof(CSeparator)));
        }
    }
}
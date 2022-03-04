using System.Windows;
using System.Windows.Controls;

namespace Core.Wpf.Controls
{
    public sealed class CListBox : ListBox
    {
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register(nameof(Header), typeof(object), typeof(CListBox), new PropertyMetadata(null));

        static CListBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CListBox), new FrameworkPropertyMetadata(typeof(CListBox)));
        }

        public object Header
        {
            get { return GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }
    }
}

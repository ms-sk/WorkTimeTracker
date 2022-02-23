using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Core.Wpf.Controls
{
    public sealed class CButton : Button
    {
        static CButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CButton), new FrameworkPropertyMetadata(typeof(CButton)));
        }

        public CButton()
        {
            Background = new SolidColorBrush(Colors.White);
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(nameof(Icon), typeof(object), typeof(CButton), new PropertyMetadata(null));

        public object Icon
        {
            get { return GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
    }
}

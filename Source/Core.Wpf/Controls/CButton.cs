using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Core.Wpf.Controls
{
    public sealed class CButton : Button
    {
        ContentPresenter? iconContentPresenter;
        ContentPresenter? contentPresenter;
        Border? border;
        Grid? grid;

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

        public static readonly DependencyProperty IconPositionProperty =
            DependencyProperty.Register(nameof(IconPosition), typeof(IconPosition), typeof(CButton), new PropertyMetadata(IconPosition.Left));


        public static readonly DependencyProperty MouseOverBorderBrushProperty =
            DependencyProperty.Register(nameof(MouseOverBorderBrush), typeof(Brush), typeof(CButton), new PropertyMetadata(null));

        public object Icon
        {
            get { return GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public IconPosition IconPosition
        {
            get { return (IconPosition)GetValue(IconPositionProperty); }
            set { SetValue(IconPositionProperty, value); }
        }

        public Brush MouseOverBorderBrush
        {
            get { return (Brush)GetValue(MouseOverBorderBrushProperty); }
            set { SetValue(MouseOverBorderBrushProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            iconContentPresenter = (ContentPresenter)GetTemplateChild("PART_Icon");
            contentPresenter = (ContentPresenter)GetTemplateChild("PART_Content");
            border = (Border)GetTemplateChild("PART_Border");
            border.BorderBrush = BorderBrush;
            grid = (Grid)GetTemplateChild("PART_Grid");

            if (iconContentPresenter == null || contentPresenter == null)
            {
                return;
            }

            if (IconPosition == IconPosition.Right)
            {
                Grid.SetColumn(contentPresenter, 0);
                Grid.SetColumn(iconContentPresenter, 2);

                grid.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
            }
            else if (IconPosition == IconPosition.Left)
            {
                Grid.SetColumn(contentPresenter, 2);
                Grid.SetColumn(iconContentPresenter, 0);

                grid.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Star);
            }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == IsMouseOverProperty)
            {
                var bl = (bool)e.NewValue;
                border.BorderBrush = bl ? MouseOverBorderBrush : BorderBrush;
            }
        }
    }
}

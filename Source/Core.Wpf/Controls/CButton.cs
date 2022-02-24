using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Core.Wpf.Controls
{
    public sealed class CButton : Button
    {
        ContentPresenter? iconContentPresenter;
        ContentPresenter? contentPresenter;
        private Grid grid;

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

        public IconPosition IconPosition
        {
            get { return (IconPosition)GetValue(IconPositionProperty); }
            set { SetValue(IconPositionProperty, value); }
        }

        public static readonly DependencyProperty IconPositionProperty =
            DependencyProperty.Register(nameof(IconPosition), typeof(IconPosition), typeof(CButton), new PropertyMetadata(IconPosition.Left));


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            iconContentPresenter = (ContentPresenter)GetTemplateChild("PART_Icon");
            contentPresenter = (ContentPresenter)GetTemplateChild("PART_Content");
            grid = (Grid)GetTemplateChild("PART_Grid");

            if (iconContentPresenter == null || contentPresenter == null)
            {
                return;
            }

            if (IconPosition == IconPosition.Right)
            {
                Grid.SetColumn(contentPresenter, 0);
                Grid.SetColumn(iconContentPresenter, 1);

                grid.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
            }
            else if (IconPosition == IconPosition.Left)
            {
                Grid.SetColumn(contentPresenter, 1);
                Grid.SetColumn(iconContentPresenter, 0);

                grid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
            }
        }
    }
}

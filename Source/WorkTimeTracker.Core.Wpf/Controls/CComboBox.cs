﻿using System.Windows;
using System.Windows.Controls;

namespace WorkTimeTracker.Core.Wpf.Controls
{
    public sealed class CComboBox : ComboBox
    {
        CButton? button;

        static CComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CComboBox), new FrameworkPropertyMetadata(typeof(CComboBox)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            button = (CButton)GetTemplateChild("ToggleButton");

            button.Click -= ToggleDropDown;
            button.Click += ToggleDropDown;
        }

        void ToggleDropDown(object sender, RoutedEventArgs e)
        {
            IsDropDownOpen = !IsDropDownOpen;
        }
    }
}

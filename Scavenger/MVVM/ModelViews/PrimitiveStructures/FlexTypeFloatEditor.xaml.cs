using Scavenger.MVVM.ViewModels.PrimitiveStructures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Scavenger.MVVM.ModelViews.PrimitiveStructures
{
    /// <summary>
    /// Interaction logic for FlexTypeFloatEditor.xaml
    /// </summary>
    public partial class FlexTypeFloatEditor : UserControl
    {
        public FlexTypeFloatViewModel FlexValue
        {
            get => (FlexTypeFloatViewModel)GetValue(FlexValueProperty);
            set => SetValue(FlexValueProperty, value);
        }

        public static readonly DependencyProperty FlexValueProperty = DependencyProperty.Register(
            "FlexValue",
            typeof(FlexTypeFloatViewModel),
            typeof(FlexTypeFloatEditor),
            new FrameworkPropertyMetadata(default(FlexTypeFloatViewModel), FrameworkPropertyMetadataOptions.AffectsRender, OnValueChanged));

        public FlexTypeFloatEditor()
        {
            InitializeComponent();
        }

        private static void OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            FlexTypeFloatEditor control = (FlexTypeFloatEditor)dependencyObject;
            if (eventArgs.NewValue is FlexTypeFloatViewModel flexValue)
            {
                control.FlexValue = flexValue;
            }
        }
    }
}

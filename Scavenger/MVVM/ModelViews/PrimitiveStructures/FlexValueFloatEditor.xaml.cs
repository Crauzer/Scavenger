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
    /// Interaction logic for FlexValueFloatEditor.xaml
    /// </summary>
    public partial class FlexValueFloatEditor : UserControl
    {
        public FlexValueFloatViewModel FlexValue
        {
            get => (FlexValueFloatViewModel)GetValue(FlexValueProperty);
            set => SetValue(FlexValueProperty, value);
        }

        public static readonly DependencyProperty FlexValueProperty = DependencyProperty.Register(
            "FlexValue",
            typeof(FlexValueFloatViewModel),
            typeof(FlexValueFloatEditor),
            new FrameworkPropertyMetadata(default(FlexValueFloatViewModel), FrameworkPropertyMetadataOptions.AffectsRender, OnValueChanged));

        public FlexValueFloatEditor()
        {
            InitializeComponent();
        }

        private static void OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            FlexValueFloatEditor control = (FlexValueFloatEditor)dependencyObject;
            if (eventArgs.NewValue is FlexValueFloatViewModel flexValue)
            {
                control.FlexValue = flexValue;
            }
        }
    }
}

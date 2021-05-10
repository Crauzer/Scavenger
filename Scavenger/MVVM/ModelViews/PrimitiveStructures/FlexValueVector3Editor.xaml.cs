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
    /// Interaction logic for FlexValueVector3Editor.xaml
    /// </summary>
    public partial class FlexValueVector3Editor : UserControl
    {
        public FlexValueVector3ViewModel FlexValue
        {
            get => (FlexValueVector3ViewModel)GetValue(FlexValueProperty);
            set => SetValue(FlexValueProperty, value);
        }

        public static readonly DependencyProperty FlexValueProperty = DependencyProperty.Register(
            "FlexValue",
            typeof(FlexValueVector3ViewModel),
            typeof(FlexValueVector3Editor),
            new FrameworkPropertyMetadata(default(FlexValueVector3ViewModel), FrameworkPropertyMetadataOptions.AffectsRender, OnValueChanged));

        public FlexValueVector3Editor()
        {
            InitializeComponent();
        }

        private static void OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            FlexValueVector3Editor control = (FlexValueVector3Editor)dependencyObject;
            if (eventArgs.NewValue is FlexValueVector3ViewModel flexValue)
            {
                control.FlexValue = flexValue;
            }
        }
    }
}

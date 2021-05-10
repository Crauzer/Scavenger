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
    /// Interaction logic for FlexValueVector2Editor.xaml
    /// </summary>
    public partial class FlexValueVector2Editor : UserControl
    {
        public FlexValueVector2ViewModel FlexValue
        {
            get => (FlexValueVector2ViewModel)GetValue(FlexValueProperty);
            set => SetValue(FlexValueProperty, value);
        }

        public static readonly DependencyProperty FlexValueProperty = DependencyProperty.Register(
            "FlexValue",
            typeof(FlexValueVector2ViewModel),
            typeof(FlexValueVector2Editor),
            new FrameworkPropertyMetadata(default(FlexValueVector2ViewModel), FrameworkPropertyMetadataOptions.AffectsRender, OnValueChanged));

        public FlexValueVector2Editor()
        {
            InitializeComponent();
        }

        private static void OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            FlexValueVector2Editor control = (FlexValueVector2Editor)dependencyObject;
            if (eventArgs.NewValue is FlexValueVector2ViewModel flexValue)
            {
                control.FlexValue = flexValue;
            }
        }
    }
}

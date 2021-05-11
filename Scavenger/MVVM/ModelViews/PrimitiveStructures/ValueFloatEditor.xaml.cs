using Scavenger.MVVM.ViewModels.PrimitiveStructures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for ValueFloatEditor.xaml
    /// </summary>
    public partial class ValueFloatEditor : UserControl, INotifyPropertyChanged
    {
        public ValueFloatViewModel Value
        {
            get => (ValueFloatViewModel)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof(ValueFloatViewModel),
            typeof(ValueFloatEditor),
            new FrameworkPropertyMetadata(default(ValueFloatViewModel), FrameworkPropertyMetadataOptions.AffectsRender, OnValueChanged));

        public event PropertyChangedEventHandler PropertyChanged;

        public ValueFloatEditor()
        {
            InitializeComponent();
        }

        private static void OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            ValueFloatEditor control = (ValueFloatEditor)dependencyObject;
            if(eventArgs.NewValue is ValueFloatViewModel value)
            {
                control.Value = value;
            }
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

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
        public float Value
        {
            get => (float)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof(float),
            typeof(ValueFloatEditor),
            new FrameworkPropertyMetadata(default(float), FrameworkPropertyMetadataOptions.AffectsRender, OnValueChanged));

        public event PropertyChangedEventHandler PropertyChanged;

        public ValueFloatEditor()
        {
            InitializeComponent();
        }

        private static void OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            ValueFloatEditor control = (ValueFloatEditor)dependencyObject;
            control.Value = (float)eventArgs.NewValue;
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

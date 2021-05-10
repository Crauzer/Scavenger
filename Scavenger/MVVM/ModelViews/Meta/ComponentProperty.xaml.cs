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

namespace Scavenger.MVVM.ModelViews.Meta
{
    /// <summary>
    /// Interaction logic for ComponentProperty.xaml
    /// </summary>
    public partial class ComponentProperty : ContentControl
    {
        public string PropertyName 
        {
            get => (string)GetValue(PropertyNameProperty);
            set => SetValue(PropertyNameProperty, value);
        }

        public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.Register(
            "PropertyName",
            typeof(string),
            typeof(ComponentProperty),
            new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.AffectsRender, OnPropertyNameChanged));

        public ComponentProperty()
        {
            InitializeComponent();
        }

        private static void OnPropertyNameChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            ComponentProperty control = (ComponentProperty)dependencyObject;
            control.PropertyName = (string)eventArgs.NewValue;
        }
    }
}

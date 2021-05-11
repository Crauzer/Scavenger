using Scavenger.MVVM.ViewModels.Meta;
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
    /// Interaction logic for MetaStructureEditor.xaml
    /// </summary>
    public partial class SomeablePropertyEditor : ContentControl
    {
        public string PropertyName
        {
            get => (string)GetValue(PropertyNameProperty);
            set => SetValue(PropertyNameProperty, value);
        }
        public object Someable
        {
            get => GetValue(SomeableProperty);
            set => SetValue(SomeableProperty, value);
        }

        public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.Register(
            "PropertyName",
            typeof(string),
            typeof(SomeablePropertyEditor),
            new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.AffectsRender, OnPropertyNameChanged));

        public static readonly DependencyProperty SomeableProperty = DependencyProperty.Register(
            "Someable",
            typeof(object),
            typeof(SomeablePropertyEditor),
            new FrameworkPropertyMetadata(default(object), FrameworkPropertyMetadataOptions.AffectsRender, OnSomeableChanged));

        public SomeablePropertyEditor()
        {
            InitializeComponent();
        }

        private static void OnPropertyNameChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            SomeablePropertyEditor control = (SomeablePropertyEditor)dependencyObject;
            control.PropertyName = (string)eventArgs.NewValue;
        }
        private static void OnSomeableChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            SomeablePropertyEditor control = (SomeablePropertyEditor)dependencyObject;
            control.Someable = eventArgs.NewValue;
        }
    }
}

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
    /// Interaction logic for ValueColorEditor.xaml
    /// </summary>
    public partial class ValueColorEditor : UserControl, INotifyPropertyChanged
    {
        public ValueColorViewModel Value
        {
            get => (ValueColorViewModel)GetValue(Color);
            set => SetValue(Color, value);
        }

        public static readonly DependencyProperty Color = DependencyProperty.Register(
            "Color",
            typeof(ValueColorViewModel),
            typeof(ValueColorEditor),
            new FrameworkPropertyMetadata(default(ValueColorViewModel), FrameworkPropertyMetadataOptions.AffectsRender, OnValueChanged));

        public event PropertyChangedEventHandler PropertyChanged;

        public ValueColorEditor()
        {
            InitializeComponent();
        }

        private static void OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            ValueColorEditor control = (ValueColorEditor)dependencyObject;
            if (eventArgs.NewValue is ValueColorViewModel value)
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

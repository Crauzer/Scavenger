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
    /// Interaction logic for ColorEditor.xaml
    /// </summary>
    public partial class ColorEditor : UserControl, INotifyPropertyChanged
    {
        public ColorViewModel Color
        {
            get => (ColorViewModel)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            "Color",
            typeof(ColorViewModel),
            typeof(ColorEditor),
            new FrameworkPropertyMetadata(default(ColorViewModel), FrameworkPropertyMetadataOptions.AffectsRender, OnColorChanged));

        public event PropertyChangedEventHandler PropertyChanged;

        public ColorEditor()
        {
            InitializeComponent();
        }

        private static void OnColorChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            ColorEditor control = (ColorEditor)dependencyObject;
            if (eventArgs.NewValue is ColorViewModel color)
            {
                control.Color = color;
            }
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

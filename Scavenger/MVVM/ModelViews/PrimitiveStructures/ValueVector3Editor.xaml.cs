using LeagueToolkit.Meta.Classes;
using Scavenger.MVVM.ViewModels.PrimitiveStructures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
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
    /// Interaction logic for Vector3Editor.xaml
    /// </summary>
    public partial class ValueVector3Editor : UserControl, INotifyPropertyChanged
    {
        public ValueVector3ViewModel Vector
        {
            get => (ValueVector3ViewModel)GetValue(VectorProperty);
            set => SetValue(VectorProperty, value);
        }

        public static readonly DependencyProperty VectorProperty = DependencyProperty.Register(
            "Vector", 
            typeof(ValueVector3ViewModel),
            typeof(ValueVector3Editor),
            new FrameworkPropertyMetadata(default(ValueVector3ViewModel), FrameworkPropertyMetadataOptions.AffectsRender, OnVectorChanged));

        public event PropertyChangedEventHandler PropertyChanged;

        public ValueVector3Editor()
        {
            InitializeComponent();
        }

        private static void OnVectorChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            ValueVector3Editor control = (ValueVector3Editor)dependencyObject;
            if (eventArgs.NewValue is ValueVector3ViewModel valueVector3)
            {
                control.Vector = valueVector3;
            }
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

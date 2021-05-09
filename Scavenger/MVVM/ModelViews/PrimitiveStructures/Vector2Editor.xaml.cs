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
    /// Interaction logic for Vector2Editor.xaml
    /// </summary>
    public partial class Vector2Editor : UserControl, INotifyPropertyChanged
    {
        public Vector2ViewModel Vector
        {
            get => (Vector2ViewModel)GetValue(VectorProperty);
            set => SetValue(VectorProperty, value);
        }

        public static readonly DependencyProperty VectorProperty = DependencyProperty.Register(
            "Vector",
            typeof(Vector2ViewModel),
            typeof(Vector2Editor),
            new FrameworkPropertyMetadata(default(Vector2ViewModel), FrameworkPropertyMetadataOptions.AffectsRender, OnVectorChanged));

        public event PropertyChangedEventHandler PropertyChanged;

        public Vector2Editor()
        {
            InitializeComponent();
        }

        private static void OnVectorChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            Vector2Editor control = (Vector2Editor)dependencyObject;
            if (eventArgs.NewValue is Vector2ViewModel vector)
            {
                control.Vector = vector;
            }
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

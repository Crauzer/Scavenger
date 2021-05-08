using MaterialDesignExtensions.Controls;
using Scavenger.MVVM.ViewModels.MetaExplorer;
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
using System.Windows.Shapes;

namespace Scavenger.MVVM.ModelViews
{
    /// <summary>
    /// Interaction logic for MetaExplorerWindow.xaml
    /// </summary>
    public partial class MetaExplorerWindow : MaterialWindow
    {
        public MetaExplorerWindow()
        {
            InitializeComponent();

            this.DataContext = new MetaExplorerViewModel();
        }
    }
}

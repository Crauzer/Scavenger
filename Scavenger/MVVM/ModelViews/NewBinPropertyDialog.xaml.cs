using LeagueToolkit.IO.PropertyBin;
using MaterialDesignThemes.Wpf;
using Scavenger.IO.Templates;
using Scavenger.MVVM.ViewModels;
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

namespace Scavenger.MVVM.ModelViews
{
    /// <summary>
    /// Interaction logic for NewBinPropertyDialog.xaml
    /// </summary>
    public partial class NewBinPropertyDialog : UserControl
    {
        public NewBinPropertyDialog(IEnumerable<StructureTemplate> structureTemplates, IEnumerable<BinPropertyType> restrictTo = null)
        {
            InitializeComponent();

            this.DataContext = new NewBinPropertyViewModel(structureTemplates, restrictTo);
        }

        public void OnOpen(object sender, DialogOpenedEventArgs eventArgs)
        {

        }
    }
}

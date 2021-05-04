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
    /// Interaction logic for NewBinTreeObjectDialog.xaml
    /// </summary>
    public partial class NewBinTreeObjectDialog : UserControl
    {
        public NewBinTreeObjectDialog(ICollection<StructureTemplate> structureTemplates)
        {
            InitializeComponent();

            this.DataContext = new NewBinTreeObjectViewModel(structureTemplates);
        }
    }
}

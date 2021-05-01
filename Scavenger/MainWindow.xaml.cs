using LeagueToolkit.IO.PropertyBin;
using Microsoft.WindowsAPICodePack.Dialogs;
using ModernWpf.Controls;
using Scavenger.MVVM.ModelViews;
using Scavenger.MVVM.ViewModels;
using Scavenger.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PathIO = System.IO.Path;

namespace Scavenger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel { get; private set; } = new MainWindowViewModel();

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this.ViewModel;

            InitializeViewModel();
        }

        public async void InitializeViewModel()
        {
            this.ViewModel.Initialize();

            await this.ViewModel.UpdateHashtables();

            Hashtables.Load();
        }

        private async void OnFileOpen(object sender, RoutedEventArgs e)
        {
            using CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filters.Add(new CommonFileDialogFilter("BIN Files", "*.bin"));

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                await this.ViewModel.LoadBinTree(PathIO.GetFileName(dialog.FileName), new BinTree(dialog.FileName));
            }
        }

        private async void OnBinTreeStructureAddField(object sender, RoutedEventArgs e)
        {
            if (e.Source is FrameworkElement frameworkElement &&
                frameworkElement.DataContext is BinTreeParentViewModel parentViewModel)
            {
                NewBinPropertyDialog dialog = new NewBinPropertyDialog(null);

                ContentDialogResult result = await dialog.ShowAsync(ContentDialogPlacement.Popup);
                if (result == ContentDialogResult.Primary)
                {
                    NewBinPropertyViewModel dialogViewModel = dialog.DataContext as NewBinPropertyViewModel;
                    BinTreeProperty newProperty = dialogViewModel.BuildProperty(parentViewModel.TreeProperty.Parent);
                    BinTreePropertyViewModel newPropertyViewModel = BinTreeUtilities.ConstructTreePropertyViewModel(parentViewModel, newProperty);

                    parentViewModel.Children.Add(newPropertyViewModel);
                }
            }
        }

        private void OnBinTreePropertyDeleteField(object sender, RoutedEventArgs e)
        {
            if(e.Source is FrameworkElement frameworkElement && 
                frameworkElement.DataContext is BinTreePropertyViewModel propertyViewModel)
            {
                propertyViewModel.Parent.RemoveField(propertyViewModel);
            }
        }
    }
}

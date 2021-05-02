using LeagueToolkit.Helpers;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using Microsoft.WindowsAPICodePack.Dialogs;
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

            DialogHelper.RootDialog = this.RootDialog;

            await this.ViewModel.UpdateHashtables();

            Hashtables.Load();
        }

        private async void OnBinFileOpen(object sender, RoutedEventArgs e)
        {
            using CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filters.Add(new CommonFileDialogFilter("BIN Files", "*.bin"));

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                await this.ViewModel.LoadBinTree(PathIO.GetFileName(dialog.FileName), new BinTree(dialog.FileName));
            }
        }

        private void OnBinFileSave(object sender, RoutedEventArgs e)
        {
            using CommonSaveFileDialog dialog = new CommonSaveFileDialog();
            dialog.Filters.Add(new CommonFileDialogFilter("BIN Files", "*.bin"));

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                BinTree binTree = this.ViewModel.SelectedBinTree.BuildBinTree();
                binTree.Write(dialog.FileName, FileVersionProvider.GetSupportedVersions(LeagueFileType.PropertyBin).Last());
            }
        }

        private async void OnBinTreeStructureAddField(object sender, RoutedEventArgs e)
        {
            if (e.Source is FrameworkElement frameworkElement &&
                frameworkElement.DataContext is BinTreeParentViewModel parentViewModel)
            {
                NewBinPropertyViewModel dialogViewModel = await DialogHelper.ShowNewBinPropertyDialog(null);
                if(dialogViewModel is not null)
                {
                    BinTreeProperty newProperty = dialogViewModel.BuildProperty(parentViewModel.TreeProperty.Parent);
                    BinTreePropertyViewModel newPropertyViewModel = BinTreeUtilities.ConstructTreePropertyViewModel(parentViewModel, newProperty);

                    parentViewModel.Children.Add(newPropertyViewModel);
                }
            }
        }

        private void OnBinTreePropertyDeleteField(object sender, RoutedEventArgs e)
        {
            if (e.Source is FrameworkElement frameworkElement &&
                frameworkElement.DataContext is BinTreePropertyViewModel propertyViewModel)
            {
                propertyViewModel.Parent.RemoveField(propertyViewModel);
            }
        }

        private async void OnBinTreeContainerAddItem(object sender, RoutedEventArgs e)
        {
            if (e.Source is FrameworkElement frameworkElement &&
                frameworkElement.DataContext is BinTreeContainerViewModel containerViewModel)
            {
                BinTreeContainer container = containerViewModel.TreeProperty as BinTreeContainer;
                NewBinPropertyViewModel dialogViewModel = await DialogHelper.ShowNewBinPropertyDialog(new List<BinPropertyType>() { container.PropertiesType });
                if(dialogViewModel is not null)
                {
                    BinTreeProperty newProperty = dialogViewModel.BuildProperty(container);
                    BinTreePropertyViewModel newPropertyViewModel = BinTreeUtilities.ConstructTreePropertyViewModel(containerViewModel, newProperty);

                    newPropertyViewModel.ShowName = false;

                    containerViewModel.Children.Add(newPropertyViewModel);
                }
            }
        }

        private void OnBinTreeTabClose(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is FrameworkElement frameworkElement &&
                frameworkElement.DataContext is BinTreeViewModel binTreeViewModel)
            {
                this.ViewModel.BinTrees.Remove(binTreeViewModel);

                if (this.ViewModel.BinTrees.Count != 0)
                {
                    this.ViewModel.SelectedBinTree = this.ViewModel.BinTrees.Last();
                }
            }
        }
    }
}

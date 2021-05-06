using HelixToolkit.Wpf;
using LeagueToolkit.Helpers;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using MaterialDesignExtensions.Controls;
using MaterialDesignThemes.Wpf;
using Microsoft.WindowsAPICodePack.Dialogs;
using Scavenger.MVVM.ModelViews;
using Scavenger.MVVM.ViewModels;
using Scavenger.Utilities;
using Scavenger.Utilities.Wrappers;
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
    public partial class MainWindow : MaterialWindow
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

            DialogHelper.MessageDialog = this.MessageDialog;
            DialogHelper.RootDialog = this.RootDialog;

            try
            {
                await this.ViewModel.UpdateHashtables();
            }
            catch (Exception exception) 
            { 
                await DialogHelper.ShowMessgeDialog($"Failed to update Hashtables\n{exception}");

                this.ViewModel.Infobar.Reset();
                this.ViewModel.IsGloballyEnabled = true;
            }

            Hashtables.Load();
        }

        private async void OnBinFileOpen(object sender, RoutedEventArgs e)
        {
            using CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filters.Add(new CommonFileDialogFilter("BIN Files", "*.bin"));

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                try
                {
                    await this.ViewModel.LoadBinTree(dialog.FileName, new BinTree(dialog.FileName));
                }
                catch(Exception exception)
                {
                    await DialogHelper.ShowMessgeDialog($"Failed to load BIN Tree\n{exception}");
                }
            }
        }
        private async void OnBinFileSave(object sender, RoutedEventArgs e)
        {
            using CommonSaveFileDialog dialog = new CommonSaveFileDialog();
            dialog.Filters.Add(new CommonFileDialogFilter("BIN Files", "*.bin"));

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                try
                {
                    BinTree binTree = this.ViewModel.SelectedBinTree.BuildBinTree();
                    binTree.Write(dialog.FileName, FileVersionProvider.GetSupportedVersions(LeagueFileType.PropertyBin).Last());
                }
                catch(Exception exception)
                {
                    await DialogHelper.ShowMessgeDialog($"Failed to save BIN Tree\n{exception}");

                    this.ViewModel.Infobar.Reset();
                    this.ViewModel.IsGloballyEnabled = true;
                }
            }
        }

        private void OnBinTreeTabClose(object sender, RoutedEventArgs e)
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

        private async void OnBinTreeObjectAddItem(object sender, RoutedEventArgs e)
        {
            if (e.Source is FrameworkElement frameworkElement &&
                frameworkElement.DataContext is BinTreeObjectViewModel objectViewModel)
            {
                NewBinPropertyViewModel dialogViewModel = await DialogHelper.ShowNewBinPropertyDialog(null);
                if (dialogViewModel is not null)
                {
                    BinTreeProperty newProperty = dialogViewModel.BuildProperty(objectViewModel.TreeObject);
                    BinTreePropertyViewModel newPropertyViewModel = BinTreeUtilities.ConstructTreePropertyViewModel(objectViewModel, newProperty);
                    if (newPropertyViewModel is not null)
                    {
                        objectViewModel.Children.Add(newPropertyViewModel);
                    }
                }
            }
        }
        private void OnBinTreeObjectDelete(object sender, RoutedEventArgs e)
        {
            if (e.Source is FrameworkElement frameworkElement &&
                frameworkElement.DataContext is BinTreeObjectViewModel propertyViewModel)
            {
                this.ViewModel.SelectedBinTree.Objects.Remove(propertyViewModel);
            }
        }

        private async void OnBinTreeStructureAddField(object sender, RoutedEventArgs e)
        {
            if (e.Source is FrameworkElement frameworkElement &&
                frameworkElement.DataContext is BinTreeParentViewModel parentViewModel)
            {
                await this.ViewModel.AddFieldToStructure(parentViewModel);
            }
        }
        private async void OnBinTreeContainerAddItem(object sender, RoutedEventArgs e)
        {
            if (e.Source is FrameworkElement frameworkElement &&
                frameworkElement.DataContext is BinTreeParentViewModel parentViewModel)
            {
                await this.ViewModel.AddItemToContainer(parentViewModel);
            }
        }
        private void OnBinTreeMapAddItem(object sender, RoutedEventArgs e)
        {
            if (e.Source is FrameworkElement frameworkElement &&
                frameworkElement.DataContext is BinTreeMapViewModel mapViewModel)
            {
                BinTreeMap map = mapViewModel.TreeProperty as BinTreeMap;
                BinTreeProperty keyProperty = BinTreeUtilities.BuildProperty("", "", map, map.KeyType, BinPropertyType.None, BinPropertyType.None);
                BinTreeProperty valueProperty = BinTreeUtilities.BuildProperty("", "", map, map.ValueType, BinPropertyType.None, BinPropertyType.None);

                BinTreeMapEntryViewModel newEntryViewModel = new BinTreeMapEntryViewModel(mapViewModel, new KeyValuePair<BinTreeProperty, BinTreeProperty>(keyProperty, valueProperty));

                mapViewModel.Children.Add(newEntryViewModel);
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

        private async void OnBinTreeStructureExportToTemplate(object sender, RoutedEventArgs e)
        {
            if (e.Source is FrameworkElement frameworkElement &&
                frameworkElement.DataContext is BinTreeParentViewModel structureViewModel)
            {
                await this.ViewModel.ExportStructureToTemplate(structureViewModel);
            }
        }

        private void OnBinTreeContainerDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is ColorZone colorZone
                && colorZone.Content is TreeViewItem treeViewItem
                && e.OriginalSource is FrameworkElement originalSource
                && e.OriginalSource is not TreeViewItem
                && (originalSource.DataContext is BinTreeContainerViewModel || originalSource.DataContext is BinTreeUnorderedContainerViewModel))
            {
                treeViewItem.IsExpanded = false;
            }
        }
        private void OnBinTreeObjectDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is ColorZone colorZone
                && colorZone.Content is TreeViewItem treeViewItem
                && e.OriginalSource is FrameworkElement originalSource
                && e.OriginalSource is not TreeViewItem
                && originalSource.DataContext is BinTreeObjectViewModel)
            {
                treeViewItem.IsExpanded = false;
            }
        }
        private void OnBinTreeStructureDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is ColorZone colorZone
                && colorZone.Content is TreeViewItem treeViewItem
                && e.OriginalSource is FrameworkElement originalSource
                && e.OriginalSource is not TreeViewItem
                && (originalSource.DataContext is BinTreeStructureViewModel || originalSource.DataContext is BinTreeEmbeddedViewModel))
            {
                treeViewItem.IsExpanded = false;
            }
        }
        private void OnBinTreeMapDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is ColorZone colorZone
                && colorZone.Content is TreeViewItem treeViewItem
                && e.OriginalSource is FrameworkElement originalSource
                && e.OriginalSource is not TreeViewItem
                && originalSource.DataContext is BinTreeMapViewModel)
            {
                treeViewItem.IsExpanded = false;
            }
        }

        private void CopyCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void CopyCommandOnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if(e.OriginalSource is FrameworkElement originalSource)
            {
                IDataObject dataObject = new DataObject();

                if(originalSource.DataContext is BinTreeObjectViewModel objectViewModel)
                {
                    dataObject.SetData(new BinTreeObjectViewModelDataWrapper(objectViewModel));
                }
                else if (originalSource.DataContext is BinTreePropertyViewModel propertyViewModel)
                {
                    dataObject.SetData(new BinTreePropertyViewModelDataWrapper(propertyViewModel));
                }

                Clipboard.SetDataObject(dataObject, false);
            }
        }
        private void PasteCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if(e.OriginalSource is FrameworkElement originalSource
                && Clipboard.GetDataObject() is IDataObject dataObject)
            {
                // BAD CODE PLS HELP
                string[] formats = dataObject.GetFormats();
                if (formats.Any(x => x.Contains("BinTree")))
                {
                    object data = dataObject.GetData(formats.First());

                    if(originalSource.DataContext is BinTreeViewModel binTreeViewModel
                        && data is BinTreeObjectViewModelDataWrapper)
                    {
                        e.CanExecute = true;
                    }
                    else if(originalSource.DataContext is BinTreeParentViewModel
                        && data is BinTreePropertyViewModelDataWrapper)
                    {
                        e.CanExecute = true;
                    }
                }
            }
        }
        private void PasteCommandOnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (Clipboard.GetDataObject() is IDataObject dataObject
                && e.OriginalSource is FrameworkElement originalSource)
            {
                // BAD CODE PLS HELP
                string[] formats = dataObject.GetFormats();
                if (formats.Any(x => x.Contains("BinTree")))
                {
                    object data = dataObject.GetData(formats.First());

                    if (originalSource.DataContext is BinTreeParentViewModel parentViewModel 
                        && data is BinTreePropertyViewModelDataWrapper propertyViewModelWrapper)
                    {
                        propertyViewModelWrapper.Property.Parent = parentViewModel;
                        propertyViewModelWrapper.Property.SyncTreeProperty();

                        parentViewModel.Children.Add(propertyViewModelWrapper.Property);
                    }
                    else if(originalSource.DataContext is BinTreeViewModel binTreeViewModel
                        && data is BinTreeObjectViewModelDataWrapper objectViewModel)
                    {
                        objectViewModel.Object.BinTree = binTreeViewModel;
                        objectViewModel.Object.SyncTreeObject();

                        binTreeViewModel.Objects.Add(objectViewModel.Object);
                    }
                }
            }
        }
    }
}

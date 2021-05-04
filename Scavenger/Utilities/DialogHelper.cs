using LeagueToolkit.IO.PropertyBin;
using MaterialDesignThemes.Wpf;
using Scavenger.IO.Templates;
using Scavenger.MVVM.ModelViews;
using Scavenger.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scavenger.Utilities
{
    public static class DialogHelper
    {
        public static DialogHost MessageDialog { get; set; }
        public static DialogHost RootDialog { get; set; }

        public static async Task<NewBinPropertyViewModel> ShowNewBinPropertyDialog(ICollection<StructureTemplate> structureTemplates, ICollection<BinPropertyType> restrictTo = null)
        {
            NewBinPropertyDialog dialog = new NewBinPropertyDialog(structureTemplates, restrictTo);

            object result = await DialogHost.Show(dialog, nameof(RootDialog), dialog.OnOpen, null);
            if(result is true)
            {
                return dialog.DataContext as NewBinPropertyViewModel;
            }
            else
            {
                return null;
            }
        }
        public static async Task<NewBinTreeObjectViewModel> ShowNewBinTreeObjectDialog(ICollection<StructureTemplate> structureTemplates)
        {
            NewBinTreeObjectDialog dialog = new (structureTemplates);

            object result = await DialogHost.Show(dialog, nameof(RootDialog));
            if (result is true)
            {
                return dialog.DataContext as NewBinTreeObjectViewModel;
            }
            else
            {
                return null;
            }
        }

        public static async Task ShowMessgeDialog(string message)
        {
            MessageDialog dialog = new MessageDialog(message);

            await DialogHost.Show(dialog, nameof(MessageDialog));
        }

        public static async Task<ExportStructureTemplateViewModel> ShowExportStructureTemplateDialog()
        {
            ExportStructureTemplateDialog dialog = new ExportStructureTemplateDialog();

            object result = await DialogHost.Show(dialog, nameof(RootDialog));
            if(result is true)
            {
                return dialog.DataContext as ExportStructureTemplateViewModel;
            }
            else
            {
                return null;
            }
        }
    }
}

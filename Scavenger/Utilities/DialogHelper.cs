using LeagueToolkit.IO.PropertyBin;
using MaterialDesignThemes.Wpf;
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

        public static async Task<NewBinPropertyViewModel> ShowNewBinPropertyDialog(IEnumerable<BinPropertyType> restrictTo)
        {
            NewBinPropertyDialog dialog = new NewBinPropertyDialog(restrictTo);

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
    }
}

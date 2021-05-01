using LeagueToolkit.IO.PropertyBin.Properties;
using Scavenger.Utilities;
using System.Collections.ObjectModel;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeOptionalViewModel : BinTreePropertyViewModel
    {
        public override string Header => $"{this.Name} -> Optional : {(this.TreeProperty as BinTreeOptional).ValueType}";
        public ObservableCollection<BinTreePropertyViewModel> Children
        {
            get => this._children;
            set
            {
                this._children = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<BinTreePropertyViewModel> _children = new ObservableCollection<BinTreePropertyViewModel>();

        public BinTreeOptionalViewModel(BinTreeOptional treeProperty) : base(treeProperty)
        {
            BinTreePropertyViewModel valueViewModel = BinTreeUtilities.ConstructTreePropertyViewModel(treeProperty.Value);
            valueViewModel.ShowName = false;

            this.Children.Add(valueViewModel);
        }
    }
}

using LeagueToolkit.IO.PropertyBin;
using System.Collections.ObjectModel;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeParentViewModel : BinTreePropertyViewModel
    {
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
    
        public BinTreeParentViewModel(BinTreeParentViewModel parent, BinTreeProperty treeProperty) : base(parent, treeProperty)
        {

        }
    }
}

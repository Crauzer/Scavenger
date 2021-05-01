using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using Scavenger.Utilities;
using System.Collections.ObjectModel;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeEmbeddedViewModel : BinTreePropertyViewModel
    {
        public override string Header => $"{this.Name} -> {this.TreeProperty.Type} : {Hashtables.GetType((this.TreeProperty as BinTreeStructure).MetaClassHash)}";
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

        public BinTreeEmbeddedViewModel(BinTreeEmbedded treeProperty) : base(treeProperty)
        {
            foreach (BinTreeProperty genericProperty in treeProperty.Properties)
            {
                this.Children.Add(BinTreeUtilities.ConstructTreePropertyViewModel(genericProperty));
            }
        }
    }
}

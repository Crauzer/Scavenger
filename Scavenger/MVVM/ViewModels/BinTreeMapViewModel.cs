using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using Scavenger.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeMapViewModel : BinTreePropertyViewModel
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

        public BinTreeMapViewModel(BinTreeMap treeProperty) : base(treeProperty)
        {
            
        }
    }
}

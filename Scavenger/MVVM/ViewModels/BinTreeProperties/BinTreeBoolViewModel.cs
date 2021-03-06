using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeBoolViewModel : BinTreePropertyViewModel
    {
        public bool Value
        {
            get => this._value;
            set
            {
                this._value = value;
                NotifyPropertyChanged();
                SyncTreeProperty();
            }
        }

        private bool _value;

        public BinTreeBoolViewModel() { }
        public BinTreeBoolViewModel(BinTreeParentViewModel parent, BinTreeBool treeProperty) : base(parent, treeProperty) 
        {
            this.Value = treeProperty.Value;
        }

        public override void SyncTreeProperty()
        {
            this.TreeProperty = new BinTreeBool((IBinTreeParent)this.Parent?.TreeProperty, this.NameHash, this.Value);
        }

        public override BinTreeProperty BuildProperty()
        {
            return new BinTreeBool(null, this.NameHash, this.Value);
        }
    }
}

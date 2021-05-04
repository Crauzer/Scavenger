using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeSByteViewModel : BinTreePropertyViewModel
    {
        public sbyte Value
        {
            get => this._value;
            set
            {
                this._value = value;
                NotifyPropertyChanged();
                SyncTreeProperty();
            }
        }
        
        public sbyte MaxValue => sbyte.MaxValue;
        public sbyte MinValue => sbyte.MinValue;

        private sbyte _value;

        public BinTreeSByteViewModel() { }
        public BinTreeSByteViewModel(BinTreeParentViewModel parent, BinTreeSByte treeProperty) : base(parent, treeProperty)
        {
            this.Value = treeProperty.Value;
        }

        public override void SyncTreeProperty()
        {
            this.TreeProperty = new BinTreeSByte((IBinTreeParent)this.Parent?.TreeProperty, this.NameHash, this.Value);
        }

        public override BinTreeProperty BuildProperty()
        {
            return new BinTreeSByte(null, this.NameHash, this.Value);
        }
    }
}

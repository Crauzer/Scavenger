using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeByteViewModel : BinTreePropertyViewModel
    {
        public byte Value
        {
            get => this._value;
            set
            {
                this._value = value;
                NotifyPropertyChanged();
            }
        }
        public byte MaxValue => byte.MaxValue;
        public byte MinValue => byte.MinValue;

        private byte _value;

        public BinTreeByteViewModel(BinTreeParentViewModel parent, BinTreeByte treeProperty) : base(parent, treeProperty)
        {
            this.Value = treeProperty.Value;
        }

        public override BinTreeProperty BuildProperty()
        {
            return new BinTreeByte(null, this.NameHash, this.Value);
        }
    }
}

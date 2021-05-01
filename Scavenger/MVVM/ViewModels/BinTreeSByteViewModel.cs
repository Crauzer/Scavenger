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
            }
        }
        public sbyte MaxValue => sbyte.MaxValue;
        public sbyte MinValue => sbyte.MinValue;

        private sbyte _value;

        public BinTreeSByteViewModel(BinTreeParentViewModel parent, BinTreeSByte treeProperty) : base(parent, treeProperty)
        {
            this.Value = treeProperty.Value;
        }

        public override BinTreeProperty BuildProperty()
        {
            uint nameHash = Fnv1a.HashLower(this.Name);

            return new BinTreeSByte(null, nameHash, this.Value);
        }
    }
}

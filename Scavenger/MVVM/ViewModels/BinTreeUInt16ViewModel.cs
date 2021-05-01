using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeUInt16ViewModel : BinTreePropertyViewModel
    {
        public ushort Value
        {
            get => this._value;
            set
            {
                this._value = value;
                NotifyPropertyChanged();
            }
        }
        public ushort MaxValue => ushort.MaxValue;
        public ushort MinValue => ushort.MinValue;

        private ushort _value;

        public BinTreeUInt16ViewModel(BinTreeParentViewModel parent, BinTreeUInt16 treeProperty) : base(parent, treeProperty)
        {
            this.Value = treeProperty.Value;
        }

        public override BinTreeProperty BuildProperty()
        {
            uint nameHash = Fnv1a.HashLower(this.Name);

            return new BinTreeUInt16(null, nameHash, this.Value);
        }
    }
}

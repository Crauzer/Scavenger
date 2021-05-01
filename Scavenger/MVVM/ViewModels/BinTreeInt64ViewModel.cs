using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeInt64ViewModel : BinTreePropertyViewModel
    {
        public long Value
        {
            get => this._value;
            set
            {
                this._value = value;
                NotifyPropertyChanged();
            }
        }
        public long MaxValue => long.MaxValue;
        public long MinValue => long.MinValue;

        private long _value;

        public BinTreeInt64ViewModel(BinTreeParentViewModel parent, BinTreeInt64 treeProperty) : base(parent, treeProperty)
        {
            this.Value = treeProperty.Value;
        }

        public override BinTreeProperty BuildProperty()
        {
            uint nameHash = Fnv1a.HashLower(this.Name);

            return new BinTreeInt64(null, nameHash, this.Value);
        }
    }
}

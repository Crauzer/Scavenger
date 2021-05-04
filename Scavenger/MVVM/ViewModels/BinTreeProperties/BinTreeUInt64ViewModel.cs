using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeUInt64ViewModel : BinTreePropertyViewModel
    {
        public ulong Value
        {
            get => this._value;
            set
            {
                this._value = value;
                NotifyPropertyChanged();
                SyncTreeProperty();
            }
        }
        public ulong MaxValue => ulong.MaxValue;
        public ulong MinValue => ulong.MinValue;

        private ulong _value;

        public BinTreeUInt64ViewModel() { }
        public BinTreeUInt64ViewModel(BinTreeParentViewModel parent, BinTreeUInt64 treeProperty) : base(parent, treeProperty)
        {
            this.Value = treeProperty.Value;
        }

        public override void SyncTreeProperty()
        {
            this.TreeProperty = new BinTreeUInt64((IBinTreeParent)this.Parent?.TreeProperty, this.NameHash, this.Value);
        }

        public override BinTreeProperty BuildProperty()
        {
            return new BinTreeUInt64(null, this.NameHash, this.Value);
        }
    }
}

using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeInt16ViewModel : BinTreePropertyViewModel
    {
        public short Value
        {
            get => this._value;
            set
            {
                this._value = value;
                NotifyPropertyChanged();
                SyncTreeProperty();
            }
        }
        public short MaxValue => short.MaxValue;
        public short MinValue => short.MinValue;

        private short _value;

        public BinTreeInt16ViewModel() { }
        public BinTreeInt16ViewModel(BinTreeParentViewModel parent, BinTreeInt16 treeProperty) : base(parent, treeProperty)
        {
            this.Value = treeProperty.Value;
        }

        public override void SyncTreeProperty()
        {
            this.TreeProperty = new BinTreeInt16((IBinTreeParent)this.Parent?.TreeProperty, this.NameHash, this.Value);
        }

        public override BinTreeProperty BuildProperty()
        {
            return new BinTreeInt16(null, this.NameHash, this.Value);
        }
    }
}

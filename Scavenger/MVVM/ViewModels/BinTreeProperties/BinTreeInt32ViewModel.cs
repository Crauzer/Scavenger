using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeInt32ViewModel : BinTreePropertyViewModel
    {
        public int Value
        {
            get => this._value;
            set
            {
                this._value = value;
                NotifyPropertyChanged();
            }
        }
        public int MaxValue => int.MaxValue;
        public int MinValue => int.MinValue;

        private int _value;

        public BinTreeInt32ViewModel() { }
        public BinTreeInt32ViewModel(BinTreeParentViewModel parent, BinTreeInt32 treeProperty) : base(parent, treeProperty)
        {
            this.Value = treeProperty.Value;
        }

        public override BinTreeProperty BuildProperty()
        {
            return new BinTreeInt32(null, this.NameHash, this.Value);
        }
    }
}

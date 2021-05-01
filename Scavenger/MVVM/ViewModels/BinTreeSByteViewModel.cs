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
    }
}

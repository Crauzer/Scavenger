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

        public BinTreeUInt16ViewModel(BinTreeUInt16 treeProperty) : base(treeProperty)
        {
            this.Value = treeProperty.Value;
        }
    }
}

using LeagueToolkit.IO.PropertyBin.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeUInt32ViewModel : BinTreePropertyViewModel
    {
        public uint Value
        {
            get => this._value;
            set
            {
                this._value = value;
                NotifyPropertyChanged();
            }
        }
        public uint MaxValue => uint.MaxValue;
        public uint MinValue => uint.MinValue;

        private uint _value;

        public BinTreeUInt32ViewModel(BinTreeUInt32 treeProperty) : base(treeProperty)
        {
            this.Value = treeProperty.Value;
        }
    }
}

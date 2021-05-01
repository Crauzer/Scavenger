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
            }
        }
        public ulong MaxValue => ulong.MaxValue;
        public ulong MinValue => ulong.MinValue;

        private ulong _value;

        public BinTreeUInt64ViewModel(BinTreeUInt64 treeProperty) : base(treeProperty)
        {
            this.Value = treeProperty.Value;
        }
    }
}

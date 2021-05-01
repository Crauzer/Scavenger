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

        public BinTreeInt32ViewModel(BinTreeInt32 treeProperty) : base(treeProperty)
        {
            this.Value = treeProperty.Value;
        }
    }
}

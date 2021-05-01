using LeagueToolkit.IO.PropertyBin.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeStringViewModel : BinTreePropertyViewModel
    {
        public string Value
        {
            get => this._value;
            set
            {
                this._value = value;
                NotifyPropertyChanged();
            }
        }

        private string _value;

        public BinTreeStringViewModel(BinTreeString treeProperty) : base(treeProperty)
        {
            this.Value = treeProperty.Value;
        }
    }
}

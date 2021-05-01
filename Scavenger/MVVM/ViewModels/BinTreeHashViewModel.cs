using LeagueToolkit.IO.PropertyBin.Properties;
using Scavenger.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeHashViewModel : BinTreePropertyViewModel
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

        public BinTreeHashViewModel(BinTreeHash treeProperty) : base(treeProperty)
        {
            this.Value = Hashtables.GetHash(treeProperty.Value);
        }
    }
}

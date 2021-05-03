using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.IO.PropertyBin;
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

        public BinTreeHashViewModel(BinTreeParentViewModel parent, BinTreeHash treeProperty) : base(parent, treeProperty)
        {
            this.Value = Hashtables.GetHash(treeProperty.Value);
        }

        public override BinTreeProperty BuildProperty()
        {
            return new BinTreeHash(null, this.NameHash, Fnv1a.HashLower(this.Value));
        }
    }
}

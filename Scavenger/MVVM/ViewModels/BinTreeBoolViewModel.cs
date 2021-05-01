using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeBoolViewModel : BinTreePropertyViewModel
    {
        public bool Value
        {
            get => this._value;
            set
            {
                this._value = value;
                NotifyPropertyChanged();
            }
        }

        private bool _value;

        public BinTreeBoolViewModel(BinTreeParentViewModel parent, BinTreeBool treeProperty) : base(parent, treeProperty) 
        {
            this.Value = treeProperty.Value;
        }

        public override BinTreeProperty BuildProperty()
        {
            uint nameHash = Fnv1a.HashLower(this.Name);

            return new BinTreeBool(null, nameHash, this.Value);
        }
    }
}

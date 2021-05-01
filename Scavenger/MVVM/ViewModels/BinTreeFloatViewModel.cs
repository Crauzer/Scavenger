using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeFloatViewModel : BinTreePropertyViewModel
    {
        public float Value
        {
            get => this._value;
            set
            {
                this._value = value;
                NotifyPropertyChanged();
            }
        }
        public float MaxValue => float.MaxValue;
        public float MinValue => float.MinValue;

        private float _value;

        public BinTreeFloatViewModel(BinTreeParentViewModel parent, BinTreeFloat treeProperty) : base(parent, treeProperty)
        {
            this.Value = treeProperty.Value;
        }

        public override BinTreeProperty BuildProperty()
        {
            uint nameHash = Fnv1a.HashLower(this.Name);

            return new BinTreeFloat(null, nameHash, this.Value);
        }
    }
}

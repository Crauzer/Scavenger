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
                SyncTreeProperty();
            }
        }
        public float MaxValue => float.MaxValue;
        public float MinValue => float.MinValue;

        private float _value;

        public BinTreeFloatViewModel() { }
        public BinTreeFloatViewModel(BinTreeParentViewModel parent, BinTreeFloat treeProperty) : base(parent, treeProperty)
        {
            this.Value = treeProperty.Value;
        }

        public override void SyncTreeProperty()
        {
            this.TreeProperty = new BinTreeFloat((IBinTreeParent)this.Parent?.TreeProperty, this.NameHash, this.Value);
        }

        public override BinTreeProperty BuildProperty()
        {
            return new BinTreeFloat(null, this.NameHash, this.Value);
        }
    }
}

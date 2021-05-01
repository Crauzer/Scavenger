using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeVector2ViewModel : BinTreePropertyViewModel
    {
        public float X
        {
            get => this._x;
            set
            {
                this._x = value;
                NotifyPropertyChanged();
            }
        }
        public float Y
        {
            get => this._y;
            set
            {
                this._y = value;
                NotifyPropertyChanged();
            }
        }

        private float _x;
        private float _y;

        public BinTreeVector2ViewModel(BinTreeParentViewModel parent, BinTreeVector2 treeProperty) : base(parent, treeProperty) 
        {
            this.X = treeProperty.Value.X;
            this.Y = treeProperty.Value.Y;
        }

        public override BinTreeProperty BuildProperty()
        {
            return new BinTreeVector2(null, this.NameHash, new Vector2(this._x, this._y));
        }
    }
}

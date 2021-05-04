using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeVector3ViewModel : BinTreePropertyViewModel
    {
        public float X
        {
            get => this._x;
            set
            {
                this._x = value;
                NotifyPropertyChanged();
                SyncTreeProperty();
            }
        }
        public float Y
        {
            get => this._y;
            set
            {
                this._y = value;
                NotifyPropertyChanged();
                SyncTreeProperty();
            }
        }
        public float Z
        {
            get => this._z;
            set
            {
                this._z = value;
                NotifyPropertyChanged();
                SyncTreeProperty();
            }
        }

        private float _x;
        private float _y;
        private float _z;

        public BinTreeVector3ViewModel() { }
        public BinTreeVector3ViewModel(BinTreeParentViewModel parent, BinTreeVector3 treeProperty) : base(parent, treeProperty)
        {
            this.X = treeProperty.Value.X;
            this.Y = treeProperty.Value.Y;
            this.Z = treeProperty.Value.Z;
        }

        public override void SyncTreeProperty()
        {
            this.TreeProperty = new BinTreeVector3((IBinTreeParent)this.Parent?.TreeProperty, this.NameHash, new Vector3(this.X, this.Y, this.Z));
        }

        public override BinTreeProperty BuildProperty()
        {
            return new BinTreeVector3(null, this.NameHash, new Vector3(this._x, this._y, this._z));
        }
    }
}

using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using MediaColor = System.Windows.Media.Color;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeVector4ViewModel : BinTreePropertyViewModel
    {
        public bool IsColorMode
        {
            get => this._isColorMode;
            set
            {
                this._isColorMode = value;
                NotifyPropertyChanged();
            }
        }
        public MediaColor SelectedColor
        {
            get => this._selectedColor;
            set
            {
                this._selectedColor = value;
                this.X = value.R / 255f;
                this.Y = value.G / 255f;
                this.Z = value.B / 255f;
                this.W = value.A / 255f;

                NotifyPropertyChanged();
                SyncTreeProperty();
            }
        }
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
        public float Z
        {
            get => this._z;
            set
            {
                this._z = value;
                NotifyPropertyChanged();
            }
        }
        public float W
        {
            get => this._w;
            set
            {
                this._w = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isColorMode;
        private MediaColor _selectedColor;
        private float _x;
        private float _y;
        private float _z;
        private float _w;

        public BinTreeVector4ViewModel() { }
        public BinTreeVector4ViewModel(BinTreeParentViewModel parent, BinTreeVector4 treeProperty) : base(parent, treeProperty)
        {
            this.SelectedColor = new MediaColor() 
            {
                R = (byte)(treeProperty.Value.X * 255),
                G = (byte)(treeProperty.Value.Y * 255),
                B = (byte)(treeProperty.Value.Z * 255),
                A = (byte)(treeProperty.Value.W * 255),
            };
            
            this.X = treeProperty.Value.X;
            this.Y = treeProperty.Value.Y;
            this.Z = treeProperty.Value.Z;
            this.W = treeProperty.Value.W;
        }

        public override void SyncTreeProperty()
        {
            this.TreeProperty = new BinTreeVector4((IBinTreeParent)this.Parent?.TreeProperty, this.NameHash, new Vector4(this.X, this.Y, this.Z, this.W));
        }

        public override BinTreeProperty BuildProperty()
        {
            return new BinTreeVector4(null, this.NameHash, new Vector4(this._x, this._y, this._z, this._w));
        }
    }
}

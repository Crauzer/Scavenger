using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeMatrix44ViewModel : BinTreePropertyViewModel
    {
        public float M11
        {
            get => this._m11;
            set
            {
                this._m11 = value;
                NotifyPropertyChanged();
            }
        }
        public float M12
        {
            get => this._m12;
            set
            {
                this._m12 = value;
                NotifyPropertyChanged();
            }
        }
        public float M13
        {
            get => this._m13;
            set
            {
                this._m13 = value;
                NotifyPropertyChanged();
            }
        }
        public float M14
        {
            get => this._m14;
            set
            {
                this._m14 = value;
                NotifyPropertyChanged();
            }
        }

        public float M21
        {
            get => this._m21;
            set
            {
                this._m21 = value;
                NotifyPropertyChanged();
            }
        }
        public float M22
        {
            get => this._m22;
            set
            {
                this._m22 = value;
                NotifyPropertyChanged();
            }
        }
        public float M23
        {
            get => this._m23;
            set
            {
                this._m23 = value;
                NotifyPropertyChanged();
            }
        }
        public float M24
        {
            get => this._m24;
            set
            {
                this._m24 = value;
                NotifyPropertyChanged();
            }
        }

        public float M31
        {
            get => this._m31;
            set
            {
                this._m31 = value;
                NotifyPropertyChanged();
            }
        }
        public float M32
        {
            get => this._m32;
            set
            {
                this._m32 = value;
                NotifyPropertyChanged();
            }
        }
        public float M33
        {
            get => this._m33;
            set
            {
                this._m33 = value;
                NotifyPropertyChanged();
            }
        }
        public float M34
        {
            get => this._m34;
            set
            {
                this._m34 = value;
                NotifyPropertyChanged();
            }
        }

        public float M41
        {
            get => this._m41;
            set
            {
                this._m41 = value;
                NotifyPropertyChanged();
            }
        }
        public float M42
        {
            get => this._m42;
            set
            {
                this._m42 = value;
                NotifyPropertyChanged();
            }
        }
        public float M43
        {
            get => this._m43;
            set
            {
                this._m43 = value;
                NotifyPropertyChanged();
            }
        }
        public float M44
        {
            get => this._m44;
            set
            {
                this._m44 = value;
                NotifyPropertyChanged();
            }
        }

        private float _m11;
        private float _m12;
        private float _m13;
        private float _m14;

        private float _m21;
        private float _m22;
        private float _m23;
        private float _m24;

        private float _m31;
        private float _m32;
        private float _m33;
        private float _m34;

        private float _m41;
        private float _m42;
        private float _m43;
        private float _m44;

        public BinTreeMatrix44ViewModel() { }
        public BinTreeMatrix44ViewModel(BinTreeParentViewModel parent, BinTreeMatrix44 treeProperty) : base(parent, treeProperty)
        {
            this.M11 = treeProperty.Value.M11;
            this.M12 = treeProperty.Value.M12;
            this.M13 = treeProperty.Value.M13;
            this.M14 = treeProperty.Value.M14;

            this.M21 = treeProperty.Value.M21;
            this.M22 = treeProperty.Value.M22;
            this.M23 = treeProperty.Value.M23;
            this.M24 = treeProperty.Value.M24;

            this.M31 = treeProperty.Value.M31;
            this.M32 = treeProperty.Value.M32;
            this.M33 = treeProperty.Value.M33;
            this.M34 = treeProperty.Value.M34;

            this.M41 = treeProperty.Value.M41;
            this.M42 = treeProperty.Value.M42;
            this.M43 = treeProperty.Value.M43;
            this.M44 = treeProperty.Value.M44;
        }

        public override BinTreeProperty BuildProperty()
        {
            Matrix4x4 matrix = new Matrix4x4()
            {
                M11 = this._m11,
                M12 = this._m12,
                M13 = this._m13,
                M14 = this._m14,

                M21 = this._m21,
                M22 = this._m22,
                M23 = this._m23,
                M24 = this._m24,

                M31 = this._m31,
                M32 = this._m32,
                M33 = this._m33,
                M34 = this._m34,

                M41 = this._m41,
                M42 = this._m42,
                M43 = this._m43,
                M44 = this._m44,
            };

            return new BinTreeMatrix44(null, this.NameHash, matrix);
        }
    }
}

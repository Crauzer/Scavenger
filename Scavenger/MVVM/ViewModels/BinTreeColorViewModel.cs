using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.Helpers.Structures;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using System;
using System.Collections.Generic;
using System.Text;
using MediaColor = System.Windows.Media.Color;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeColorViewModel : BinTreePropertyViewModel
    {
        public MediaColor SelectedColor
        {
            get => this._selectedColor;
            set
            {
                this._selectedColor = value;
                this.R = value.R / 255f;
                this.G = value.G / 255f;
                this.B = value.B / 255f;
                this.A = value.A / 255f;

                NotifyPropertyChanged();
            }
        }
        public float R
        {
            get => this._r;
            set
            {
                this._r = value;
                NotifyPropertyChanged();
            }
        }
        public float G
        {
            get => this._g;
            set
            {
                this._g = value;
                NotifyPropertyChanged();
            }
        }
        public float B
        {
            get => this._b;
            set
            {
                this._b = value;
                NotifyPropertyChanged();
            }
        }
        public float A
        {
            get => this._a;
            set
            {
                this._a = value;
                NotifyPropertyChanged();
            }
        }

        private MediaColor _selectedColor;
        private float _r;
        private float _g;
        private float _b;
        private float _a;

        public BinTreeColorViewModel(BinTreeParentViewModel parent, BinTreeColor treeProperty) : base(parent, treeProperty)
        {
            this.SelectedColor = new MediaColor()
            {
                R = (byte)(treeProperty.Value.R * 255),
                G = (byte)(treeProperty.Value.G * 255),
                B = (byte)(treeProperty.Value.B * 255),
                A = (byte)(treeProperty.Value.A * 255),
            };

            this.R = treeProperty.Value.R;
            this.G = treeProperty.Value.G;
            this.B = treeProperty.Value.B;
            this.A = treeProperty.Value.A;
        }

        public override BinTreeProperty BuildProperty()
        {
            uint nameHash = Fnv1a.HashLower(this.Name);

            return new BinTreeColor(null, nameHash, new Color(this._r, this._g, this._b, this._a));
        }
    }
}

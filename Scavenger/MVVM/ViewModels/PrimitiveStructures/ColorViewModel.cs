using LeagueToolkit.Helpers.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels.PrimitiveStructures
{
    public class ColorViewModel : PropertyNotifier
    {
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

        private float _r;
        private float _g;
        private float _b;
        private float _a;

        public ColorViewModel(Color color)
        {
            this.R = color.R;
            this.G = color.G;
            this.B = color.B;
            this.A = color.A;
        }
        
        public Color ToColor()
        {
            return new Color(this.R, this.G, this.B, this.A);
        }
    }
}

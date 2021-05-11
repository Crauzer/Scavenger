using LeagueToolkit.Helpers.Structures;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using MediaColor = System.Windows.Media.Color;

namespace Scavenger.MVVM.ViewModels.PrimitiveStructures
{
    public class ColorViewModel : PropertyNotifier
    {
        public MediaColor Color
        {
            get => this._color;
            set
            {
                this._color = value;
                NotifyPropertyChanged();
            }
        }

        private MediaColor _color;

        public ColorViewModel(Color color)
        {
            this.Color = new MediaColor()
            {
                R = (byte)(color.R * 255),
                G = (byte)(color.G * 255),
                B = (byte)(color.B * 255),
                A = (byte)(color.A * 255),
            };
        }
        public ColorViewModel(Vector4 color)
        {
            this.Color = new MediaColor()
            {
                R = (byte)(color.X * 255),
                G = (byte)(color.Y * 255),
                B = (byte)(color.Z * 255),
                A = (byte)(color.W * 255),
            };
        }

        public Color ToColor()
        {
            return new Color(this.R, this.G, this.B, this.A);
        }
        public Vector4 ToVector4()
        {
            return new Vector4(this.R, this.G, this.B, this.A);
        }
    }
}

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
            return new Color(this.Color.R, this.Color.G, this.Color.B, this.Color.A);
        }
        public Vector4 ToVector4()
        {
            return new Vector4()
            {
                X = this.Color.R / 255f,
                Y = this.Color.R / 255f,
                Z = this.Color.R / 255f,
                W = this.Color.R / 255f,
            };
        }
    }
}

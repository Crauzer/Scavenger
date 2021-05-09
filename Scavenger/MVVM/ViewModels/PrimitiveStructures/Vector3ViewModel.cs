using System.Numerics;

namespace Scavenger.MVVM.ViewModels.PrimitiveStructures
{
    public class Vector3ViewModel : PropertyNotifier
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
        public float Z
        {
            get => this._z;
            set
            {
                this._z = value;
                NotifyPropertyChanged();
            }
        }

        private float _x;
        private float _y;
        private float _z;

        public delegate void ComponentChanged();

        public Vector3ViewModel(Vector3 vector)
        {
            this.X = vector.X;
            this.Y = vector.Y;
            this.Z = vector.Z;
        }
    
        public Vector3 ToVector()
        {
            return new Vector3(this.X, this.Y, this.Z);
        }
    }
}

using System.Numerics;

namespace Scavenger.MVVM.ViewModels.PrimitiveStructures
{
    public class Vector2ViewModel : PropertyNotifier
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

        public delegate void ComponentChanged();

        public Vector2ViewModel(Vector2 vector)
        {
            this.X = vector.X;
            this.Y = vector.Y;
        }
    
        public Vector2 ToVector()
        {
            return new Vector2(this.X, this.Y);
        }
    }
}

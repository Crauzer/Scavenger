using LeagueToolkit.Meta.Classes;

namespace Scavenger.MVVM.ViewModels.PrimitiveStructures
{
    public class ValueFloatViewModel : PropertyNotifier
    {
        public float ConstantValue
        {
            get => this._constantValue;
            set
            {
                this._constantValue = value;
                NotifyPropertyChanged();
            }
        }

        private float _constantValue;

        public ValueFloatViewModel(ValueFloat valueFloat)
        {
            this.ConstantValue = valueFloat.ConstantValue;
        }

        public ValueFloat ToValueFloat()
        {
            return new ValueFloat() 
            {
                ConstantValue = this.ConstantValue
            };
        }
    }
}

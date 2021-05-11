using LeagueToolkit.Meta.Classes;
using Scavenger.MVVM.ViewModels.Meta.Structures;

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
        public ValueFloatDynamics Dynamics
        {
            get => this._dynamics;
            set
            {
                this._dynamics = value;
                NotifyPropertyChanged();
            }
        }

        private float _constantValue;
        private ValueFloatDynamics _dynamics;

        public ValueFloatViewModel(ValueFloat valueFloat)
        {
            this.ConstantValue = valueFloat.ConstantValue;
            this.Dynamics = new ValueFloatDynamics(valueFloat.Dynamics);
        }

        public ValueFloat ToValueFloat()
        {
            return new ValueFloat() 
            {
                ConstantValue = this.ConstantValue,
                Dynamics = this.Dynamics.ToVfxAnimatedFloatVariableData()
            };
        }
    }
}

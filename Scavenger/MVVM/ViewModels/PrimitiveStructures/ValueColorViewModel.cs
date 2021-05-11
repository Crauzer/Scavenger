using LeagueToolkit.Meta.Classes;
using Scavenger.MVVM.ViewModels.Meta.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels.PrimitiveStructures
{
    public class ValueColorViewModel : PropertyNotifier
    {
        public ColorViewModel ConstantValue
        {
            get => this._constantValue;
            set
            {
                this._constantValue = value;
                NotifyPropertyChanged();
            }
        }
        public ValueColorDynamics Dynamics
        {
            get => this._dynamics;
            set
            {
                this._dynamics = value;
                NotifyPropertyChanged();
            }
        }

        private ColorViewModel _constantValue;
        private ValueColorDynamics _dynamics;
        
        public ValueColorViewModel(ValueColor valueColor)
        {
            this.ConstantValue = new ColorViewModel(valueColor.ConstantValue);
            this.Dynamics = new ValueColorDynamics(valueColor.Dynamics);
        }

        public ValueColor ToValueVector3()
        {
            return new ValueColor()
            {
                ConstantValue = this.ConstantValue.ToVector4(),
                Dynamics = this.Dynamics.ToVfxAnimatedColorVariableData()
            };
        }
    }
}

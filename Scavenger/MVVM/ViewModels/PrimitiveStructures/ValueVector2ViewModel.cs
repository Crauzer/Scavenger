using LeagueToolkit.Meta.Classes;
using Scavenger.MVVM.ViewModels.Meta.Structures;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Scavenger.MVVM.ViewModels.PrimitiveStructures
{
    public class ValueVector2ViewModel : PropertyNotifier
    {
        public Vector2ViewModel ConstantValue
        {
            get => this._constantValue;
            set
            {
                this._constantValue = value;
                NotifyPropertyChanged();
            }
        }
        public ValueVector2Dynamics Dynamics
        {
            get => this._dynamics;
            set
            {
                this._dynamics = value;
                NotifyPropertyChanged();
            }
        }

        private Vector2ViewModel _constantValue;
        private ValueVector2Dynamics _dynamics;

        public ValueVector2ViewModel()
        {
            this.ConstantValue = new Vector2ViewModel(new Vector2());
            this.Dynamics = new ValueVector2Dynamics();
        }
        public ValueVector2ViewModel(ValueVector2 vector)
        {
            this.ConstantValue = new Vector2ViewModel(vector.ConstantValue);
            this.Dynamics = new ValueVector2Dynamics(vector.Dynamics);
        }

        public ValueVector2 ToValueVector2()
        {
            return new ValueVector2() 
            {
                ConstantValue = this.ConstantValue.ToVector(),
                Dynamics = this.Dynamics.ToVfxAnimatedVector2fVariableData()
            };
        }
    }
}

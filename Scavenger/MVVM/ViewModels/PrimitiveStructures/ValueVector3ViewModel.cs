using LeagueToolkit.Meta.Classes;
using Scavenger.MVVM.ViewModels.Meta.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels.PrimitiveStructures
{
    public class ValueVector3ViewModel : PropertyNotifier
    {
        public Vector3ViewModel ConstantValue
        {
            get => this._constantValue;
            set
            {
                this._constantValue = value;
                NotifyPropertyChanged();
            }
        }
        public ValueVector3Dynamics Dynamics
        {
            get => this._dynamics;
            set
            {
                this._dynamics = value;
                NotifyPropertyChanged();
            }
        }

        private Vector3ViewModel _constantValue;
        private ValueVector3Dynamics _dynamics;

        public ValueVector3ViewModel()
        {
            this.ConstantValue = new Vector3ViewModel();
            this.Dynamics = new ValueVector3Dynamics();
        }
        public ValueVector3ViewModel(ValueVector3 vector)
        {
            this.ConstantValue = new Vector3ViewModel(vector.ConstantValue);
            this.Dynamics = new ValueVector3Dynamics(vector.Dynamics);
        }

        public ValueVector3 ToValueVector3()
        {
            return new ValueVector3() 
            {
                ConstantValue = this.ConstantValue.ToVector(),
                Dynamics = this.Dynamics.ToVfxAnimatedVector3fVariableData()
            };
        }
    }
}

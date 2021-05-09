using LeagueToolkit.Meta.Classes;
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

        private Vector3ViewModel _constantValue;

        public ValueVector3ViewModel(ValueVector3 valueVector3)
        {
            this.ConstantValue = new Vector3ViewModel(valueVector3.ConstantValue);
        }

        public ValueVector3 ToValueVector3()
        {
            return new ValueVector3() 
            {
                ConstantValue = this.ConstantValue.ToVector()
            };
        }
    }
}

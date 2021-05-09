using LeagueToolkit.Meta.Classes;
using System;
using System.Collections.Generic;
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

        private Vector2ViewModel _constantValue;

        public ValueVector2ViewModel(ValueVector2 valueVector2)
        {
            this.ConstantValue = new Vector2ViewModel(valueVector2.ConstantValue);
        }

        public ValueVector2 ToValueVector2()
        {
            return new ValueVector2() 
            {
                ConstantValue = this.ConstantValue.ToVector()
            };
        }
    }
}

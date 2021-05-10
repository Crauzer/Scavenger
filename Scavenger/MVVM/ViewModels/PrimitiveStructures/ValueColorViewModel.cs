using LeagueToolkit.Meta.Classes;
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

        private ColorViewModel _constantValue;

        public ValueColorViewModel(ValueColor valueColor)
        {
            this.ConstantValue = new ColorViewModel(valueColor.ConstantValue);
        }

        public ValueColor ToValueVector3()
        {
            return new ValueColor()
            {
                ConstantValue = this.ConstantValue.ToVector4()
            };
        }
    }
}

using LeagueToolkit.Meta;
using LeagueToolkit.Meta.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels.PrimitiveStructures
{
    public class FlexValueFloatViewModel : PropertyNotifier
    {
        public ValueFloatViewModel Value
        {
            get => this._value;
            set
            {
                this._value = value;
                NotifyPropertyChanged();
            }
        }
        public uint FlexId
        {
            get => this._flexId;
            set
            {
                this._flexId = value;
                NotifyPropertyChanged();
            }
        }

        private ValueFloatViewModel _value;
        private uint _flexId;

        public FlexValueFloatViewModel()
        {
            this.Value = new ValueFloatViewModel();
        }
        public FlexValueFloatViewModel(FlexValueFloat flexValueFloat)
        {
            this.Value = new ValueFloatViewModel(flexValueFloat.Value);
            this.FlexId = flexValueFloat.FlexID;
        }

        public FlexValueFloat ToFlexValueFloat()
        {
            return new FlexValueFloat()
            {
                Value = new MetaEmbedded<ValueFloat>(this.Value.ToValueFloat()),
                FlexID = this.FlexId
            };
        }
    }
}

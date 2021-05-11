using LeagueToolkit.Meta.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels.PrimitiveStructures
{
    public class FlexTypeFloatViewModel : PropertyNotifier
    {
        public float Value
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

        private float _value;
        private uint _flexId;

        public FlexTypeFloatViewModel() { }
        public FlexTypeFloatViewModel(FlexTypeFloat flexTypeFloat)
        {
            this.Value = flexTypeFloat.Value;
            this.FlexId = flexTypeFloat.FlexID;
        }

        public FlexTypeFloat ToFlexTypeFloat()
        {
            return new FlexTypeFloat()
            {
                Value = this.Value,
                FlexID = this.FlexId
            };
        }
    }
}

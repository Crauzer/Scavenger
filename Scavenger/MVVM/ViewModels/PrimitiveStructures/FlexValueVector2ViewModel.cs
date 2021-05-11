using LeagueToolkit.Meta;
using LeagueToolkit.Meta.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels.PrimitiveStructures
{
    public class FlexValueVector2ViewModel : PropertyNotifier
    {
        public ValueVector2ViewModel Value
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

        private ValueVector2ViewModel _value;
        private uint _flexId;

        public FlexValueVector2ViewModel() 
        {
            this.Value = new ValueVector2ViewModel();
        }
        public FlexValueVector2ViewModel(FlexValueVector2 vector)
        {
            this.Value = new ValueVector2ViewModel(vector.Value);
            this.FlexId = vector.FlexID;
        }

        public FlexValueVector2 ToFlexValueVector2()
        {
            return new FlexValueVector2()
            {
                Value = new MetaEmbedded<ValueVector2>(this.Value.ToValueVector2()),
                FlexID = this.FlexId
            };
        }
    }
}

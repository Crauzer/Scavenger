using LeagueToolkit.Meta;
using LeagueToolkit.Meta.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels.PrimitiveStructures
{
    public class FlexValueVector3ViewModel : PropertyNotifier
    {
        public ValueVector3ViewModel Value
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

        private ValueVector3ViewModel _value;
        private uint _flexId;

        public FlexValueVector3ViewModel(FlexValueVector3 vector)
        {
            this.Value = new ValueVector3ViewModel(vector.Value);
            this.FlexId = vector.FlexID;
        }

        public FlexValueVector3 ToFlexValueVector2()
        {
            return new FlexValueVector3()
            {
                Value = new MetaEmbedded<ValueVector3>(this.Value.ToValueVector3()),
                FlexID = this.FlexId
            };
        }
    }
}

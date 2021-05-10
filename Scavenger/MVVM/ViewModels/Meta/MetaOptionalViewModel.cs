using LeagueToolkit.Meta;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels.Meta
{
    public class MetaOptionalViewModel<T> : PropertyNotifier
    {
        public bool IsSome
        {
            get => this._isSome;
            set
            {
                this._isSome = value;
                NotifyPropertyChanged();
            }
        }
        public T Value
        {
            get => this._value;
            set
            {
                this._value = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isSome;
        private T _value;

        public MetaOptionalViewModel()
        {
            this.IsSome = false;
        }
        public MetaOptionalViewModel(T value)
        {
            this.IsSome = value is not null;
            this.Value = value;
        }

        public MetaOptional<T> ToMetaOptional()
        {
            return new MetaOptional<T>(this.Value, this.IsSome);
        }
    }
}

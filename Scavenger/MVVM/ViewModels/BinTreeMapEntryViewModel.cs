using LeagueToolkit.IO.PropertyBin;
using Scavenger.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeMapEntryViewModel : BinTreePropertyViewModel
    {
        public BinTreePropertyViewModel KeyProperty
        {
            get => this._keyProperty;
            set
            {
                this._keyProperty = value;
                NotifyPropertyChanged();
            }
        }
        public BinTreePropertyViewModel ValueProperty
        {
            get => this._valueProperty;
            set
            {
                this._valueProperty = value;
                NotifyPropertyChanged();
            }
        }

        private BinTreePropertyViewModel _keyProperty;
        private BinTreePropertyViewModel _valueProperty;
        private KeyValuePair<BinTreeProperty, BinTreeProperty> _pair;

        public BinTreeMapEntryViewModel(BinTreeParentViewModel parent, KeyValuePair<BinTreeProperty, BinTreeProperty> pair) : base(parent, null)
        {
            this._pair = pair;
            this.KeyProperty = BinTreeUtilities.ConstructTreePropertyViewModel(parent, pair.Key);
            this.ValueProperty = BinTreeUtilities.ConstructTreePropertyViewModel(parent, pair.Value);

            this.KeyProperty.ShowName = false;
            this.ValueProperty.ShowName = false;
        }
    }
}

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

                this._keyProperty.SyncTreeProperty();
            }
        }
        public BinTreePropertyViewModel ValueProperty
        {
            get => this._valueProperty;
            set
            {
                this._valueProperty = value;
                NotifyPropertyChanged();

                this._valueProperty.SyncTreeProperty();
            }
        }

        private BinTreePropertyViewModel _keyProperty;
        private BinTreePropertyViewModel _valueProperty;

        public BinTreeMapEntryViewModel() { }
        public BinTreeMapEntryViewModel(BinTreeParentViewModel parent, KeyValuePair<BinTreeProperty, BinTreeProperty> pair) : base(parent, null)
        {
            this.KeyProperty = BinUtilities.ConstructTreePropertyViewModel(parent, pair.Key);
            this.ValueProperty = BinUtilities.ConstructTreePropertyViewModel(parent, pair.Value);

            this.KeyProperty.ShowName = false;
            this.ValueProperty.ShowName = false;
        }

        public override void SyncTreeProperty()
        {
            this.KeyProperty.Parent = this.Parent;
            this.KeyProperty.SyncTreeProperty();

            this.ValueProperty.Parent = this.Parent;
            this.ValueProperty.SyncTreeProperty();
        }
    }
}

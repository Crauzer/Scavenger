using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using Newtonsoft.Json;
using Scavenger.Utilities;
using System;
using System.Collections.ObjectModel;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeOptionalViewModel : BinTreeParentViewModel
    {
        [JsonIgnore] public string Metadata => $"Optional<{this.ValueType}>";

        public BinPropertyType ValueType
        {
            get => this._valueType;
            set
            {
                this._valueType = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsSome
        {
            get => this._isSome;
            set
            {
                this._isSome = value;
                NotifyPropertyChanged();
                SyncTreeProperty();
            }
        }

        private BinPropertyType _valueType;
        private bool _isSome;

        public BinTreeOptionalViewModel() : base(null, null, new BinTreeOptional(null, 0, BinPropertyType.None, null)) { }
        public BinTreeOptionalViewModel(BinTreeParentViewModel parent, BinTreeOptional treeProperty) : base(parent.BinTree, parent, treeProperty)
        {
            this.ValueType = treeProperty.ValueType;

            BinTreePropertyViewModel valueViewModel = BinTreeUtilities.ConstructTreePropertyViewModel(this, treeProperty.Value);
            if (valueViewModel is not null)
            {
                this.IsSome = true;
                valueViewModel.ShowName = false;

                this.Children.Add(valueViewModel);
            }
        }

        public override void SyncTreeProperty()
        {
            this.TreeProperty = BuildProperty();
        }

        public override BinTreeProperty BuildProperty()
        {
            if (this.Children.Count == 0)
            {
                return new BinTreeOptional(null, this.NameHash, this.ValueType, null);
            }
            else if (this.Children.Count == 1 && this.IsSome)
            {
                return new BinTreeOptional(null, this.NameHash, this.ValueType, this.Children[0].BuildProperty());
            }
            else
            {
                throw new InvalidOperationException("Optional Property must contain one child");
            }
        }
    }
}

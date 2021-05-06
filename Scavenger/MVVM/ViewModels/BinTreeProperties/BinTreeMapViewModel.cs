using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using Newtonsoft.Json;
using Scavenger.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeMapViewModel : BinTreeParentViewModel
    {
        [JsonIgnore] public string Metadata => $"{this.TreeProperty.Type}<{this.KeyType}, {this.ValueType}>";

        public BinPropertyType KeyType
        {
            get => this._keyType;
            set
            {
                this._keyType = value;
                NotifyPropertyChanged();
            }
        }
        public BinPropertyType ValueType
        {
            get => this._valueType;
            set
            {
                this._valueType = value;
                NotifyPropertyChanged();
            }
        }

        private BinPropertyType _keyType;
        private BinPropertyType _valueType;

        public BinTreeMapViewModel()
            : base(null, null, new BinTreeMap(null, 0, BinPropertyType.None, BinPropertyType.None, Enumerable.Empty<KeyValuePair<BinTreeProperty, BinTreeProperty>>())) { }
        public BinTreeMapViewModel(BinTreeParentViewModel parent, BinTreeMap treeProperty) : base(parent.BinTree, parent, treeProperty)
        {
            this.KeyType = treeProperty.KeyType;
            this.ValueType = treeProperty.ValueType;

            foreach (var pair in treeProperty.Map)
            {
                this.Children.Add(new BinTreeMapEntryViewModel(this, pair));
            }

            this.Children.CollectionChanged += OnChildrenCollectionChanged;
        }

        public override void SyncTreeProperty()
        {
            base.SyncTreeProperty();

            List<KeyValuePair<BinTreeProperty, BinTreeProperty>> map = new();
            foreach (BinTreeMapEntryViewModel entryViewModel in this.Children)
            {
                entryViewModel.Parent = this;
                entryViewModel.SyncTreeProperty();

                map.Add(new KeyValuePair<BinTreeProperty, BinTreeProperty>(entryViewModel.KeyProperty.BuildProperty(), entryViewModel.ValueProperty.BuildProperty()));
            }

            this.TreeProperty = new BinTreeMap((IBinTreeParent)this.Parent?.TreeProperty, this.NameHash, this.KeyType, this.ValueType, map);
        }

        public override BinTreeProperty BuildProperty()
        {
            List<KeyValuePair<BinTreeProperty, BinTreeProperty>> map = new();
            foreach (BinTreeMapEntryViewModel entryViewModel in this.Children)
            {
                BinTreeProperty key = entryViewModel.KeyProperty.BuildProperty();
                BinTreeProperty value = entryViewModel.ValueProperty.BuildProperty();

                map.Add(new KeyValuePair<BinTreeProperty, BinTreeProperty>(key, value));
            }

            return new BinTreeMap(null, this.NameHash, this.KeyType, this.ValueType, map);
        }
    }
}

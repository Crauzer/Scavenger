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
        [JsonIgnore] public string Metadata => $"{this.TreeProperty.Type}<{(this.TreeProperty as BinTreeMap).KeyType}, {(this.TreeProperty as BinTreeMap).ValueType}>";

        public BinTreeMapViewModel()
            : base(null, null, new BinTreeMap(null, 0, BinPropertyType.None, BinPropertyType.None, Enumerable.Empty<KeyValuePair<BinTreeProperty, BinTreeProperty>>())) { }
        public BinTreeMapViewModel(BinTreeParentViewModel parent, BinTreeMap treeProperty) : base(parent.BinTree, parent, treeProperty)
        {
            foreach(var pair in treeProperty.Map)
            {
                this.Children.Add(new BinTreeMapEntryViewModel(this, pair));
            }
        }

        public override BinTreeProperty BuildProperty()
        {
            BinTreeMap treeMap = this.TreeProperty as BinTreeMap;
            var map = new List<KeyValuePair<BinTreeProperty, BinTreeProperty>>();
            foreach (BinTreeMapEntryViewModel entryViewModel in this.Children)
            {
                BinTreeProperty key = entryViewModel.KeyProperty.BuildProperty();
                BinTreeProperty value = entryViewModel.ValueProperty.BuildProperty();

                map.Add(new KeyValuePair<BinTreeProperty, BinTreeProperty>(key, value));
            }

            return new BinTreeMap(null, this.NameHash, treeMap.KeyType, treeMap.ValueType, map);
        }
    }
}

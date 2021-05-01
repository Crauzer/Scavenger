using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using Scavenger.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeMapViewModel : BinTreeParentViewModel
    {
        public string Metadata => $" -> {this.TreeProperty.Type}<{(this.TreeProperty as BinTreeMap).KeyType}, {(this.TreeProperty as BinTreeMap).ValueType}>";

        public BinTreeMapViewModel(BinTreeParentViewModel parent, BinTreeMap treeProperty) : base(parent, treeProperty)
        {
            foreach(var pair in treeProperty.Map)
            {
                this.Children.Add(new BinTreeMapEntryViewModel(this, pair));
            }
        }
    }
}

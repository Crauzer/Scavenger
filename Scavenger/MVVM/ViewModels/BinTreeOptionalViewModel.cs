using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using Scavenger.Utilities;
using System;
using System.Collections.ObjectModel;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeOptionalViewModel : BinTreeParentViewModel
    {
        public override string Header => $"{this.Name} -> Optional : {(this.TreeProperty as BinTreeOptional).ValueType}";

        public BinTreeOptionalViewModel(BinTreeParentViewModel parent, BinTreeOptional treeProperty) : base(parent, treeProperty)
        {
            BinTreePropertyViewModel valueViewModel = BinTreeUtilities.ConstructTreePropertyViewModel(this, treeProperty.Value);
            valueViewModel.ShowName = false;

            this.Children.Add(valueViewModel);
        }

        public override BinTreeProperty BuildProperty()
        {
            BinTreeOptional treeOptional = this.TreeProperty as BinTreeOptional;
            uint nameHash = Fnv1a.HashLower(this.Name);

            if (this.Children.Count != 1)
            {
                throw new InvalidOperationException("Optional Property must contain one child");
            }

            return new BinTreeOptional(null, nameHash, treeOptional.ValueType, this.Children[0].BuildProperty());
        }
    }
}

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
        public string Metadata => $"Optional<{(this.TreeProperty as BinTreeOptional).ValueType}>";

        public BinTreeOptionalViewModel(BinTreeParentViewModel parent, BinTreeOptional treeProperty) : base(parent, treeProperty)
        {
            BinTreePropertyViewModel valueViewModel = BinTreeUtilities.ConstructTreePropertyViewModel(this, treeProperty.Value);
            if (valueViewModel is not null)
            {
                valueViewModel.ShowName = false;

                this.Children.Add(valueViewModel);
            }
        }

        public override BinTreeProperty BuildProperty()
        {
            BinTreeOptional treeOptional = this.TreeProperty as BinTreeOptional;

            return this.Children.Count switch
            {
                0 => new BinTreeOptional(null, this.NameHash, treeOptional.ValueType, null),
                1 => new BinTreeOptional(null, this.NameHash, treeOptional.ValueType, this.Children[0].BuildProperty()),
                _ => throw new InvalidOperationException("Optional Property must contain one child")
            };
        }
    }
}

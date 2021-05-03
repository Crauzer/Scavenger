using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using Scavenger.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeUnorderedContainerViewModel : BinTreeParentViewModel
    {
        public string Metadata => $" -> {this.TreeProperty.Type} : {(this.TreeProperty as BinTreeUnorderedContainer).PropertiesType}";

        public BinTreeUnorderedContainerViewModel(BinTreeParentViewModel parent, BinTreeUnorderedContainer treeProperty) : base(parent, treeProperty)
        {
            int itemIndex = 0;
            foreach (BinTreeProperty genericProperty in treeProperty.Properties)
            {
                BinTreePropertyViewModel propertyViewModel = BinTreeUtilities.ConstructTreePropertyViewModel(this, genericProperty);
                propertyViewModel.Name = itemIndex.ToString();
                propertyViewModel.ShowName = false;

                this.Children.Add(propertyViewModel);

                itemIndex++;
            }
        }

        public override BinTreeProperty BuildProperty()
        {
            BinTreeUnorderedContainer container = this.TreeProperty as BinTreeUnorderedContainer;
            List<BinTreeProperty> properties = new List<BinTreeProperty>();
            foreach (BinTreePropertyViewModel propertyViewModel in this.Children)
            {
                properties.Add(propertyViewModel.BuildProperty());
            }

            return new BinTreeUnorderedContainer(null, this.NameHash, container.PropertiesType, properties);
        }
    }
}

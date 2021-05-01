using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using Scavenger.MVVM.Commands;
using Scavenger.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeContainerViewModel : BinTreeParentViewModel
    {
        public string Metadata => $" -> {this.TreeProperty.Type} : {(this.TreeProperty as BinTreeContainer).PropertiesType}";

        public BinTreeContainerViewModel(BinTreeParentViewModel parent, BinTreeContainer treeProperty) : base(parent, treeProperty)
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
            BinTreeContainer container = this.TreeProperty as BinTreeContainer;
            uint nameHash = Fnv1a.HashLower(this.Name);

            List<BinTreeProperty> properties = new List<BinTreeProperty>();
            foreach(BinTreePropertyViewModel propertyViewModel in this.Children)
            {
                properties.Add(propertyViewModel.BuildProperty());
            }

            return new BinTreeContainer(null, nameHash, container.PropertiesType, properties);
        }
    }
}

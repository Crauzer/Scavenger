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
        public override string Header => $"{this.Name} -> {this.TreeProperty.Type} : {(this.TreeProperty as BinTreeContainer).PropertiesType}";

        public ICommand AddChildCommand => new RelayCommand(AddChild);

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

        private void AddChild(object o)
        {

        }
    }
}

using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using Newtonsoft.Json;
using Scavenger.MVVM.Commands;
using Scavenger.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeContainerViewModel : BinTreeParentViewModel
    {
        [JsonIgnore] public string Metadata => $"{this.TreeProperty.Type}<{this.PropertiesType}>";

        public BinPropertyType PropertiesType 
        {
            get => this._propertiesType;
            set
            {
                this._propertiesType = value;
                NotifyPropertyChanged();
            }
        }

        private BinPropertyType _propertiesType;

        public BinTreeContainerViewModel() : base(null, null, new BinTreeContainer(null, 0, BinPropertyType.None, Enumerable.Empty<BinTreeProperty>()))
        {

        }
        public BinTreeContainerViewModel(BinTreeParentViewModel parent, BinTreeContainer treeProperty) : base(parent.BinTree, parent, treeProperty)
        {
            this.PropertiesType = treeProperty.PropertiesType;

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

        public override void SyncTreeProperty()
        {
            base.SyncTreeProperty();

            List<BinTreeProperty> properties = new List<BinTreeProperty>();
            foreach (BinTreePropertyViewModel propertyViewModel in this.Children)
            {
                propertyViewModel.Parent = this;
                propertyViewModel.SyncTreeProperty();

                properties.Add(propertyViewModel.BuildProperty());
            }

            this.TreeProperty = new BinTreeContainer((IBinTreeParent)this.Parent?.TreeProperty, this.NameHash, this.PropertiesType, properties);
        }

        public override BinTreeProperty BuildProperty()
        {
            BinTreeContainer container = this.TreeProperty as BinTreeContainer;
            List<BinTreeProperty> properties = new List<BinTreeProperty>();
            foreach(BinTreePropertyViewModel propertyViewModel in this.Children)
            {
                properties.Add(propertyViewModel.BuildProperty());
            }

            return new BinTreeContainer(null, this.NameHash, container.PropertiesType, properties);
        }
    }
}

using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using Newtonsoft.Json;
using Scavenger.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeUnorderedContainerViewModel : BinTreeParentViewModel
    {
        [JsonIgnore] public string Metadata => $"{this.TreeProperty.Type} : {this.PropertiesType}";

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

        public BinTreeUnorderedContainerViewModel() : base(null, null, new BinTreeUnorderedContainer(null, 0, BinPropertyType.None, Enumerable.Empty<BinTreeProperty>())) { }
        public BinTreeUnorderedContainerViewModel(BinTreeParentViewModel parent, BinTreeUnorderedContainer treeProperty) : base(parent.BinTree, parent, treeProperty)
        {
            this.PropertiesType = treeProperty.PropertiesType;

            foreach (BinTreeProperty genericProperty in treeProperty.Properties)
            {
                BinTreePropertyViewModel propertyViewModel = BinTreeUtilities.ConstructTreePropertyViewModel(this, genericProperty);
                propertyViewModel.ShowName = false;

                this.Children.Add(propertyViewModel);
            }

            this.Children.CollectionChanged += OnChildrenCollectionChanged;
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

            this.TreeProperty = new BinTreeUnorderedContainer((IBinTreeParent)this.Parent?.TreeProperty, this.NameHash, this.PropertiesType, properties);
        }

        public override BinTreeProperty BuildProperty()
        {
            List<BinTreeProperty> properties = new List<BinTreeProperty>();
            foreach (BinTreePropertyViewModel propertyViewModel in this.Children)
            {
                properties.Add(propertyViewModel.BuildProperty());
            }

            return new BinTreeUnorderedContainer(null, this.NameHash, this.PropertiesType, properties);
        }
    }
}

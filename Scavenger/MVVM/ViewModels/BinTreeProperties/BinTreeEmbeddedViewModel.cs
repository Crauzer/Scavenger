using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using Newtonsoft.Json;
using Scavenger.Utilities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeEmbeddedViewModel : BinTreeParentViewModel
    {
        [JsonIgnore] public string Metadata => $"{this.TreeProperty.Type}<{this.MetaClass}>";
        public string MetaClass
        {
            get => this._metaClass;
            set
            {
                this._metaClass = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(this.Metadata));
            }
        }

        private string _metaClass = string.Empty;

        public BinTreeEmbeddedViewModel() : base(null, null, new BinTreeEmbedded(null, 0, 0, Enumerable.Empty<BinTreeProperty>()))
        {

        }
        public BinTreeEmbeddedViewModel(BinTreeParentViewModel parent, BinTreeEmbedded treeProperty) : base(parent.BinTree, parent, treeProperty)
        {
            this.MetaClass = Hashtables.GetType((this.TreeProperty as BinTreeStructure).MetaClassHash);

            foreach (BinTreeProperty genericProperty in treeProperty.Properties)
            {
                this.Children.Add(BinUtilities.ConstructTreePropertyViewModel(this, genericProperty));
            }

            this.Children.CollectionChanged += OnChildrenCollectionChanged;
        }

        public override void SyncTreeProperty()
        {
            base.SyncTreeProperty();

            List<BinTreeProperty> properties = new();
            foreach (BinTreePropertyViewModel propertyViewModel in this.Children)
            {
                propertyViewModel.Parent = this;
                propertyViewModel.SyncTreeProperty();

                properties.Add(propertyViewModel.BuildProperty());
            }

            this.TreeProperty = new BinTreeEmbedded((IBinTreeParent)this.Parent?.TreeProperty, this.NameHash, Fnv1a.HashLower(this.MetaClass), properties);
        }

        public override BinTreeProperty BuildProperty()
        {
            uint metaClassHash = Fnv1a.HashLower(this.MetaClass);

            List<BinTreeProperty> properties = new();
            foreach (BinTreePropertyViewModel propertyViewModel in this.Children)
            {
                properties.Add(propertyViewModel.BuildProperty());
            }

            return new BinTreeEmbedded(null, this.NameHash, metaClassHash, properties);
        }
    }
}

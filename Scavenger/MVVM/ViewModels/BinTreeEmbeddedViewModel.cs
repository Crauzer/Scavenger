using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using Scavenger.Utilities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeEmbeddedViewModel : BinTreeParentViewModel
    {
        public string Metadata => $" -> {this.TreeProperty.Type} : {this.MetaClass}";
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

        private string _metaClass;

        public BinTreeEmbeddedViewModel(BinTreeParentViewModel parent, BinTreeEmbedded treeProperty) : base(parent, treeProperty)
        {
            this.MetaClass = Hashtables.GetType((this.TreeProperty as BinTreeStructure).MetaClassHash);

            foreach (BinTreeProperty genericProperty in treeProperty.Properties)
            {
                this.Children.Add(BinTreeUtilities.ConstructTreePropertyViewModel(this, genericProperty));
            }
        }

        public override BinTreeProperty BuildProperty()
        {
            uint nameHash = Fnv1a.HashLower(this.Name);
            uint metaClassHash = Fnv1a.HashLower(this.MetaClass);

            List<BinTreeProperty> properties = new List<BinTreeProperty>();
            foreach (BinTreePropertyViewModel propertyViewModel in this.Children)
            {
                properties.Add(propertyViewModel.BuildProperty());
            }

            return new BinTreeEmbedded(null, nameHash, metaClassHash, properties);
        }
    }
}

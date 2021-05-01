using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using Scavenger.Utilities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeStructureViewModel : BinTreeParentViewModel
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

        public BinTreeStructureViewModel(BinTreeParentViewModel parent, BinTreeStructure treeProperty) : base(parent, treeProperty)
        {
            BinTreeStructure structure = (this.TreeProperty as BinTreeStructure);

            this.MetaClass = structure.MetaClassHash switch
            {
                0 => "NULL",
                _ => Hashtables.GetType(structure.MetaClassHash)
            };

            foreach (BinTreeProperty genericProperty in treeProperty.Properties ?? Enumerable.Empty<BinTreeProperty>())
            {
                this.Children.Add(BinTreeUtilities.ConstructTreePropertyViewModel(this, genericProperty));
            }
        }

        public override BinTreeProperty BuildProperty()
        {
            uint metaClassHash = Fnv1a.HashLower(this.MetaClass);

            List<BinTreeProperty> properties = new List<BinTreeProperty>();
            foreach (BinTreePropertyViewModel propertyViewModel in this.Children)
            {
                properties.Add(propertyViewModel.BuildProperty());
            }

            return new BinTreeStructure(null, this.NameHash, metaClassHash, properties);
        }
    }
}

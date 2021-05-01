using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using Scavenger.Utilities;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeObjectLinkViewModel : BinTreePropertyViewModel
    {
        public string Value
        {
            get => this._value;
            set
            {
                this._value = value;
                NotifyPropertyChanged();
            }
        }

        private string _value;

        public BinTreeObjectLinkViewModel(BinTreeParentViewModel parent, BinTreeObjectLink treeProperty) : base(parent, treeProperty)
        {
            this.Value = Hashtables.GetObject(treeProperty.Value);
        }

        public override BinTreeProperty BuildProperty()
        {
            uint nameHash = Fnv1a.HashLower(this.Name);

            return new BinTreeObjectLink(null, nameHash, Fnv1a.HashLower(this.Value));
        }
    }
}

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

        public BinTreeObjectLinkViewModel() { }
        public BinTreeObjectLinkViewModel(BinTreeParentViewModel parent, BinTreeObjectLink treeProperty) : base(parent, treeProperty)
        {
            this.Value = Hashtables.GetObject(treeProperty.Value);
        }

        public override void SyncTreeProperty()
        {
            this.TreeProperty = new BinTreeObjectLink((IBinTreeParent)this.Parent?.TreeProperty, this.NameHash, Fnv1a.HashLower(this.Value));
        }

        public override BinTreeProperty BuildProperty()
        {
            return new BinTreeObjectLink(null, this.NameHash, Fnv1a.HashLower(this.Value));
        }
    }
}

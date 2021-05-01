using LeagueToolkit.Helpers.Cryptography;
using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using Scavenger.Utilities;
using System.Text;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeWadEntryLinkViewModel : BinTreePropertyViewModel
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

        public BinTreeWadEntryLinkViewModel(BinTreeParentViewModel parent, BinTreeWadEntryLink treeProperty) : base(parent, treeProperty)
        {
            this.Value = Hashtables.GetWadEntry(treeProperty.Value);
        }

        public override BinTreeProperty BuildProperty()
        {
            uint nameHash = Fnv1a.HashLower(this.Name);

            return new BinTreeWadEntryLink(null, nameHash, XXHash.XXH64(Encoding.UTF8.GetBytes(this.Value.ToLower())));
        }
    }
}

using LeagueToolkit.IO.PropertyBin.Properties;
using Scavenger.Utilities;

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
    }
}

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

        public BinTreeObjectLinkViewModel(BinTreeObjectLink treeProperty) : base(treeProperty)
        {
            this.Value = Hashtables.GetObject(treeProperty.Value);
        }
    }
}

using LeagueToolkit.IO.PropertyBin;

namespace Scavenger.MVVM.ViewModels
{
    public class NewBinPropertyViewModel : PropertyNotifier
    {
        public string Name
        {
            get => this._name;
            set
            {
                this._name = value;
                NotifyPropertyChanged();
            }
        }
        public BinPropertyType PropertyType
        {
            get => this._propertyType;
            set
            {
                this._propertyType = value;
                NotifyPropertyChanged();
            }
        }
        public BinPropertyType PrimaryType
        {
            get => this._primaryType;
            set
            {
                this._primaryType = value;
                NotifyPropertyChanged();
            }
        }
        public BinPropertyType SecondaryType
        {
            get => this._secondaryType;
            set
            {
                this._secondaryType = value;
                NotifyPropertyChanged();
            }
        }

        private string _name;
        private BinPropertyType _propertyType;
        private BinPropertyType _primaryType;
        private BinPropertyType _secondaryType;


    }
}

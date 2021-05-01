using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.Helpers.Structures;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using Scavenger.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

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
        public string MetaClass
        {
            get => this._metaClass;
            set
            {
                this._metaClass = value;
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

        public IEnumerable<BinPropertyType> PropertyTypes
        {
            get => this._propertyTypes;
            set
            {
                this._propertyTypes = value;
                NotifyPropertyChanged();
            }
        }
        public IEnumerable<BinPropertyType> Types
        {
            get => this._types;
            set
            {
                this._types = value;
                NotifyPropertyChanged();
            }
        }

        private string _name = string.Empty;
        private string _metaClass = string.Empty;
        private IEnumerable<BinPropertyType> _propertyTypes;
        private IEnumerable<BinPropertyType> _types;
        private BinPropertyType _propertyType;
        private BinPropertyType _primaryType;
        private BinPropertyType _secondaryType;

        public NewBinPropertyViewModel(IEnumerable<BinPropertyType> restirctToTypes)
        {
            if (restirctToTypes is null is false && restirctToTypes.Any() is false) this.PropertyTypes = Enum.GetValues(typeof(BinPropertyType)).Cast<BinPropertyType>();
            else this.PropertyTypes = restirctToTypes;
        }

        public BinTreeProperty BuildProperty(IBinTreeParent parent)
        {
            return BinTreeUtilities.BuildProperty(this.Name, this.MetaClass, parent, this.PropertyType, this.PrimaryType, this.SecondaryType);
        }
    }
}

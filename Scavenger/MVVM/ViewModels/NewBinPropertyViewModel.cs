using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.Helpers.Structures;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using Scavenger.IO.Templates;
using Scavenger.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Scavenger.MVVM.ViewModels
{
    public class NewBinPropertyViewModel : PropertyNotifier
    {
        public bool IsStructureTemplateSelectable
        {
            get => this._IsStructureTemplateSelectable;
            set
            {
                this._IsStructureTemplateSelectable = value;
                NotifyPropertyChanged();
            }
        }
        public StructureTemplate StructureTemplate
        {
            get => this._structureTemplate;
            set
            {
                this._structureTemplate = value;

                if (value is not null)
                {
                    this.MetaClass = value.MetaClass;
                    this.PropertyTypes = new List<BinPropertyType>()
                    {
                        BinPropertyType.Structure, BinPropertyType.Embedded
                    }.Intersect(this.PropertyTypes);

                    BinPropertyType requestedType = value.IsEmbedded ? BinPropertyType.Embedded : BinPropertyType.Structure;
                    if (this.PropertyTypes.Contains(requestedType))
                    {
                        this.PropertyType = requestedType;
                    }
                    else this.PropertyType = this.PropertyTypes.First();
                }
                else
                {
                    this.PropertyTypes = GeneratePropertyTypes();
                }

                NotifyPropertyChanged();
            }
        }

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
        public bool IsMetaClassRequired
        {
            get => this._isMetaClassRequired;
            set
            {
                this._isMetaClassRequired = value;
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

                this.IsMetaClassRequired = value switch
                {
                    BinPropertyType.Structure => true,
                    BinPropertyType.Embedded => true,
                    _ => false,
                };
                this.IsPrimaryTypeRequired = value switch
                {
                    BinPropertyType.Container => true,
                    BinPropertyType.UnorderedContainer => true,
                    BinPropertyType.Optional => true,
                    BinPropertyType.Map => true,
                    _ => false,
                };
                this.IsSecondaryTypeRequired = value switch
                {
                    BinPropertyType.Map => true,
                    _ => false,
                };
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

        public bool IsPrimaryTypeRequired
        {
            get => this._isPrimaryTypeRequired;
            set
            {
                this._isPrimaryTypeRequired = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsSecondaryTypeRequired
        {
            get => this._isSecondaryTypeRequired;
            set
            {
                this._isSecondaryTypeRequired = value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<StructureTemplate> StructureTemplates { get; }
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

        private bool _IsStructureTemplateSelectable = true;
        private StructureTemplate _structureTemplate;
        
        private string _name = string.Empty;
        private string _metaClass = string.Empty;
        private bool _isMetaClassRequired;

        private IEnumerable<BinPropertyType> _propertyTypes;
        private IEnumerable<BinPropertyType> _types;
        
        private BinPropertyType _propertyType;
        private BinPropertyType _primaryType;
        private BinPropertyType _secondaryType;

        private bool _isPrimaryTypeRequired;
        private bool _isSecondaryTypeRequired;

        public NewBinPropertyViewModel(ICollection<StructureTemplate> structureTemplates, ICollection<BinPropertyType> restirctToTypes = null)
        {
            this.StructureTemplates = structureTemplates;
            
            if (restirctToTypes is null is false && restirctToTypes.Any()) this.PropertyTypes = restirctToTypes;
            else this.PropertyTypes = GeneratePropertyTypes();

            this.Types = GeneratePropertyTypes();
        }

        public BinTreeProperty BuildProperty(IBinTreeParent parent)
        {
            if (this.StructureTemplate is not null)
            {
                BinTreeStructure structureProperty = BinUtilities.BuildProperty(this.Name, this.MetaClass, parent, this.PropertyType, this.PrimaryType, this.SecondaryType) as BinTreeStructure;

                foreach(PropertyTemplate propertyTemplate in this.StructureTemplate.Properties)
                {
                    BinTreeProperty templateProperty = BinUtilities.BuildProperty(propertyTemplate.Name, propertyTemplate.MetaClass, structureProperty,
                        propertyTemplate.Type, propertyTemplate.PrimaryType, propertyTemplate.SecondaryType);

                    structureProperty.AddProperty(templateProperty);
                }

                return structureProperty;
            }
            else
            {
                return BinUtilities.BuildProperty(this.Name, this.MetaClass, parent, this.PropertyType, this.PrimaryType, this.SecondaryType);
            }
        }

        private IEnumerable<BinPropertyType> GeneratePropertyTypes()
        {
            return Enum.GetValues(typeof(BinPropertyType)).Cast<BinPropertyType>();
        }
    }
}

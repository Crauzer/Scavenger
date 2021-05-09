using LeagueToolkit.IO.PropertyBin;
using Scavenger.IO.Templates;
using Scavenger.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels
{
    public class NewBinTreeObjectViewModel : PropertyNotifier
    {
        public StructureTemplate StructureTemplate
        {
            get => this._structureTemplate;
            set
            {
                this._structureTemplate = value;

                if (value is not null)
                {
                    this.MetaClass = value.MetaClass;

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

        public IEnumerable<StructureTemplate> StructureTemplates { get; }

        private StructureTemplate _structureTemplate;
        private string _name = string.Empty;
        private string _metaClass = string.Empty;

        public NewBinTreeObjectViewModel(ICollection<StructureTemplate> structureTemplates)
        {
            this.StructureTemplates = structureTemplates;
        }

        public BinTreeObject BuildObject()
        {
            if (this.StructureTemplate is not null)
            {
                List<BinTreeProperty> properties = new();
                foreach (PropertyTemplate propertyTemplate in this.StructureTemplate.Properties)
                {
                    BinTreeProperty templateProperty = BinUtilities.BuildProperty(propertyTemplate.Name, propertyTemplate.MetaClass, null,
                        propertyTemplate.Type, propertyTemplate.PrimaryType, propertyTemplate.SecondaryType);

                    properties.Add(templateProperty);
                }

                return new BinTreeObject(this.MetaClass, this.Name, properties);
            }
            else
            {
                return new BinTreeObject(this.MetaClass, this.Name, new List<BinTreeProperty>());
            }
        }
    }
}

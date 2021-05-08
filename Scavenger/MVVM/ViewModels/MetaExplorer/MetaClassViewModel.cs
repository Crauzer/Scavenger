using LeagueToolkit.Meta;
using LeagueToolkit.Meta.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Scavenger.MVVM.ViewModels.MetaExplorer
{
    public class MetaClassViewModel : PropertyNotifier
    {
        public string Header { get; }

        public List<MetaPropertyViewModel> Properties { get; } = new();

        public MetaClassViewModel(TypeInfo metaClassTypeInfo)
        {
            MetaClassAttribute attribute = metaClassTypeInfo.GetCustomAttribute<MetaClassAttribute>();

            string name = GetNameFromMetaAttribute(attribute);

            if (metaClassTypeInfo.BaseType is TypeInfo baseType
                && baseType.GetInterface(nameof(IMetaClass)) is not null)
            {
                MetaClassAttribute baseTypeMetaAttribute = baseType.GetCustomAttribute<MetaClassAttribute>();
                string baseTypeName = GetNameFromMetaAttribute(baseTypeMetaAttribute);

                this.Header = $"{name} <extends> {baseTypeName}";
            }
            else
            {
                this.Header = name;
            }

            foreach(PropertyInfo property in metaClassTypeInfo.GetProperties())
            {
                this.Properties.Add(new MetaPropertyViewModel(property));
            }
        }

        private string GetNameFromMetaAttribute(MetaClassAttribute attribute)
        {
            return attribute.Name switch
            {
                "" => $"0x{attribute.NameHash:X8}",
                null => $"0x{attribute.NameHash:X8}",
                _ => attribute.Name
            };
        }
    }
}

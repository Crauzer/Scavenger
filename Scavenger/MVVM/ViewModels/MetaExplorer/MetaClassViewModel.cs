using LeagueToolkit.Meta.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Scavenger.MVVM.ViewModels.MetaExplorer
{
    public class MetaClassViewModel : PropertyNotifier
    {
        public string Name { get; }

        public List<MetaPropertyViewModel> Properties { get; } = new();

        public MetaClassViewModel(TypeInfo metaClassTypeInfo)
        {
            MetaClassAttribute attribute = metaClassTypeInfo.GetCustomAttribute<MetaClassAttribute>();

            if (string.IsNullOrEmpty(attribute.Name))
            {
                this.Name = $"0x{attribute.NameHash:X8}";
            }
            else this.Name = attribute.Name;

            foreach(PropertyInfo property in metaClassTypeInfo.GetProperties())
            {
                this.Properties.Add(new MetaPropertyViewModel(property));
            }
        }
    }
}

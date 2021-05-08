using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.Meta.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Scavenger.MVVM.ViewModels.MetaExplorer
{
    public class MetaPropertyViewModel : PropertyNotifier
    {
        public string Name { get; }

        public string Type { get; }

        public MetaPropertyViewModel(PropertyInfo metaPropertyInfo)
        {
            MetaPropertyAttribute attribute = metaPropertyInfo.GetCustomAttribute<MetaPropertyAttribute>();

            if (string.IsNullOrEmpty(attribute.Name))
            {
                this.Name = $"0x{attribute.NameHash:X8}";
            }
            else this.Name = attribute.Name;

            this.Type = GetPropertyTypeName(attribute);
        }

        private string GetPropertyTypeName(MetaPropertyAttribute property)
        {
            return property.ValueType switch
            {
                BinPropertyType.Container => GetContainerTypeName(property),
                BinPropertyType.UnorderedContainer => GetContainerTypeName(property),
                BinPropertyType.Structure => $"{property.ValueType} : {property.OtherClass}",
                BinPropertyType.Embedded => $"{property.ValueType} : {property.OtherClass}",
                BinPropertyType.Optional => GetOptionalTypeName(property),
                BinPropertyType.Map => GetMapTypeName(property),
                _ => $"{property.ValueType}"
            };
        }
    
        private string GetContainerTypeName(MetaPropertyAttribute property)
        {
            bool doesItemTypeNeedOtherClass = DoesTypeNeedOtherClass(property.PrimaryType);

            return $"{property.ValueType}<{property.PrimaryType}{(doesItemTypeNeedOtherClass ? $" : {property.OtherClass}" : "")}>";
        }
        private string GetOptionalTypeName(MetaPropertyAttribute property)
        {
            bool doesItemTypeNeedOtherClass = DoesTypeNeedOtherClass(property.PrimaryType);

            return $"{property.ValueType}<{property.PrimaryType}{(doesItemTypeNeedOtherClass ? $" : {property.OtherClass}" : "")}>";
        }
        private string GetMapTypeName(MetaPropertyAttribute property)
        {
            bool doesValueItemTypeNeedOtherClass = DoesTypeNeedOtherClass(property.SecondaryType);

            return $"{property.ValueType}<{property.PrimaryType}, {property.SecondaryType}{(doesValueItemTypeNeedOtherClass ? $" : {property.OtherClass}" : "")}>";
        }

        private bool DoesTypeNeedOtherClass(BinPropertyType type)
        {
            return type switch
            {
                BinPropertyType.Structure => true,
                BinPropertyType.Embedded => true,
                _ => false,
            };
        }
    }
}

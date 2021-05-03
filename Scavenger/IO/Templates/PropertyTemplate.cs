using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Scavenger.MVVM.ViewModels;

namespace Scavenger.IO.Templates
{
    public class PropertyTemplate
    {
        public string Name { get; set; }
        public string MetaClass { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public BinPropertyType Type { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public BinPropertyType PrimaryType { get; set; } = BinPropertyType.None;

        [JsonConverter(typeof(StringEnumConverter))]
        public BinPropertyType SecondaryType { get; set; } = BinPropertyType.None;

        public PropertyTemplate() { }
        public PropertyTemplate(BinTreePropertyViewModel viewModel)
        {
            this.Name = viewModel.Name;
            this.Type = viewModel.TreeProperty.Type;

            if(viewModel is BinTreeContainerViewModel containerViewModel)
            {
                this.PrimaryType = (containerViewModel.TreeProperty as BinTreeContainer).PropertiesType;
            }
            else if(viewModel is BinTreeUnorderedContainerViewModel unorderedContainerViewModel)
            {
                this.PrimaryType = (unorderedContainerViewModel.TreeProperty as BinTreeContainer).PropertiesType;
            }
            else if(viewModel is BinTreeMapViewModel mapViewModel)
            {
                BinTreeMap map = mapViewModel.TreeProperty as BinTreeMap;

                this.PrimaryType = map.KeyType;
                this.SecondaryType = map.ValueType;
            }
            else if(viewModel is BinTreeOptionalViewModel optionalViewModel)
            {
                BinTreeOptional optional = optionalViewModel.TreeProperty as BinTreeOptional;

                this.PrimaryType = optional.ValueType;
            }
            else if(viewModel is BinTreeStructureViewModel structureViewModel)
            {
                this.MetaClass = structureViewModel.MetaClass;
            }
            else if(viewModel is BinTreeEmbeddedViewModel embeddedViewModel)
            {
                this.MetaClass = embeddedViewModel.MetaClass;
            }
        }
    }
}

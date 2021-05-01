using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using Scavenger.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Scavenger.MVVM.TemplateSelectors
{
    public class BinPropertyTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if ( container is FrameworkElement element && item is BinTreePropertyViewModel propertyViewModel)
            {
                return propertyViewModel switch
                {
                    BinTreeBoolViewModel _ => element.FindResource("BinTreeBoolTemplate") as DataTemplate,
                    BinTreeSByteViewModel _ => element.FindResource("BinTreeNumberTemplate") as DataTemplate,
                    BinTreeByteViewModel _ => element.FindResource("BinTreeNumberTemplate") as DataTemplate,
                    BinTreeInt16ViewModel _ => element.FindResource("BinTreeNumberTemplate") as DataTemplate,
                    BinTreeUInt16ViewModel _ => element.FindResource("BinTreeNumberTemplate") as DataTemplate,
                    BinTreeInt32ViewModel _ => element.FindResource("BinTreeNumberTemplate") as DataTemplate,
                    BinTreeUInt32ViewModel _ => element.FindResource("BinTreeNumberTemplate") as DataTemplate,
                    BinTreeInt64ViewModel _ => element.FindResource("BinTreeNumberTemplate") as DataTemplate,
                    BinTreeUInt64ViewModel _ => element.FindResource("BinTreeNumberTemplate") as DataTemplate,
                    BinTreeFloatViewModel _ => element.FindResource("BinTreeNumberTemplate") as DataTemplate,
                    BinTreeVector2ViewModel _ => element.FindResource("BinTreeVector2Template") as DataTemplate,
                    BinTreeVector3ViewModel _ => element.FindResource("BinTreeVector3Template") as DataTemplate,
                    BinTreeVector4ViewModel _ => element.FindResource("BinTreeVector4Template") as DataTemplate,
                    BinTreeMatrix44ViewModel _ => element.FindResource("BinTreeMatrix44Template") as DataTemplate,
                    BinTreeColorViewModel _ => element.FindResource("BinTreeColorTemplate") as DataTemplate,
                    BinTreeWadEntryLinkViewModel _ => element.FindResource("BinTreeStringTemplate") as DataTemplate,
                    BinTreeHashViewModel _ => element.FindResource("BinTreeStringTemplate") as DataTemplate,
                    BinTreeUnorderedContainerViewModel _ => element.FindResource("BinTreeContainerTemplate") as DataTemplate,
                    BinTreeContainerViewModel _ => element.FindResource("BinTreeContainerTemplate") as DataTemplate,
                    BinTreeStringViewModel _ => element.FindResource("BinTreeStringTemplate") as DataTemplate,
                    BinTreeEmbeddedViewModel _ => element.FindResource("BinTreeStructureTemplate") as DataTemplate,
                    BinTreeStructureViewModel _ => element.FindResource("BinTreeStructureTemplate") as DataTemplate,
                    BinTreeObjectLinkViewModel _ => element.FindResource("BinTreeStringTemplate") as DataTemplate,
                    BinTreeMapEntryViewModel _ => element.FindResource("BinTreeMapEntryTemplate") as DataTemplate,
                    BinTreeMapViewModel _ => element.FindResource("BinTreeMapTemplate") as DataTemplate,
                    BinTreeOptionalViewModel _ => element.FindResource("BinTreeOptionalTemplate") as DataTemplate,
                    BinTreeBitBoolViewModel _ => element.FindResource("BinTreeNumberTemplate") as DataTemplate,
                    _ => element.FindResource("BinTreePropertyTemplate") as DataTemplate
                };
            }

            return null;
        }
    }
}

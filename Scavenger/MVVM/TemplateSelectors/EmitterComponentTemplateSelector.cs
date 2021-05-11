using Scavenger.MVVM.ViewModels;
using Scavenger.MVVM.ViewModels.ObjectEditors.VfxSystemDefinitionDataEditor.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Scavenger.MVVM.TemplateSelectors
{
    public class EmitterComponentTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (container is FrameworkElement element && item is EmitterComponent emitterComponent)
            {
                return emitterComponent switch
                {
                    BirthEmitterComponent _ => element.FindResource("BirthEmitterComponentTemplate") as DataTemplate,
                    //PositionEmitterComponent _ => element.FindResource("PositionEmitterComponentTemplate") as DataTemplate,
                    //RotationEmitterComponent _ => element.FindResource("RotationEmitterComponentTemplate") as DataTemplate,
                    ScaleEmitterComponent _ => element.FindResource("ScaleEmitterComponentTemplate") as DataTemplate,
                    //RenderEmitterComponent _ => element.FindResource("RenderEmitterComponentTemplate") as DataTemplate,
                    //TextureEmitterComponent _ => element.FindResource("TextureEmitterComponentTemplate") as DataTemplate,
                    _ => null
                };
            }

            return null;
        }
    }
}

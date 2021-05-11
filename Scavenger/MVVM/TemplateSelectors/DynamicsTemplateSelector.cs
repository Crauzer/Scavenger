using Scavenger.MVVM.ViewModels.Meta.Structures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Scavenger.MVVM.TemplateSelectors
{
    public class DynamicsTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (container is FrameworkElement element)
            {
                return item switch
                {
                    ValueFloatDynamics _ => element.FindResource("ValueFloatDynamicsTemplate") as DataTemplate,
                    ValueVector2Dynamics _ => element.FindResource("ValueVector2DynamicsTemplate") as DataTemplate,
                    ValueVector3Dynamics _ => element.FindResource("ValueVector3DynamicsTemplate") as DataTemplate,
                    ValueColorDynamics _ => element.FindResource("ValueColorDynamicsTemplate") as DataTemplate
                };
            }

            return null;
        }
    }
}

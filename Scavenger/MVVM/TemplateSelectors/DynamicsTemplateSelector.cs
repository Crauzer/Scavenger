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
                    ValueVector3Dynamics dynamics => element.FindResource("ValueVector3DynamicsTemplate") as DataTemplate
                };
            }

            return null;
        }
    }
}

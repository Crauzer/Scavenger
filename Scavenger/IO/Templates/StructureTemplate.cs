using LeagueToolkit.IO.PropertyBin.Properties;
using Scavenger.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.IO.Templates
{
    public class StructureTemplate
    {
        public string TemplateName { get; set; }
        public string MetaClass { get; set; }
        public bool IsEmbedded { get; set; }
        public List<PropertyTemplate> Properties { get; set; } = new ();

        public StructureTemplate() { }
        public StructureTemplate(string templateName, BinTreeStructureViewModel viewModel)
        {
            this.TemplateName = templateName;
            this.MetaClass = viewModel.MetaClass;

            foreach(BinTreePropertyViewModel propertyViewModel in viewModel.Children)
            {
                this.Properties.Add(new PropertyTemplate(propertyViewModel));
            }
        }
        public StructureTemplate(string templateName, BinTreeEmbeddedViewModel viewModel)
        {
            this.TemplateName = templateName;
            this.MetaClass = viewModel.MetaClass;
            this.IsEmbedded = true;

            foreach (BinTreePropertyViewModel propertyViewModel in viewModel.Children)
            {
                this.Properties.Add(new PropertyTemplate(propertyViewModel));
            }
        }
    }
}

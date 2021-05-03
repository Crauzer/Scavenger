using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels
{
    public class ExportStructureTemplateViewModel : PropertyNotifier
    {
        public string TemplateName
        {
            get => this._templateName;
            set
            {
                this._templateName = value;
                NotifyPropertyChanged();
            }
        }

        private string _templateName;
    }
}

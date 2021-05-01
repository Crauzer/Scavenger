using LeagueToolkit.IO.PropertyBin;
using Scavenger.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreePropertyViewModel : PropertyNotifier
    {
        public virtual string Header { get; set; }
        public string Name
        {
            get => this._name;
            set
            {
                this._name = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(this.Header));
            }
        }
        public bool ShowName
        {
            get => this._showName;
            set
            {
                this._showName = value;
                NotifyPropertyChanged();
            }
        }

        private bool _showName;
        private string _name;

        public BinTreeProperty TreeProperty { get; private set; }

        public BinTreePropertyViewModel(BinTreeProperty treeProperty, bool showName = true)
        {
            this.Name = Hashtables.GetField(treeProperty.NameHash);
            this.ShowName = showName;
            this.TreeProperty = treeProperty;
        }
    }
}

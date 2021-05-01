using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using Scavenger.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeObjectViewModel : PropertyNotifier
    {
        public string Name
        {
            get => this._name;
            set
            {
                this._name = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<BinTreePropertyViewModel> Properties
        {
            get => this._properties;
            set
            {
                this._properties = value;
                NotifyPropertyChanged();
            }
        }

        private string _name;

        private BinTreeObject _treeObject;
        private ObservableCollection<BinTreePropertyViewModel> _properties = new ObservableCollection<BinTreePropertyViewModel>();

        public BinTreeObjectViewModel(BinTreeObject treeObject)
        {
            this._treeObject = treeObject;
            this.Name = Hashtables.GetObject(treeObject.PathHash);

            foreach(BinTreeProperty genericProperty in treeObject.Properties)
            {
                this.Properties.Add(BinTreeUtilities.ConstructTreePropertyViewModel(genericProperty));
            }
        }
    }
}

using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using Scavenger.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeObjectViewModel : BinTreeParentViewModel
    {
        public string MetaClass
        {
            get => this._metaClass;
            set
            {
                this._metaClass = value;
                NotifyPropertyChanged();
            }
        }

        private string _metaClass;

        public BinTreeObject TreeObject { get; private set; }

        public BinTreeObjectViewModel(BinTreeObject treeObject) : base(null, null)
        {
            this.TreeObject = treeObject;
            this.Name = Hashtables.GetObject(treeObject.PathHash);
            this.MetaClass = Hashtables.GetType(treeObject.MetaClassHash);

            foreach(BinTreeProperty genericProperty in treeObject.Properties)
            {
                this.Children.Add(BinTreeUtilities.ConstructTreePropertyViewModel(this, genericProperty));
            }
        }

        public BinTreeObject BuildObject()
        {
            List<BinTreeProperty> properties = new List<BinTreeProperty>();
            foreach(BinTreePropertyViewModel propertyViewModel in this.Children)
            {
                properties.Add(propertyViewModel.BuildProperty());
            }

            return new BinTreeObject(this.MetaClass, this.Name, properties);
        }
    }
}

using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using Newtonsoft.Json;
using Scavenger.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Data;

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

        [JsonIgnore] public BinTreeObject TreeObject { get; private set; }

        public BinTreeObjectViewModel() : base(null, null, null) { }
        public BinTreeObjectViewModel(BinTreeViewModel tree, BinTreeObject treeObject) : base(tree, null, null)
        {
            this.BinTree = tree;
            this.TreeObject = treeObject;
            this._name = Hashtables.GetObject(treeObject.PathHash);
            this._metaClass = Hashtables.GetType(treeObject.MetaClassHash);

            foreach(BinTreeProperty genericProperty in treeObject.Properties)
            {
                this.Children.Add(BinTreeUtilities.ConstructTreePropertyViewModel(this, genericProperty));
            }

            this.Children.CollectionChanged += OnChildrenCollectionChanged;
        }

        public void SyncTreeObject()
        {
            this.TreeObject = BuildObject();
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

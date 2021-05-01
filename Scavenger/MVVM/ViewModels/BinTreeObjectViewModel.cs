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
        private BinTreeObject _treeObject;

        public BinTreeObjectViewModel(BinTreeObject treeObject) : base(null, null)
        {
            this._treeObject = treeObject;
            this.Name = Hashtables.GetObject(treeObject.PathHash);
            this.MetaClass = Hashtables.GetType(treeObject.MetaClassHash);

            foreach(BinTreeProperty genericProperty in treeObject.Properties)
            {
                this.Children.Add(BinTreeUtilities.ConstructTreePropertyViewModel(this, genericProperty));
            }
        }
    }
}

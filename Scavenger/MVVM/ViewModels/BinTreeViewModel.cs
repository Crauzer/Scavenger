using LeagueToolkit.IO.PropertyBin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeViewModel : PropertyNotifier
    {
        public ObservableCollection<BinTreeObjectViewModel> Objects
        {
            get => this._objects;
            set
            {
                this._objects = value;
                NotifyPropertyChanged();
            }
        }

        private BinTree _tree;
        private ObservableCollection<BinTreeObjectViewModel> _objects = new ObservableCollection<BinTreeObjectViewModel>();

        public BinTreeViewModel(BinTree binTree)
        {
            this._tree = binTree;

            GenerateObjects();
        }

        private void GenerateObjects()
        {
            foreach(BinTreeObject treeObject in this._tree.Objects)
            {
                this.Objects.Add(new BinTreeObjectViewModel(treeObject));
            }
        }
    }
}

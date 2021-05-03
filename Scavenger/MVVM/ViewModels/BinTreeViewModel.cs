using LeagueToolkit.IO.PropertyBin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeViewModel : PropertyNotifier
    {
        public string BinName
        {
            get => this._binName;
            set
            {
                this._binName = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<BinTreeObjectViewModel> Objects
        {
            get => this._objects;
            set
            {
                this._objects = value;
                NotifyPropertyChanged();
            }
        }
        public string Filter
        {
            get => this._filter;
            set
            {
                this._filter = value;

                ICollectionView view = CollectionViewSource.GetDefaultView(this.Objects);
                view.Filter = treeObject =>
                {
                    BinTreeObjectViewModel item = treeObject as BinTreeObjectViewModel;

                    //Do this in try-catch because Regex can throw an exception if pattern is wrong
                    try
                    {
                        return Regex.IsMatch(item.Name, value);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                };

                NotifyPropertyChanged();
            }
        }

        private string _binName;
        private BinTree _tree;
        private string _filter;
        private ObservableCollection<BinTreeObjectViewModel> _objects = new ObservableCollection<BinTreeObjectViewModel>();

        public BinTreeViewModel(string binName, BinTree binTree)
        {
            this._binName = binName;
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
    
        public BinTree BuildBinTree()
        {
            BinTree binTree = new BinTree();

            foreach(BinTreeObjectViewModel objectViewModel in this.Objects)
            {
                binTree.AddObject(objectViewModel.BuildObject());
            }

            this._tree.Dependencies.ForEach(x => binTree.Dependencies.Add(x));

            return binTree;
        }
    }
}

using LeagueToolkit.IO.PropertyBin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Data;
using System.IO;
using System.Windows.Input;
using Scavenger.MVVM.Commands;
using Scavenger.Utilities;
using Scavenger.IO.Templates;
using System.Reflection;
using LeagueToolkit.Meta.Classes;
using System.Threading.Tasks;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeViewModel : PropertyNotifier
    {
        public string BinName
        {
            get => Path.GetFileName(this._binPath);
            set
            {
                this._binPath = value;
                NotifyPropertyChanged();
            }
        }
        public string BinPath
        {
            get => this._binPath;
            set
            {
                this._binPath = value;
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
        public string ObjectFilter
        {
            get => this._objectFilter;
            set
            {
                this._objectFilter = value;

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
        public ObservableCollection<StructureTemplate> StructureTemplates
        {
            get => this._structureTemplates;
            set
            {
                this._structureTemplates = value;
                NotifyPropertyChanged();
            }
        }

        private string _binPath;
        private BinTree _tree;
        private string _objectFilter;
        private ObservableCollection<BinTreeObjectViewModel> _objects = new ObservableCollection<BinTreeObjectViewModel>();
        private ObservableCollection<StructureTemplate> _structureTemplates;

        private object _objectsLock = new object();

        public ICommand AddObjectCommand => new RelayCommand(OnAddObject);

        public BinTreeViewModel(string binPath, BinTree binTree, ObservableCollection<StructureTemplate> structureTemplates)
        {
            this._binPath = binPath;
            this._tree = binTree;
            this._structureTemplates = structureTemplates;

            GenerateObjects();
            Lint();
        }

        private void GenerateObjects()
        {
            Parallel.ForEach(this._tree.Objects, treeObject =>
            {
                BinTreeObjectViewModel objectViewModel = new BinTreeObjectViewModel(this, treeObject);

                lock (this._objectsLock)
                {
                    this.Objects.Add(objectViewModel);
                }
            });
        }
    
        public void Lint()
        {
            Assembly metaAssembly = Assembly.GetAssembly(typeof(ValueVector3));

            Parallel.ForEach(this.Objects, treeObject =>
            {
                treeObject.Lint(metaAssembly, null);
            });
        }

        private async void OnAddObject(object o)
        {
            NewBinTreeObjectViewModel newObjectViewModel = await DialogHelper.ShowNewBinTreeObjectDialog(this.StructureTemplates);
            if(newObjectViewModel is not null)
            {
                this.Objects.Add(new BinTreeObjectViewModel(this, newObjectViewModel.BuildObject()));
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

    public enum LintStatus
    {
        None,
        Warning,
        Valid
    }
}

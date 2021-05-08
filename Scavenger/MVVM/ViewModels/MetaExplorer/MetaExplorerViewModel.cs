using LeagueToolkit.Meta.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace Scavenger.MVVM.ViewModels.MetaExplorer
{
    public class MetaExplorerViewModel : PropertyNotifier
    {
        public string MetaClassFilter
        {
            get => this._metaClassFilter;
            set
            {
                this._metaClassFilter = value;

                ICollectionView view = CollectionViewSource.GetDefaultView(this.MetaClasses);
                view.Filter = item =>
                {
                    try
                    {
                        if(item is MetaClassViewModel metaClass)
                        {
                            return Regex.IsMatch(metaClass.Name, value);
                        }
                    }
                    catch (Exception)
                    {
                        return false;
                    }

                    return false;
                };

                NotifyPropertyChanged();
            }
        }

        public MetaClassViewModel SelectedMetaClass
        {
            get => this._selectedMetaClass;
            set
            {
                this._selectedMetaClass = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<MetaClassViewModel> MetaClasses
        {
            get => this._metaClasses;
            set
            {
                this._metaClasses = value;
                NotifyPropertyChanged();
            }
        }

        private string _metaClassFilter;

        private MetaClassViewModel _selectedMetaClass;
        private ObservableCollection<MetaClassViewModel> _metaClasses = new();

        public MetaExplorerViewModel()
        {
            Assembly metaAssembly = Assembly.GetAssembly(typeof(ValueVector3));

            foreach(TypeInfo metaClass in metaAssembly.DefinedTypes)
            {
                this.MetaClasses.Add(new MetaClassViewModel(metaClass));
            }
        }

    }
}

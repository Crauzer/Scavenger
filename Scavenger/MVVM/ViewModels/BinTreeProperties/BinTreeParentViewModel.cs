using LeagueToolkit.IO.PropertyBin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeParentViewModel : BinTreePropertyViewModel
    {
        [JsonIgnore] public string PropertyFilter
        {
            get => this._propertyFilter;
            set
            {
                this._propertyFilter = value;

                ICollectionView view = CollectionViewSource.GetDefaultView(this.Children);
                view.Filter = child =>
                {
                    try
                    {
                        if (child is BinTreeParentViewModel parent)
                        {
                            if(parent.Find(x => Regex.IsMatch(x.Name, value)) == null)
                            {
                                return false;
                            }
                            else
                            {
                                parent.PropertyFilter = value;
                                return true;
                            }
                        }
                        else if (child is BinTreePropertyViewModel property)
                        {
                            return Regex.IsMatch(property.Name, value);
                        }
                    }
                    catch(Exception)
                    {
                        return false;
                    }

                    return false;
                };

                NotifyPropertyChanged();
            }
        }
        [JsonIgnore] public string TextValueFilter
        {
            get => this._textValueFilter;
            set
            {
                this._textValueFilter = value;

                ICollectionView view = CollectionViewSource.GetDefaultView(this.Children);
                view.Filter = child =>
                {
                    try
                    {
                        if (child is BinTreeParentViewModel parent)
                        {
                            if (parent.GetAllProperties().Any(x => DoesValueMatch(x)))
                            {
                                parent.TextValueFilter = value;
                                return true;
                            }
                        }
                        else if (child is BinTreePropertyViewModel property)
                        {
                            return DoesValueMatch(property);
                        }
                    }
                    catch (Exception)
                    {
                        return false;
                    }

                    return false;
                };

                NotifyPropertyChanged();
            
                bool DoesValueMatch(BinTreePropertyViewModel property)
                {
                    return property switch
                    {
                        BinTreeStringViewModel @string => Regex.IsMatch(@string.Value, value),
                        BinTreeObjectLinkViewModel objectLink => Regex.IsMatch(objectLink.Value, value),
                        BinTreeWadEntryLinkViewModel wadEntryLink => Regex.IsMatch(wadEntryLink.Value, value),
                        BinTreeHashViewModel hash => Regex.IsMatch(hash.Value, value),
                        _ => false
                    };
                }
            }
        }
        [JsonIgnore] public BinTreeViewModel BinTree
        {
            get => this._binTree;
            set => this._binTree = value;
        }
        public ObservableCollection<BinTreePropertyViewModel> Children
        {
            get => this._children;
            set
            {
                this._children = value;
                NotifyPropertyChanged();
            }
        }

        private string _propertyFilter;
        private string _textValueFilter;
        private BinTreeViewModel _binTree;
        private ObservableCollection<BinTreePropertyViewModel> _children = new ObservableCollection<BinTreePropertyViewModel>();
    
        public BinTreeParentViewModel(BinTreeViewModel tree, BinTreeParentViewModel parent, BinTreeProperty treeProperty) : base(parent, treeProperty)
        {
            this._binTree = tree;
        }

        public void RemoveField(BinTreePropertyViewModel propertyViewModel)
        {
            this.Children.Remove(propertyViewModel);
        }

        public BinTreePropertyViewModel Find(Func<BinTreePropertyViewModel, bool> predicate)
        {
            return GetAllProperties().FirstOrDefault(predicate);
        }

        public IEnumerable<BinTreePropertyViewModel> GetAllProperties()
        {
            foreach(BinTreePropertyViewModel property in this.Children)
            {
                switch (property)
                {
                    case BinTreeParentViewModel parentChild:
                    {
                        foreach(BinTreePropertyViewModel childProperty in parentChild.GetAllProperties() ?? Enumerable.Empty<BinTreePropertyViewModel>())
                        {
                            yield return childProperty;
                        }

                        break;
                    }
                    case BinTreePropertyViewModel propertyChild:
                    {
                        yield return propertyChild;
                        break;
                    }
                }
            }
        }

        public override void SyncTreeProperty()
        {
            if(this is not BinTreeObjectViewModel)
            {
                this._binTree = this.Parent?.BinTree;
            }
        }
    }
}

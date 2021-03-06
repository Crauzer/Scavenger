using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.Meta.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeParentViewModel : BinTreePropertyViewModel
    {
        [JsonIgnore]
        public string PropertyFilter
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
                            if(string.IsNullOrEmpty(value))
                            {
                                parent.PropertyFilter = value;
                                return true;
                            }
                            else
                            {
                                bool thisNameMatches = Regex.IsMatch(this.Name, value);
                                if (parent.Find(x => Regex.IsMatch(x.Name, value)) == null
                                && thisNameMatches is false)
                                {
                                    return false;
                                }
                                else if (thisNameMatches)
                                {
                                    return true;
                                }
                                else
                                {
                                    parent.PropertyFilter = value;
                                    return true;
                                }
                            }
                        }
                        else if (child is BinTreePropertyViewModel property)
                        {
                            if (string.IsNullOrEmpty(value)) return true;
                            else return Regex.IsMatch(property.Name, value);
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
        [JsonIgnore]
        public string TextValueFilter
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
                            if (string.IsNullOrEmpty(value)
                            || parent.GetAllProperties().Any(x => DoesValueMatch(x)))
                            {
                                parent.TextValueFilter = value;
                                return true;
                            }
                            else if (string.IsNullOrEmpty(value))
                            {
                                parent.TextValueFilter = value;
                                return true;
                            }
                        }
                        else if (child is BinTreePropertyViewModel property)
                        {
                            if (string.IsNullOrEmpty(value) is false) return DoesValueMatch(property);
                            else return true;
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
        [JsonIgnore]
        public BinTreeViewModel BinTree
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

        protected void OnChildrenCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Assembly metaAssembly = Assembly.GetAssembly(typeof(ValueVector3));
            TypeInfo parentMetaClassType = this.Parent?.FindMetaClassType(metaAssembly);

            if (e.Action is NotifyCollectionChangedAction.Add)
            {
                Lint(metaAssembly, parentMetaClassType);
            }
            else if (e.Action is NotifyCollectionChangedAction.Remove)
            {
                Lint(metaAssembly, parentMetaClassType);
            }
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
            foreach (BinTreePropertyViewModel property in this.Children)
            {
                switch (property)
                {
                    case BinTreeParentViewModel parentChild:
                    {
                        foreach (BinTreePropertyViewModel childProperty in parentChild.GetAllProperties() ?? Enumerable.Empty<BinTreePropertyViewModel>())
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

        public void LintChildren(Assembly metaAssembly, TypeInfo parentMetaClassType)
        {
            foreach (BinTreePropertyViewModel property in this.Children)
            {
                property.Lint(metaAssembly, parentMetaClassType);

                if (property.LintStatus == LintStatus.Warning) this.LintStatus = LintStatus.Warning;
            }
        }

        public override void SyncTreeProperty()
        {
            if (this is not BinTreeObjectViewModel)
            {
                this._binTree = this.Parent?.BinTree;
            }
        }
    }
}

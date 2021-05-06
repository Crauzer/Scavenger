using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using LeagueToolkit.Meta;
using LeagueToolkit.Meta.Attributes;
using Newtonsoft.Json;
using Scavenger.MVVM.Commands;
using Scavenger.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using LeagueToolkit.Meta.Classes;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreePropertyViewModel : PropertyNotifier
    {
        [JsonIgnore]
        public LintStatus LintStatus
        {
            get => this._lintStatus;
            set
            {
                this._lintStatus = value;
                NotifyPropertyChanged();
            }
        }
        [JsonIgnore] public virtual string Header { get; set; }

        public string Name
        {
            get => this._name;
            set
            {
                this._name = value;
                this._nameHash = Fnv1a.HashLower(value);

                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(this.Header));
                NotifyPropertyChanged(nameof(this.NameHash));
            }
        }
        public uint NameHash
        {
            get => this._nameHash;
            set
            {
                this._name = this switch
                {
                    BinTreeObjectViewModel _ => Hashtables.GetObject(value),
                    _ => Hashtables.GetField(value)
                };
                this._nameHash = value;

                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(this.Name));
            }
        }
        public bool ShowName
        {
            get => this._showName;
            set
            {
                this._showName = value;
                NotifyPropertyChanged();
            }
        }

        private LintStatus _lintStatus = LintStatus.None;
        private bool _showName;
        private string _name;
        private uint _nameHash;

        [JsonIgnore] public BinTreeParentViewModel Parent { get; set; }
        [JsonIgnore] public BinTreeProperty TreeProperty { get; set; }

        public BinTreePropertyViewModel() { }
        public BinTreePropertyViewModel(BinTreeParentViewModel parent, BinTreeProperty treeProperty, bool showName = true)
        {
            this.Parent = parent;
            this.NameHash = treeProperty is null ? 0 : treeProperty.NameHash;
            this.ShowName = showName;
            this.TreeProperty = treeProperty;
        }

        public void Lint(Assembly metaAssembly, TypeInfo parentMetaClassType)
        {
            this.LintStatus = VerifyProperty(metaAssembly, parentMetaClassType);

            switch (this)
            {
                case BinTreeObjectViewModel treeObject:
                {
                    if (FindMetaClassType(metaAssembly, Fnv1a.HashLower(treeObject.MetaClass)) is TypeInfo treeObjectMetaClassType)
                    {
                        treeObject.LintChildren(metaAssembly, treeObjectMetaClassType);
                    }

                    return;
                }
                case BinTreeStructureViewModel structure:
                {
                    if (FindMetaClassType(metaAssembly, Fnv1a.HashLower(structure.MetaClass)) is TypeInfo structureMetaClassType)
                    {
                        structure.LintChildren(metaAssembly, structureMetaClassType);
                    }
                    else this.LintStatus = LintStatus.Warning;

                    return;
                }
                case BinTreeEmbeddedViewModel embedded:
                {
                    if (FindMetaClassType(metaAssembly, Fnv1a.HashLower(embedded.MetaClass)) is TypeInfo embeddedMetaClassType)
                    {
                        embedded.LintChildren(metaAssembly, embeddedMetaClassType);
                    }
                    else this.LintStatus = LintStatus.Warning;

                    return;
                }
                case BinTreeContainerViewModel container:
                {
                    if (FindPropertyInfo(parentMetaClassType) is not null)
                    {
                        container.LintChildren(metaAssembly, null);
                    }
                    else this.LintStatus = LintStatus.Warning;

                    break;
                }
                case BinTreeUnorderedContainerViewModel unorderedContainer:
                {
                    if (FindPropertyInfo(parentMetaClassType) is not null)
                    {
                        unorderedContainer.LintChildren(metaAssembly, null);
                    }
                    else this.LintStatus = LintStatus.Warning;

                    break;
                }
                case BinTreeMapViewModel map:
                {
                    map.LintChildren(metaAssembly, null);

                    break;
                }
                case BinTreeMapEntryViewModel mapEntry:
                {
                    mapEntry.KeyProperty.Lint(metaAssembly, null);
                    mapEntry.ValueProperty.Lint(metaAssembly, null);

                    if (mapEntry.KeyProperty.LintStatus == LintStatus.Warning
                        || mapEntry.ValueProperty.LintStatus == LintStatus.Warning)
                    {
                        mapEntry.LintStatus = LintStatus.Warning;
                    }

                    break;
                }
                case BinTreePropertyViewModel:
                {
                    if (parentMetaClassType is null)
                    {
                        this.LintStatus = LintStatus.Valid;
                        return;
                    }

                    foreach (PropertyInfo propertyInfo in parentMetaClassType.GetProperties())
                    {
                        MetaPropertyAttribute metaPropertyAttribute = propertyInfo.GetCustomAttribute(typeof(MetaPropertyAttribute)) as MetaPropertyAttribute;

                        if (metaPropertyAttribute.NameHash == this.NameHash)
                        {
                            this.LintStatus = LintStatus.Valid;
                            return;
                        }
                    }

                    break;
                }
            }
        }
        protected LintStatus VerifyProperty(Assembly metaAssembly, TypeInfo parentMetaClassType)
        {
            if (parentMetaClassType is null)
            {
                return LintStatus.Valid;
            }
            else if (FindPropertyInfo(parentMetaClassType) is not null)
            {
                return LintStatus.Valid;
            }
            else
            {
                return LintStatus.Warning;
            }
        }
        protected PropertyInfo FindPropertyInfo(TypeInfo parentMetaClassType)
        {
            foreach (PropertyInfo propertyInfo in parentMetaClassType.GetProperties())
            {
                MetaPropertyAttribute metaPropertyAttribute = propertyInfo.GetCustomAttribute(typeof(MetaPropertyAttribute)) as MetaPropertyAttribute;
                if (metaPropertyAttribute.NameHash == this.NameHash)
                {
                    return propertyInfo;
                }
            }

            return null;
        }
        protected TypeInfo FindMetaClassType(Assembly metaAssembly)
        {
            uint metaClassHash = this switch
            {
                BinTreeObjectViewModel treeObject => Fnv1a.HashLower(treeObject.MetaClass),
                BinTreeStructureViewModel structure => Fnv1a.HashLower(structure.MetaClass),
                BinTreeEmbeddedViewModel embedded => Fnv1a.HashLower(embedded.MetaClass),
                _ => default(uint)
            };

            return FindMetaClassType(metaAssembly, metaClassHash);
        }
        private TypeInfo FindMetaClassType(Assembly metaAssembly, uint metaClassHash)
        {
            foreach (TypeInfo typeInfo in metaAssembly.DefinedTypes)
            {
                if (typeInfo.GetInterface(nameof(IMetaClass)) is not null)
                {
                    MetaClassAttribute metaPropertyAttribute = typeInfo.GetCustomAttribute(typeof(MetaClassAttribute)) as MetaClassAttribute;
                    if (metaPropertyAttribute.NameHash == metaClassHash)
                    {
                        return typeInfo;
                    }
                }
            }

            return null;
        }

        public virtual BinTreeProperty BuildProperty()
        {
            return new BinTreeNone(null, this._nameHash);
        }
        public virtual void SyncTreeProperty() { }
    }
}

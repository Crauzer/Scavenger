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
            switch (this)
            {
                case BinTreeObjectViewModel treeObject:
                {
                    if (FindMetaClassType(Fnv1a.HashLower(treeObject.MetaClass)) is TypeInfo treeObjectMetaClassType)
                    {
                        this.LintStatus = LintStatus.Valid;

                        foreach (BinTreePropertyViewModel property in treeObject.Children)
                        {
                            property.Lint(metaAssembly, treeObjectMetaClassType);

                            if (property.LintStatus == LintStatus.Warning) this.LintStatus = LintStatus.Warning;
                        }
                    }

                    return;
                }
                case BinTreeStructureViewModel structure:
                {
                    if (FindPropertyInfo(parentMetaClassType) is not null
                        && FindMetaClassType(Fnv1a.HashLower(structure.MetaClass)) is TypeInfo structureMetaClassType)
                    {
                        this.LintStatus = LintStatus.Valid;

                        foreach (BinTreePropertyViewModel property in structure.Children)
                        {
                            property.Lint(metaAssembly, structureMetaClassType);

                            if (property.LintStatus == LintStatus.Warning) this.LintStatus = LintStatus.Warning;
                        }
                    }
                    else this.LintStatus = LintStatus.Warning;

                    return;
                }
                case BinTreeEmbeddedViewModel embedded:
                {
                    if (embedded.Name == "loadscreen") { 
                    }

                    if (FindPropertyInfo(parentMetaClassType) is not null
                        && FindMetaClassType(Fnv1a.HashLower(embedded.MetaClass)) is TypeInfo embeddedMetaClassType)
                    {
                        this.LintStatus = LintStatus.Valid;

                        foreach (BinTreePropertyViewModel property in embedded.Children)
                        {
                            property.Lint(metaAssembly, embeddedMetaClassType);

                            if (property.LintStatus == LintStatus.Warning) this.LintStatus = LintStatus.Warning;
                        }
                    }
                    else this.LintStatus = LintStatus.Warning;

                    return;
                }
                case BinTreePropertyViewModel:
                {
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

            this.LintStatus = LintStatus.Warning;

            PropertyInfo FindPropertyInfo(TypeInfo parentMetaClassType)
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
            TypeInfo FindMetaClassType(uint metaClassHash)
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
        }

        public virtual BinTreeProperty BuildProperty()
        {
            return new BinTreeNone(null, this._nameHash);
        }
        public virtual void SyncTreeProperty() { }
    }
}

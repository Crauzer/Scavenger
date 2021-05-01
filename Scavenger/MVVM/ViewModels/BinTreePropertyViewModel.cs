using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using Scavenger.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Text;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreePropertyViewModel : PropertyNotifier
    {
        public virtual string Header { get; set; }
        public string Name
        {
            get => Hashtables.GetField(this._nameHash);
            set
            {
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

        private bool _showName;
        private uint _nameHash;

        public BinTreeParentViewModel Parent { get; private set; }
        public BinTreeProperty TreeProperty { get; private set; }

        public BinTreePropertyViewModel(BinTreeParentViewModel parent, BinTreeProperty treeProperty, bool showName = true)
        {
            this.Parent = parent;
            this.NameHash = treeProperty is null ? 0 : treeProperty.NameHash;
            this.ShowName = showName;
            this.TreeProperty = treeProperty;
        }

        public virtual BinTreeProperty BuildProperty()
        {
            return new BinTreeNone(null, this._nameHash);
        }
    }
}

using CSharpImageLibrary;
using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.IO.MapGeometry;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using LeagueToolkit.IO.SimpleSkinFile;
using LeagueToolkit.IO.StaticObjectFile;
using Newtonsoft.Json;
using Scavenger.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeStringViewModel : BinTreePropertyViewModel
    {
        [JsonIgnore] public PreviewViewModel Preview
        {
            get => this._preview;
            set
            {
                this._preview = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsAsset
        {
            get => this._isAsset;
            set
            {
                this._isAsset = value;
                NotifyPropertyChanged();
            }
        }
        public bool ShouldPreviewAsset
        {
            get => this._shouldPreviewAsset;
            set
            {
                this._shouldPreviewAsset = value;
                NotifyPropertyChanged();

                PreviewAsset();
            }
        }
        public string Value
        {
            get => this._value;
            set
            {
                this._value = value;

                this.IsAsset = BinUtilities.IsAsset(this) && BinUtilities.IsPreviewableAsset(this);
                PreviewAsset();

                NotifyPropertyChanged();
                SyncTreeProperty();
            }
        }

        private PreviewViewModel _preview = new PreviewViewModel();
        private bool _isAsset;
        private bool _shouldPreviewAsset;
        private string _value;

        public BinTreeStringViewModel() { }
        public BinTreeStringViewModel(BinTreeParentViewModel parent, BinTreeString treeProperty) : base(parent, treeProperty)
        {
            this._value = treeProperty.Value;
            this._isAsset = BinUtilities.IsAsset(this) && BinUtilities.IsPreviewableAsset(this);
        }

        private void PreviewAsset()
        {
            // Check if the string is an asset
            string extension = Path.GetExtension(this.Value);
            if (string.IsNullOrEmpty(extension) is false
                && this.Parent is not null)
            {
                string assetPath = BinUtilities.ResolveAssetPath(this.Parent.BinTree.BinPath, this.Value);
                if (File.Exists(assetPath))
                {
                    switch (Path.GetExtension(assetPath))
                    {
                        case ".skn":
                        {
                            this.Preview.Preview(new SimpleSkin(assetPath));
                            break;
                        }
                        case ".scb":
                        {
                            this.Preview.Preview(StaticObject.ReadSCB(assetPath));
                            break;
                        }
                        case ".sco":
                        {
                            this.Preview.Preview(StaticObject.ReadSCO(assetPath));
                            break;
                        }
                        case ".dds":
                        {
                            try
                            {
                                MemoryStream imageStream = new MemoryStream(File.ReadAllBytes(assetPath));
                                this.Preview.Preview(new ImageEngineImage(imageStream));
                            }
                            catch (FileFormatException)
                            {
                                this.Preview.Clear();
                            }
                            break;
                        }
                        case ".mapgeo":
                        {
                            this.Preview.Preview(new MapGeometry(assetPath));
                            break;
                        }
                    }
                }
            }

            
        }

        public override void SyncTreeProperty()
        {
            this.TreeProperty = new BinTreeString((IBinTreeParent)this.Parent?.TreeProperty, this.NameHash, this.Value);
        }

        public override BinTreeProperty BuildProperty()
        {
            return new BinTreeString(null, this.NameHash, this.Value);
        }
    }
}

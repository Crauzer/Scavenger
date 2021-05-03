using CSharpImageLibrary;
using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.IO.MapGeometry;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using LeagueToolkit.IO.SimpleSkinFile;
using LeagueToolkit.IO.StaticObjectFile;
using Scavenger.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Scavenger.MVVM.ViewModels
{
    public class BinTreeStringViewModel : BinTreePropertyViewModel
    {
        public PreviewViewModel Preview
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
            }
        }
        public string Value
        {
            get => this._value;
            set
            {
                this._value = value;
                NotifyPropertyChanged();
            }
        }

        private PreviewViewModel _preview;
        private bool _isAsset;
        private bool _shouldPreviewAsset;
        private string _value;

        public BinTreeStringViewModel(BinTreeParentViewModel parent, BinTreeString treeProperty) : base(parent, treeProperty)
        {
            this.Value = treeProperty.Value;
            this.Preview = new PreviewViewModel();
            this.IsAsset = BinTreeUtilities.IsAsset(this) && BinTreeUtilities.IsPreviewableAsset(this);
            this.ShouldPreviewAsset = false;

            // Check if the string is an asset
            string extension = Path.GetExtension(this.Value);
            if(string.IsNullOrEmpty(extension) is false)
            {
                string binPath = parent.BinTree.BinPath;
                int indexOfData = binPath.LastIndexOf("\\data\\");
                string binFolder = indexOfData == -1 ? Path.GetDirectoryName(binPath) : binPath.Remove(binPath.LastIndexOf("\\data\\"));
                string assetPath = Path.Combine(binFolder, this.Value);
                string assetExtension = Path.GetExtension(assetPath);

                switch (assetExtension)
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

        public override BinTreeProperty BuildProperty()
        {
            return new BinTreeString(null, this.NameHash, this.Value);
        }
    }
}

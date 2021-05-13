using LeagueToolkit.Meta;
using LeagueToolkit.Meta.Classes;
using Scavenger.MVVM.ViewModels.PrimitiveStructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels.Meta.Structures
{
    public class VfxPaletteDefinitionViewModel : PropertyNotifier
    {
        public int PaletteCount
        {
            get => this._paletteCount;
            set
            {
                this._paletteCount = value;
                NotifyPropertyChanged();
            }
        }
        public byte PaletteTextureAddressMode
        {
            get => this._paletteTextureAddressMode;
            set
            {
                this._paletteTextureAddressMode = value;
                NotifyPropertyChanged();
            }
        }
        public string PaletteTexture
        {
            get => this._paletteTexture;
            set
            {
                this._paletteTexture = value;
                NotifyPropertyChanged();
            }
        }

        public ValueVector3ViewModel PaletteSelector
        {
            get => this._paletteSelector;
            set
            {
                this._paletteSelector = value;
                NotifyPropertyChanged();
            }
        }

        public ValueColorViewModel PaletteSrcMixColor
        {
            get => this._paletteSrcMixColor;
            set
            {
                this._paletteSrcMixColor = value;
                NotifyPropertyChanged();
            }
        }

        public ValueFloatViewModel M886635206
        {
            get => this._m886635206;
            set
            {
                this._m886635206 = value;
                NotifyPropertyChanged();
            }
        }
        public ValueFloatViewModel M1157448907
        {
            get => this._m1157448907;
            set
            {
                this._m1157448907 = value;
                NotifyPropertyChanged();
            }
        }

        private int _paletteCount;
        private byte _paletteTextureAddressMode;
        private string _paletteTexture;

        private ValueVector3ViewModel _paletteSelector;

        private ValueColorViewModel _paletteSrcMixColor;

        private ValueFloatViewModel _m886635206;
        private ValueFloatViewModel _m1157448907;

        public VfxPaletteDefinitionViewModel() : this(new VfxPaletteDefinitionData()) { }
        public VfxPaletteDefinitionViewModel(VfxPaletteDefinitionData paletteDefiniton)
        {
            this.PaletteCount = paletteDefiniton.PaletteCount;
            this.PaletteTextureAddressMode = paletteDefiniton.PaletteTextureAddressMode;
            this.PaletteTexture = paletteDefiniton.PaletteTexture;

            this.PaletteSelector = new ValueVector3ViewModel(paletteDefiniton.PaletteSelector);

            this.PaletteSrcMixColor = new ValueColorViewModel(paletteDefiniton.PalleteSrcMixColor);

            this.M886635206 = new ValueFloatViewModel(paletteDefiniton.m886635206);
            this.M1157448907 = new ValueFloatViewModel(paletteDefiniton.m1157448907);
        }

        public VfxPaletteDefinitionData ToVfxPaletteDefinitionData()
        {
            return new VfxPaletteDefinitionData()
            {
                PaletteCount = this.PaletteCount,
                PaletteTextureAddressMode = this.PaletteTextureAddressMode,
                PaletteTexture = this.PaletteTexture,

                PaletteSelector = new MetaEmbedded<ValueVector3>(this.PaletteSelector.ToValueVector3()),

                PalleteSrcMixColor = new MetaEmbedded<ValueColor>(this.PaletteSrcMixColor.ToValueColor()),

                m886635206 = new MetaEmbedded<ValueFloat>(this.M886635206.ToValueFloat()),
                m1157448907 = new MetaEmbedded<ValueFloat>(this.M1157448907.ToValueFloat())
            };
        }
    }
}

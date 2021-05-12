using LeagueToolkit.Meta;
using LeagueToolkit.Meta.Classes;
using Scavenger.MVVM.ViewModels.PrimitiveStructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels.Meta.Structures
{
    public class VfxTrailDefinitionViewModel : PropertyNotifier
    {
        public byte Mode
        {
            get => this._mode;
            set
            {
                this._mode = value;
                NotifyPropertyChanged();
            }
        }
        public byte SmoothingMode
        {
            get => this._smoothingMode;
            set
            {
                this._smoothingMode = value;
                NotifyPropertyChanged();
            }
        }

        public int MaxAddedPerFrame
        {
            get => this._maxAddedPerFrame;
            set
            {
                this._maxAddedPerFrame = value;
                NotifyPropertyChanged();
            }
        }
        public ValueVector3ViewModel BirthTrailingSize
        {
            get => this._birthTilingSize;
            set
            {
                this._birthTilingSize = value;
                NotifyPropertyChanged();
            }
        }

        public float Cutoff
        {
            get => this._cutoff;
            set
            {
                this._cutoff = value;
                NotifyPropertyChanged();
            }
        }

        private byte _mode;
        private byte _smoothingMode;

        private int _maxAddedPerFrame;
        private ValueVector3ViewModel _birthTilingSize;

        private float _cutoff;

        public VfxTrailDefinitionViewModel(VfxTrailDefinitionData trailDefinition)
        {
            this.Mode = trailDefinition.Mode;
            this.SmoothingMode = trailDefinition.SmoothingMode;

            this.MaxAddedPerFrame = trailDefinition.MaxAddedPerFrame;
            this.BirthTrailingSize = new ValueVector3ViewModel(trailDefinition.BirthTilingSize);

            this.Cutoff = trailDefinition.Cutoff;
        }

        public VfxTrailDefinitionData ToVfxTrailDefinitionData()
        {
            return new VfxTrailDefinitionData()
            {
                Mode = this.Mode,
                SmoothingMode = this.SmoothingMode,

                MaxAddedPerFrame = this.MaxAddedPerFrame,
                BirthTilingSize = new MetaEmbedded<ValueVector3>(this.BirthTrailingSize.ToValueVector3()),

                Cutoff = this.Cutoff
            };
        }
    }
}

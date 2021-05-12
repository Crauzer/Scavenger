using LeagueToolkit.Meta;
using LeagueToolkit.Meta.Classes;
using Scavenger.MVVM.ViewModels.PrimitiveStructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels.Meta.Structures
{
    public class VfxBeamDefinitionViewModel : PropertyNotifier
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
        public byte TrailMode
        {
            get => this._trailMode;
            set
            {
                this._trailMode = value;
                NotifyPropertyChanged();
            }
        }

        public int Segments
        {
            get => this._segments;
            set
            {
                this._segments = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsColorBindedWithDistance
        {
            get => this._isColorBindedWithDistance;
            set
            {
                this._isColorBindedWithDistance = value;
                NotifyPropertyChanged();
            }
        }
        public ValueColorViewModel AnimatedColorWithDistance
        {
            get => this._animatedColorWithDistance;
            set
            {
                this._animatedColorWithDistance = value;
                NotifyPropertyChanged();
            }
        }

        public ValueVector3ViewModel BirthTilingSize
        {
            get => this._birthTilingSize;
            set
            {
                this._birthTilingSize = value;
                NotifyPropertyChanged();
            }
        }

        public Vector3ViewModel LocalSpaceTargetOffset
        {
            get => this._localSpaceTargetOffset;
            set
            {
                this._localSpaceTargetOffset = value;
                NotifyPropertyChanged();
            }
        }
        public Vector3ViewModel LocalSpaceSourceOffset
        {
            get => this._localSpaceSourceOffset;
            set
            {
                this._localSpaceSourceOffset = value;
                NotifyPropertyChanged();
            }
        }

        private byte _mode;
        private byte _trailMode;

        private int _segments;

        private bool _isColorBindedWithDistance;
        private ValueColorViewModel _animatedColorWithDistance;

        private ValueVector3ViewModel _birthTilingSize;

        private Vector3ViewModel _localSpaceTargetOffset;
        private Vector3ViewModel _localSpaceSourceOffset;

        public VfxBeamDefinitionViewModel(VfxBeamDefinitionData beamDefinition)
        {
            this.Mode = beamDefinition.Mode;
            this.TrailMode = beamDefinition.TrailMode;

            this.Segments = beamDefinition.Segments;

            this.IsColorBindedWithDistance = beamDefinition.IsColorBindedWithDistance;
            this.AnimatedColorWithDistance = new ValueColorViewModel(beamDefinition.AnimatedColorWithDistance);

            this.BirthTilingSize = new ValueVector3ViewModel(beamDefinition.BirthTilingSize);

            this.LocalSpaceTargetOffset = new Vector3ViewModel(beamDefinition.LocalSpaceTargetOffset);
            this.LocalSpaceSourceOffset = new Vector3ViewModel(beamDefinition.LocalSpaceSourceOffset);
        }

        public VfxBeamDefinitionData ToVfxBeamDefinitionData()
        {
            return new VfxBeamDefinitionData()
            {
                Mode = this.Mode,
                TrailMode = this.TrailMode,

                Segments = this.Segments,

                IsColorBindedWithDistance = this.IsColorBindedWithDistance,
                AnimatedColorWithDistance = new MetaEmbedded<ValueColor>(this.AnimatedColorWithDistance.ToValueColor()),

                BirthTilingSize = new MetaEmbedded<ValueVector3>(this.BirthTilingSize.ToValueVector3()),

                LocalSpaceTargetOffset = this.LocalSpaceTargetOffset.ToVector(),
                LocalSpaceSourceOffset = this.LocalSpaceSourceOffset.ToVector()
            };
        }
    }
}

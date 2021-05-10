using LeagueToolkit.Meta.Classes;
using Scavenger.MVVM.ViewModels.Meta;
using Scavenger.MVVM.ViewModels.PrimitiveStructures;
using System.Numerics;

namespace Scavenger.MVVM.ViewModels.ObjectEditors.VfxSystemDefinitionDataEditor.Components
{
    public class ScaleEmitterComponent : EmitterComponent
    {
        public override string Name => "Scale";

        // ----------- PROPERTIES ----------- \\

        public bool IsUniformScale
        {
            get => this._isUniformScale;
            set
            {
                this._isUniformScale = value;
                NotifyPropertyChanged();
            }
        }
        public bool ScaleUpFromOrigin
        {
            get => this._scaleUpFromOrigin;
            set
            {
                this._scaleUpFromOrigin = value;
                NotifyPropertyChanged();
            }
        }

        public float ScaleEmitOffsetByBoundObjectHeight
        {
            get => this._scaleEmitOffsetByBoundObjectHeight;
            set
            {
                this._scaleEmitOffsetByBoundObjectHeight = value;
                NotifyPropertyChanged();
            }
        }
        public float ScaleEmitOffsetByBoundObjectRadius
        {
            get => this._scaleEmitOffsetByBoundObjectRadius;
            set
            {
                this._scaleEmitOffsetByBoundObjectRadius = value;
                NotifyPropertyChanged();
            }
        }
        public float ScaleEmitOffsetByBoundObjectSize
        {
            get => this._scaleEmitOffsetByBoundObjectSize;
            set
            {
                this._scaleEmitOffsetByBoundObjectSize = value;
                NotifyPropertyChanged();
            }
        }

        public float ScaleBirthScaleByBoundObjectHeight
        {
            get => this._scaleBirthScaleByBoundObjectHeight;
            set
            {
                this._scaleBirthScaleByBoundObjectHeight = value;
                NotifyPropertyChanged();
            }
        }
        public float ScaleBirthScaleByBoundObjectRadius
        {
            get => this._scaleBirthScaleByBoundObjectRadius;
            set
            {
                this._scaleBirthScaleByBoundObjectRadius = value;
                NotifyPropertyChanged();
            }
        }
        public float ScaleBirthScaleByBoundObjectSize
        {
            get => this._scaleBirthScaleByBoundObjectSize;
            set
            {
                this._scaleBirthScaleByBoundObjectSize = value;
                NotifyPropertyChanged();
            }
        }

        public float DirectionVelocityScale
        {
            get => this._directionVelocityScale;
            set
            {
                this._directionVelocityScale = value;
                NotifyPropertyChanged();
            }
        }
        public float DirectionVelocityMinScale
        {
            get => this._directionVelocityMinScale;
            set
            {
                this._directionVelocityMinScale = value;
                NotifyPropertyChanged();
            }
        }

        public MetaStructureViewModel<FlexTypeFloatViewModel> FlexScaleBirthScale
        {
            get => this._flexScaleBirthScale;
            set
            {
                this._flexScaleBirthScale = value;
                NotifyPropertyChanged();
            }
        }
        public MetaStructureViewModel<FlexTypeFloatViewModel> FlexScaleEmitOffset
        {
            get => this._flexScaleEmitOffset;
            set
            {
                this._flexScaleEmitOffset = value;
                NotifyPropertyChanged();
            }
        }
        public MetaStructureViewModel<FlexTypeFloatViewModel> FlexInstanceScale
        {
            get => this._flexInstanceScale;
            set
            {
                this._flexInstanceScale = value;
                NotifyPropertyChanged();
            }
        }

        public Vector2ViewModel ScaleBias
        {
            get => this._scaleBias;
            set
            {
                this._scaleBias = value;
                NotifyPropertyChanged();
            }
        }
        public ValueVector3ViewModel LingerScale0
        {
            get => this._lingerScale0;
            set
            {
                this._lingerScale0 = value;
                NotifyPropertyChanged();
            }
        }
        public float EmissionMeshScale
        {
            get => this._emissionMeshScale;
            set
            {
                this._emissionMeshScale = value;
                NotifyPropertyChanged();
            }
        }

        public ValueVector3ViewModel Scale0
        {
            get => this._scale0;
            set
            {
                this._scale0 = value;
                NotifyPropertyChanged();
            }
        }
        public ValueFloatViewModel Scale1
        {
            get => this._scale1;
            set
            {
                this._scale1 = value;
                NotifyPropertyChanged();
            }
        }
        public Vector3ViewModel ScaleOverride
        {
            get => this._scaleOverride;
            set
            {
                this._scaleOverride = value;
                NotifyPropertyChanged();
            }
        }

        public ValueVector3ViewModel BirthScale0
        {
            get => this._birthScale0;
            set
            {
                this._birthScale0 = value;
                NotifyPropertyChanged();
            }
        }
        public ValueFloatViewModel BirthScale1
        {
            get => this._birthScale1;
            set
            {
                this._birthScale1 = value;
                NotifyPropertyChanged();
            }
        }

        // ----------- FIELDS ----------- \\

        private bool _isUniformScale;
        private bool _scaleUpFromOrigin;

        private float _scaleEmitOffsetByBoundObjectHeight;
        private float _scaleEmitOffsetByBoundObjectRadius;
        private float _scaleEmitOffsetByBoundObjectSize;

        private float _scaleBirthScaleByBoundObjectHeight;
        private float _scaleBirthScaleByBoundObjectRadius;
        private float _scaleBirthScaleByBoundObjectSize;

        private float _directionVelocityScale;
        private float _directionVelocityMinScale;

        private MetaStructureViewModel<FlexTypeFloatViewModel> _flexScaleBirthScale;
        private MetaStructureViewModel<FlexTypeFloatViewModel> _flexScaleEmitOffset;
        private MetaStructureViewModel<FlexTypeFloatViewModel> _flexInstanceScale;

        private Vector2ViewModel _scaleBias;
        private ValueVector3ViewModel _lingerScale0;
        private float _emissionMeshScale;

        private ValueVector3ViewModel _scale0;
        private ValueFloatViewModel _scale1;
        private Vector3ViewModel _scaleOverride;

        private ValueVector3ViewModel _birthScale0;
        private ValueFloatViewModel _birthScale1;

        public ScaleEmitterComponent(VfxSystemDefinitionDataViewModel system, VfxEmitterDefinitionData emitter) : base(system)
        {
            this.IsUniformScale = emitter.IsUniformScale.Value == 1 ? true : false;
            this.ScaleUpFromOrigin = emitter.ScaleUpFromOrigin.Value == 1 ? true : false;

            this.ScaleEmitOffsetByBoundObjectHeight = emitter.ScaleEmitOffsetByBoundObjectHeight;
            this.ScaleEmitOffsetByBoundObjectRadius = emitter.ScaleEmitOffsetByBoundObjectRadius;
            this.ScaleEmitOffsetByBoundObjectSize = emitter.ScaleEmitOffsetByBoundObjectSize;

            this.ScaleBirthScaleByBoundObjectHeight = emitter.ScaleBirthScaleByBoundObjectHeight;
            this.ScaleBirthScaleByBoundObjectRadius = emitter.ScaleBirthScaleByBoundObjectRadius;
            this.ScaleBirthScaleByBoundObjectSize = emitter.ScaleBirthScaleByBoundObjectSize;

            this.DirectionVelocityScale = emitter.DirectionVelocityScale;
            this.DirectionVelocityMinScale = emitter.DirectionVelocityMinScale;

            this.FlexScaleBirthScale = emitter.FlexScaleBirthScale is null ? new() : new(new(emitter.FlexScaleBirthScale));
            this.FlexScaleEmitOffset = emitter.FlexScaleEmitOffset is null ? new() : new(new(emitter.FlexScaleEmitOffset));
            this.FlexInstanceScale = emitter.FlexInstanceScale is null ? new() : new(new(emitter.FlexInstanceScale));

            this.ScaleBias = new Vector2ViewModel(emitter.ScaleBias);
            this.LingerScale0 = new ValueVector3ViewModel(emitter.LingerScale0);
            this.EmissionMeshScale = emitter.EmissionMeshScale;

            this.Scale0 = new ValueVector3ViewModel(emitter.Scale0);
            this.Scale1 = new ValueFloatViewModel(emitter.Scale1);
            this.ScaleOverride = new Vector3ViewModel(emitter.ScaleOverride);

            this.BirthScale0 = new ValueVector3ViewModel(emitter.BirthScale0);
            this.BirthScale1 = new ValueFloatViewModel(emitter.BirthScale1);
        }
    }
}

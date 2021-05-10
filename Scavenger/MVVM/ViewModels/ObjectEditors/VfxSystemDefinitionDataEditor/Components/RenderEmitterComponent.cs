using LeagueToolkit.Meta.Classes;
using Scavenger.MVVM.ViewModels.Meta;
using Scavenger.MVVM.ViewModels.PrimitiveStructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Scavenger.MVVM.ViewModels.ObjectEditors.VfxSystemDefinitionDataEditor.Components
{
    public class RenderEmitterComponent : EmitterComponent
    {
        public override string Name => "Render";

        private ObservableCollection<string> _keywordsRequired;
        private ObservableCollection<string> _keywordsIncluded;

        private byte _importance;

        private bool _isGroundLayer;
        private bool _isSingleParticle;

        private MetaOptionalViewModel<float> _lifetime;

        private ValueColorViewModel _birthColor;
        private ValueColorViewModel _color;
        private Vector2ViewModel _colorLookUpOffsets;
        private byte _colorLookUpTypeX;
        private byte _colorLookUpTypeY;

        private bool _useLingerColor;
        private ValueColorViewModel _lingerColor;

        private MetaOptionalViewModel<float> _maximumRateByVelocity;
        private ValueVector2ViewModel _rateByVelocityFunction;

        private bool _colorRenderFlags;
        private byte _meshRenderFlags;

        private bool _disableBackfaceCull;
        private byte _colorblindVisibility;

        private byte _censorPolicy;
        private byte _spectatorPolicy;

        private float _timeActiveDuringPeriod;
        
        private byte _stencilMode;
        private byte _blendMode;
        private byte _stencilRef;
        private byte _alphaRef;
        private short _pass;

        private bool _useEmissionMeshNormalForBirth;
        private string _emissionMeshName;

        private MetaOptionalViewModel<float> _emitterLinger;
        private MetaOptionalViewModel<float> _particleLinger;
        private byte _particleLingerType;

        private ValueFloatViewModel _particleLifetime;

        public RenderEmitterComponent(VfxSystemDefinitionDataViewModel system, VfxEmitterDefinitionData vfxEmitterDefinitionData) : base(system)
        {

        }
    }
}

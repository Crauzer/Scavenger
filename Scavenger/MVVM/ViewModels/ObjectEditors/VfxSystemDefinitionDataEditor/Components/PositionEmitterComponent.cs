using LeagueToolkit.Meta.Classes;
using Scavenger.MVVM.ViewModels.PrimitiveStructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels.ObjectEditors.VfxSystemDefinitionDataEditor.Components
{
    public class PositionEmitterComponent : EmitterComponent
    {
        public override string Name => "Position";

        private ValueVector3ViewModel _velocity;
        private ValueVector3ViewModel _lingerVelocity;

        private bool _useLingerVelocity;

        public PositionEmitterComponent(VfxSystemDefinitionDataViewModel system, VfxEmitterDefinitionData vfxEmitterDefinitionData) : base(system)
        {

        }
    }
}

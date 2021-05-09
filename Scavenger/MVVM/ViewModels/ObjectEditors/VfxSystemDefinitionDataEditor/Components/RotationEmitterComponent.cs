using LeagueToolkit.Meta.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels.ObjectEditors.VfxSystemDefinitionDataEditor.Components
{
    public class RotationEmitterComponent : EmitterComponent
    {
        public override string Name => "Rotation";

        public RotationEmitterComponent(VfxSystemDefinitionDataViewModel system, VfxEmitterDefinitionData vfxEmitterDefinitionData) : base(system)
        {

        }
    }
}

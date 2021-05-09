using LeagueToolkit.Meta.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels.ObjectEditors.VfxSystemDefinitionDataEditor.Components
{
    public class RenderEmitterComponent : EmitterComponent
    {
        public override string Name => "Render";

        public RenderEmitterComponent(VfxSystemDefinitionDataViewModel system, VfxEmitterDefinitionData vfxEmitterDefinitionData) : base(system)
        {

        }
    }
}

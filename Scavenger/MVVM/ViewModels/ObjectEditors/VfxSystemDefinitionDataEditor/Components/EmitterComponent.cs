namespace Scavenger.MVVM.ViewModels.ObjectEditors.VfxSystemDefinitionDataEditor.Components
{
    public class EmitterComponent : PropertyNotifier
    {
        public virtual string Name => "Component";

        public VfxSystemDefinitionDataViewModel System { get; }

        public EmitterComponent(VfxSystemDefinitionDataViewModel system)
        {
            this.System = system;
        }
    }
}

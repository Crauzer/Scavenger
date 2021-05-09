using LeagueToolkit.Meta.Classes;
using Scavenger.MVVM.ViewModels.PrimitiveStructures;
using System.Numerics;

namespace Scavenger.MVVM.ViewModels.ObjectEditors.VfxSystemDefinitionDataEditor.Components
{
    public class ScaleEmitterComponent : EmitterComponent
    {
        public override string Name => "Scale";

        public bool IsUniformScale
        {
            get => this._isUniformScale;
            set
            {
                this._isUniformScale = value;
                NotifyPropertyChanged();
            }
        }
        public ValueVector3ViewModel InitialScale
        {
            get => this._initialScale;
            set
            {
                this._initialScale = value;
                NotifyPropertyChanged();
            }
        }
        public ValueVector3ViewModel Scale0
        {
            get => this._particleScale;
            set
            {
                this._particleScale = value;
                NotifyPropertyChanged();
            }
        }
        public ValueFloat FlexScale
        {
            get => this._flexScale;
            set
            {
                this._flexScale = value;
                NotifyPropertyChanged();
            }
        }
        public ValueFloat ScaleByRadius
        {
            get => this._scaleByRadius;
            set
            {
                this._scaleByRadius = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isUniformScale;
        private ValueVector3ViewModel _initialScale;
        private ValueVector3ViewModel _particleScale;
        private ValueFloat _flexScale;
        private ValueFloat _scaleByRadius;

        public ScaleEmitterComponent(VfxSystemDefinitionDataViewModel system, VfxEmitterDefinitionData vfxEmitterDefinitionData) : base(system)
        {
            this.IsUniformScale = vfxEmitterDefinitionData.IsUniformScale.Value == 1 ? true : false;
            this.InitialScale = new ValueVector3ViewModel(vfxEmitterDefinitionData.BirthScale0);
            this.Scale0 = new ValueVector3ViewModel(vfxEmitterDefinitionData.Scale0);
        }
    }
}

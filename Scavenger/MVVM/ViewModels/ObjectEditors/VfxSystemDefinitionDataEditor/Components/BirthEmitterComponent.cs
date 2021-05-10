using LeagueToolkit.Meta.Classes;
using Scavenger.MVVM.ViewModels.Meta;
using Scavenger.MVVM.ViewModels.PrimitiveStructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels.ObjectEditors.VfxSystemDefinitionDataEditor.Components
{
    public class BirthEmitterComponent : EmitterComponent
    {
        public override string Name => "Birth";

        // ----------- PROPERTIES ----------- \\

        public ValueFloatViewModel BirthFrameRate
        {
            get => this._birthFrameRate;
            set
            {
                this._birthFrameRate = value;
                NotifyPropertyChanged();
            }
        }

        public MetaStructureViewModel<FlexValueVector3ViewModel> FlexBirthTranslation
        {
            get => this._flexBirthTranslation;
            set
            {
                this._flexBirthTranslation = value;
                NotifyPropertyChanged();
            }
        }

        public ValueVector3ViewModel BirthRotation0
        {
            get => this._birthRotation0;
            set
            {
                this._birthRotation0 = value;
                NotifyPropertyChanged();
            }
        }
        public ValueFloatViewModel BirthRotation1
        {
            get => this._birthRotation1;
            set
            {
                this._birthRotation1 = value;
                NotifyPropertyChanged();
            }
        }

        public ValueVector3ViewModel BirthVelocity
        {
            get => this._birthVelocity;
            set
            {
                this._birthVelocity = value;
                NotifyPropertyChanged();
            }
        }
        public MetaStructureViewModel<FlexValueVector3ViewModel> FlexBirthVelocity
        {
            get => this._flexBirthVelocity;
            set
            {
                this._flexBirthVelocity = value;
                NotifyPropertyChanged();
            }
        }

        public ValueVector3ViewModel BirthOrbitalVelocity
        {
            get => this._birthOrbitalVelocity;
            set
            {
                this._birthOrbitalVelocity = value;
                NotifyPropertyChanged();
            }
        }

        public ValueVector3ViewModel BirthRotationalVelocity0
        {
            get => this._birthRotationalVelocity0;
            set
            {
                this._birthRotationalVelocity0 = value;
                NotifyPropertyChanged();
            }
        }
        public MetaStructureViewModel<FlexValueVector3ViewModel> FlexBirthRotationalVelocity0
        {
            get => this._flexBirthRotationalVelocity0;
            set
            {
                this._flexBirthRotationalVelocity0 = value;
                NotifyPropertyChanged();
            }
        }
        public ValueFloatViewModel BirthRotationalVelocity1
        {
            get => this._birthRotationalVelocity1;
            set
            {
                this._birthRotationalVelocity1 = value;
                NotifyPropertyChanged();
            }
        }
        public MetaStructureViewModel<FlexValueFloatViewModel> FlexBirthRotationalVelocity1
        {
            get => this._flexBirthRotationalVelocity1;
            set
            {
                this._flexBirthRotationalVelocity1 = value;
                NotifyPropertyChanged();
            }
        }

        public ValueVector3ViewModel BirthAcceleration
        {
            get => this._birthAcceleration;
            set
            {
                this._birthAcceleration = value;
                NotifyPropertyChanged();
            }
        }

        public ValueVector3ViewModel BirthRotationalAcceleration
        {
            get => this._birthRotationalAcceleration;
            set
            {
                this._birthRotationalAcceleration = value;
                NotifyPropertyChanged();
            }
        }

        public ValueVector2ViewModel BirthUvOffset
        {
            get => this._birthUvOffset;
            set
            {
                this._birthUvOffset = value;
                NotifyPropertyChanged();
            }
        }
        public MetaStructureViewModel<FlexValueVector2ViewModel> FlexBirthUvOffset
        {
            get => this._flexBirthUvOffset;
            set
            {
                this._flexBirthUvOffset = value;
                NotifyPropertyChanged();
            }
        }
        public ValueVector2ViewModel BirthUvOffsetMult
        {
            get => this._birthUvOffsetMult;
            set
            {
                this._birthUvOffsetMult = value;
                NotifyPropertyChanged();
            }
        }

        public ValueFloatViewModel BirthUvRotateRate
        {
            get => this._birthUvRotateRate;
            set
            {
                this._birthUvRotateRate = value;
                NotifyPropertyChanged();
            }
        }
        public ValueFloatViewModel BirthUvRotateRateMult
        {
            get => this._birthUvRotateRateMult;
            set
            {
                this._birthUvRotateRateMult = value;
                NotifyPropertyChanged();
            }
        }

        public ValueVector2ViewModel BirthUvScrollRate
        {
            get => this._birthUvScrollRate;
            set
            {
                this._birthUvScrollRate = value;
                NotifyPropertyChanged();
            }
        }
        public MetaStructureViewModel<FlexValueVector2ViewModel> FlexBirthUvScrollRate
        {
            get => this._flexBirthUvScrollRate;
            set
            {
                this._flexBirthUvScrollRate = value;
                NotifyPropertyChanged();
            }
        }
        public ValueVector2ViewModel BirthUvScrollRateMult
        {
            get => this._birthUvScrollRateMult;
            set
            {
                this._birthUvScrollRateMult = value;
                NotifyPropertyChanged();
            }
        }
        public MetaStructureViewModel<FlexValueVector2ViewModel> FlexBirthUvScrollRateMult
        {
            get => this._flexBirthUvScrollRateMult;
            set
            {
                this._flexBirthUvScrollRateMult = value;
                NotifyPropertyChanged();
            }
        }

        public ValueVector3ViewModel BirthDrag
        {
            get => this._birthDrag;
            set
            {
                this._birthDrag = value;
                NotifyPropertyChanged();
            }
        }

        // ----------- FIELDS ----------- \\

        private ValueFloatViewModel _birthFrameRate;

        private MetaStructureViewModel<FlexValueVector3ViewModel> _flexBirthTranslation;

        private ValueVector3ViewModel _birthRotation0;
        private ValueFloatViewModel _birthRotation1;

        private ValueVector3ViewModel _birthVelocity;
        private MetaStructureViewModel<FlexValueVector3ViewModel> _flexBirthVelocity;

        private ValueVector3ViewModel _birthOrbitalVelocity;

        private ValueVector3ViewModel _birthRotationalVelocity0;
        private MetaStructureViewModel<FlexValueVector3ViewModel> _flexBirthRotationalVelocity0;
        private ValueFloatViewModel _birthRotationalVelocity1;
        private MetaStructureViewModel<FlexValueFloatViewModel> _flexBirthRotationalVelocity1;

        private ValueVector3ViewModel _birthAcceleration;

        private ValueVector3ViewModel _birthRotationalAcceleration;

        private ValueVector2ViewModel _birthUvOffset;
        private MetaStructureViewModel<FlexValueVector2ViewModel> _flexBirthUvOffset;
        private ValueVector2ViewModel _birthUvOffsetMult;

        private ValueFloatViewModel _birthUvRotateRate;
        private ValueFloatViewModel _birthUvRotateRateMult;

        private ValueVector2ViewModel _birthUvScrollRate;
        private MetaStructureViewModel<FlexValueVector2ViewModel> _flexBirthUvScrollRate;
        private ValueVector2ViewModel _birthUvScrollRateMult;
        private MetaStructureViewModel<FlexValueVector2ViewModel> _flexBirthUvScrollRateMult;

        private ValueVector3ViewModel _birthDrag;

        public BirthEmitterComponent(VfxSystemDefinitionDataViewModel system, VfxEmitterDefinitionData emitter) : base(system)
        {
            this.BirthFrameRate = new ValueFloatViewModel(emitter.BirthFrameRate);

            this.FlexBirthTranslation = emitter.FlexBirthTranslation is null ? new () : new(new(emitter.FlexBirthTranslation));

            this.BirthRotation0 = new ValueVector3ViewModel(emitter.BirthRotation0);
            this.BirthRotation1 = new ValueFloatViewModel(emitter.BirthRotation1);

            this.BirthVelocity = new ValueVector3ViewModel(emitter.BirthVelocity);
            this.FlexBirthVelocity = emitter.FlexBirthVelocity is null ? new() : new(new(emitter.FlexBirthVelocity));

            this.BirthOrbitalVelocity = new ValueVector3ViewModel(emitter.BirthOrbitalVelocity);

            this.BirthRotationalVelocity0 = new ValueVector3ViewModel(emitter.BirthRotationalVelocity0);
            this.FlexBirthRotationalVelocity0 = emitter.FlexBirthRotationalVelocity0 is null ? new() : new(new(emitter.FlexBirthRotationalVelocity0));
            this.BirthRotationalVelocity1 = new ValueFloatViewModel(emitter.BirthRotationalVelocity1);
            this.FlexBirthRotationalVelocity1 = emitter.FlexBirthRotationalVelocity1 is null ? new() : new(new(emitter.FlexBirthRotationalVelocity1));

            this.BirthAcceleration = new ValueVector3ViewModel(emitter.BirthAcceleration);

            this.BirthRotationalAcceleration = new ValueVector3ViewModel(emitter.BirthRotationalAcceleration);

            this.BirthUvOffset = new ValueVector2ViewModel(emitter.BirthUVOffset);
            this.FlexBirthUvOffset = emitter.FlexBirthUVOffset is null ? new() : new(new(emitter.FlexBirthUVOffset));
            this.BirthUvOffsetMult = new ValueVector2ViewModel(emitter.BirthUVOffsetMult);

            this.BirthUvRotateRate = new ValueFloatViewModel(emitter.BirthUvRotateRate);
            this.BirthUvRotateRateMult = new ValueFloatViewModel(emitter.BirthUvRotateRateMult);

            this.BirthUvScrollRate = new ValueVector2ViewModel(emitter.BirthUvScrollRate);
            this.FlexBirthUvScrollRate = emitter.FlexBirthUVScrollRate is null ? new () : new(new(emitter.FlexBirthUVScrollRate));
            this.BirthUvScrollRateMult = new ValueVector2ViewModel(emitter.BirthUvScrollRateMult);
            this.FlexBirthUvScrollRateMult = emitter.FlexBirthUVScrollRateMult is null ? new() : new(new(emitter.FlexBirthUVScrollRateMult));
        }
    }
}

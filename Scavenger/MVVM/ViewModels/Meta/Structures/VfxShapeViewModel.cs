using LeagueToolkit.Meta;
using LeagueToolkit.Meta.Classes;
using Scavenger.MVVM.ViewModels.PrimitiveStructures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Numerics;

namespace Scavenger.MVVM.ViewModels.Meta.Structures
{
    public class VfxShapeViewModel : PropertyNotifier
    {
        public ValueVector3ViewModel BirthTranslation
        {
            get => this._birthTranslation;
            set
            {
                this._birthTranslation = value;
                NotifyPropertyChanged();
            }
        }
        public ValueVector3ViewModel EmitOffset
        {
            get => this._emitOffset;
            set
            {
                this._emitOffset = value;
                NotifyPropertyChanged();
            }
        }

        public MetaContainerViewModel<ValueFloatViewModel> EmitRotationAngles
        {
            get => this._emitRotationAngles;
            set
            {
                this._emitRotationAngles = value;
                NotifyPropertyChanged();
            }
        }
        public MetaContainerViewModel<Vector3ViewModel> EmitRotationAxes
        {
            get => this._emitRotationAxes;
            set
            {
                this._emitRotationAxes = value;
                NotifyPropertyChanged();
            }
        }

        private ValueVector3ViewModel _birthTranslation;
        private ValueVector3ViewModel _emitOffset;

        private MetaContainerViewModel<ValueFloatViewModel> _emitRotationAngles;
        private MetaContainerViewModel<Vector3ViewModel> _emitRotationAxes;

        public VfxShapeViewModel(VfxShape shape)
        {
            this.BirthTranslation = new ValueVector3ViewModel(shape.BirthTranslation);
            this.EmitOffset = new ValueVector3ViewModel(shape.EmitOffset);

            this.EmitRotationAngles = new MetaContainerViewModel<ValueFloatViewModel>(shape.EmitRotationAngles.Select(x => new ValueFloatViewModel(x)), () => new());
            this.EmitRotationAxes = new MetaContainerViewModel<Vector3ViewModel>(shape.EmitRotationAxes.Select(x => new Vector3ViewModel(x)), () => new());
        }

        public VfxShape ToVfxShape()
        {
            return new VfxShape()
            {
                BirthTranslation = new MetaEmbedded<ValueVector3>(this.BirthTranslation.ToValueVector3()),
                EmitOffset = new MetaEmbedded<ValueVector3>(this.EmitOffset.ToValueVector3()),

                EmitRotationAngles = new MetaContainer<MetaEmbedded<ValueFloat>>(this.EmitRotationAngles.ToContainer(x => new MetaEmbedded<ValueFloat>(x.ToValueFloat()))),
                EmitRotationAxes = new MetaContainer<Vector3>(this.EmitRotationAxes.ToContainer(x => x.ToVector()))
            };
        }
    }
}

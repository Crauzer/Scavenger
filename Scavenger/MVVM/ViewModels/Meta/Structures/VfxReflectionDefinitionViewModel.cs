using LeagueToolkit.Meta.Classes;
using Scavenger.MVVM.ViewModels.PrimitiveStructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels.Meta.Structures
{
    public class VfxReflectionDefinitionViewModel : PropertyNotifier
    {
        public float Fresnel
        {
            get => this._fresnel;
            set
            {
                this._fresnel = value;
                NotifyPropertyChanged();
            }
        }
        public ColorViewModel FresnelColor
        {
            get => this._fresnelColor;
            set
            {
                this._fresnelColor = value;
                NotifyPropertyChanged();
            }
        }

        public float ReflectionFresnel
        {
            get => this._reflectionFresnel;
            set
            {
                this._reflectionFresnel = value;
                NotifyPropertyChanged();
            }
        }
        public ColorViewModel ReflectionFresnelColor
        {
            get => this._reflectionFresnelColor;
            set
            {
                this._reflectionFresnelColor = value;
                NotifyPropertyChanged();
            }
        }

        public string ReflectionMapTexture
        {
            get => this._reflectionMapTexture;
            set
            {
                this._reflectionMapTexture = value;
                NotifyPropertyChanged();
            }
        }

        public float ReflectionOpacityDirect
        {
            get => this._reflectionOpacityDirect;
            set
            {
                this._reflectionOpacityDirect = value;
                NotifyPropertyChanged();
            }
        }
        public float ReflectionOpacityGlancing
        {
            get => this._reflectionOpacityGlancing;
            set
            {
                this._reflectionOpacityGlancing = value;
                NotifyPropertyChanged();
            }
        }

        private float _fresnel;
        private ColorViewModel _fresnelColor;

        private float _reflectionFresnel;
        private ColorViewModel _reflectionFresnelColor;

        private string _reflectionMapTexture;

        private float _reflectionOpacityDirect;
        private float _reflectionOpacityGlancing;

        public VfxReflectionDefinitionViewModel(VfxReflectionDefinitionData reflectionDefinition)
        {
            this.Fresnel = reflectionDefinition.Fresnel;
            this.FresnelColor = new ColorViewModel(reflectionDefinition.FresnelColor);

            this.ReflectionFresnel = reflectionDefinition.ReflectionFresnel;
            this.ReflectionFresnelColor = new ColorViewModel(reflectionDefinition.ReflectionFresnelColor);

            this.ReflectionMapTexture = reflectionDefinition.ReflectionMapTexture;

            this.ReflectionOpacityDirect = reflectionDefinition.ReflectionOpacityDirect;
            this.ReflectionOpacityGlancing = reflectionDefinition.ReflectionOpacityGlancing;
        }

        public VfxReflectionDefinitionData ToVfxReflectionDefinitionData()
        {
            return new VfxReflectionDefinitionData()
            {
                Fresnel = this.Fresnel,
                FresnelColor = this.FresnelColor.ToVector4(),

                ReflectionFresnel = this.ReflectionFresnel,
                ReflectionFresnelColor = this.ReflectionFresnelColor.ToVector4(),

                ReflectionMapTexture = this.ReflectionMapTexture,

                ReflectionOpacityDirect = this.ReflectionOpacityDirect,
                ReflectionOpacityGlancing = this.ReflectionOpacityGlancing
            };
        }
    }
}

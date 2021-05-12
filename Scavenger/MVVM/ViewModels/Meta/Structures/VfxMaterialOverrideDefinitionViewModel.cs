using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.Meta;
using LeagueToolkit.Meta.Classes;
using Scavenger.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels.Meta.Structures
{
    public class VfxMaterialOverrideDefinitionViewModel : PropertyNotifier
    {
        public string Material
        {
            get => this._material;
            set
            {
                this._material = value;
                NotifyPropertyChanged();
            }
        }
        public MetaOptionalViewModel<string> SubmeshName
        {
            get => this._submeshName;
            set
            {
                this._submeshName = value;
                NotifyPropertyChanged();
            }
        }

        public int Priority
        {
            get => this._priority;
            set
            {
                this._priority = value;
                NotifyPropertyChanged();
            }
        }
        public uint OverrideBlendMode
        {
            get => this._overrideBlendMode;
            set
            {
                this._overrideBlendMode = value;
                NotifyPropertyChanged();
            }
        }

        public string BaseTexture
        {
            get => this._baseTexture;
            set
            {
                this._baseTexture = value;
                NotifyPropertyChanged();
            }
        }
        public string GlossTexture
        {
            get => this._glossTexture;
            set
            {
                this._glossTexture = value;
                NotifyPropertyChanged();
            }
        }

        public float TransitionSample
        {
            get => this._transitionSample;
            set
            {
                this._transitionSample = value;
                NotifyPropertyChanged();
            }
        }
        public uint TransitionSource
        {
            get => this._transitionSource;
            set
            {
                this._transitionSource = value;
                NotifyPropertyChanged();
            }
        }
        public string TransitionTexture
        {
            get => this._transitionTexture;
            set
            {
                this._transitionTexture = value;
                NotifyPropertyChanged();
            }
        }

        private string _material;
        private MetaOptionalViewModel<string> _submeshName;

        private int _priority;
        private uint _overrideBlendMode;

        private string _baseTexture;
        private string _glossTexture;

        private float _transitionSample;
        private uint _transitionSource;
        private string _transitionTexture;

        public VfxMaterialOverrideDefinitionViewModel(VfxMaterialOverrideDefinitionData materialOverride)
        {
            this.Material = Hashtables.GetObject(materialOverride.Material);
            this.SubmeshName = new MetaOptionalViewModel<string>(materialOverride.SubMeshName.Value ?? string.Empty);

            this.Priority = materialOverride.Priority;
            this.OverrideBlendMode = materialOverride.OverrideBlendMode;

            this.BaseTexture = materialOverride.BaseTexture;
            this.GlossTexture = materialOverride.GlossTexture;

            this.TransitionSample = materialOverride.TransitionSample;
            this.TransitionSource = materialOverride.TransitionSource;
            this.TransitionTexture = materialOverride.TransitionTexture;
        }

        public VfxMaterialOverrideDefinitionData ToVfxMaterialOverrideDefinitionData()
        {
            return new VfxMaterialOverrideDefinitionData()
            {
                Material = new MetaObjectLink(Fnv1a.HashLower(this.Material)),
                SubMeshName = this.SubmeshName.ToMetaOptional(),

                Priority = this.Priority,
                OverrideBlendMode = this.OverrideBlendMode,

                BaseTexture = this.BaseTexture,
                GlossTexture = this.GlossTexture,

                TransitionSample = this.TransitionSample,
                TransitionSource = this.TransitionSource,
                TransitionTexture = this.TransitionTexture
            };
        }
    }
}

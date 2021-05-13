using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.Meta;
using LeagueToolkit.Meta.Classes;
using Scavenger.MVVM.ViewModels.PrimitiveStructures;
using Scavenger.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scavenger.MVVM.ViewModels.Meta.Structures
{
    public class VfxChildParticleSetViewModel : PropertyNotifier
    {
        public bool ChildEmitOnDeath
        {
            get => this._childEmitOnDeath;
            set
            {
                this._childEmitOnDeath = value;
                NotifyPropertyChanged();
            }
        }
        public ValueFloatViewModel ChildrenProbability
        {
            get => this._childrenProbability;
            set
            {
                this._childrenProbability = value;
                NotifyPropertyChanged();
            }
        }
        public MetaContainerViewModel<VfxChildParticleViewModel> Children
        {
            get => this._children;
            set
            {
                this._children = value;
                NotifyPropertyChanged();
            }
        }

        private bool _childEmitOnDeath;
        private ValueFloatViewModel _childrenProbability;
        private MetaContainerViewModel<VfxChildParticleViewModel> _children = new(() => new());

        public VfxChildParticleSetViewModel(VfxChildParticleSetDefinitionData childParticleSet)
        {
            this.ChildEmitOnDeath = childParticleSet.ChildEmitOnDeath;
            this.ChildrenProbability = new ValueFloatViewModel(childParticleSet.ChildrenProbability);

            if (childParticleSet.ChildrenIdentifiers.Count != childParticleSet.BoneToSpawnAt.Count)
            {
                throw new InvalidOperationException("Children Identifer count does not match Bone To Spawn At count");
            }

            for (int i = 0; i < childParticleSet.ChildrenIdentifiers.Count; i++)
            {
                this.Children.Items.Add(new VfxChildParticleViewModel(childParticleSet.BoneToSpawnAt[i], childParticleSet.ChildrenIdentifiers[i]));
            }
        }

        public VfxChildParticleSetDefinitionData ToVfxChildParticleSetDefinitionData()
        {
            return new VfxChildParticleSetDefinitionData()
            {
                ChildEmitOnDeath = this.ChildEmitOnDeath,
                ChildrenProbability = new MetaEmbedded<ValueFloat>(this.ChildrenProbability.ToValueFloat()),

                BoneToSpawnAt = new(this.Children.Items.Select(x => x.BoneToSpawnAt).ToList()),
                ChildrenIdentifiers = new(this.Children.Items.Select(x => new MetaEmbedded<VfxChildIdentifier>(x.Identifier.ToVfxChildIdentifier())).ToList())
            };
        }
    }

    public class VfxChildParticleViewModel : PropertyNotifier
    {
        public string BoneToSpawnAt
        {
            get => this._boneToSpawnAt;
            set
            {
                this._boneToSpawnAt = value;
                NotifyPropertyChanged();
            }
        }
        public VfxChildIdentifierViewModel Identifier
        {
            get => this._identifier;
            set
            {
                this._identifier = value;
                NotifyPropertyChanged();
            }
        }

        private string _boneToSpawnAt;
        private VfxChildIdentifierViewModel _identifier;

        public VfxChildParticleViewModel()
        {
            this.BoneToSpawnAt = string.Empty;
            this.Identifier = new VfxChildIdentifierViewModel(new VfxChildIdentifier());
        }
        public VfxChildParticleViewModel(string boneToSpawnAt, VfxChildIdentifier identifier)
        {
            this.BoneToSpawnAt = boneToSpawnAt;
            this.Identifier = new VfxChildIdentifierViewModel(identifier);
        }
    }

    public class VfxChildIdentifierViewModel : PropertyNotifier
    {
        public string EffectName
        {
            get => this._effectName;
            set
            {
                this._effectName = value;
                NotifyPropertyChanged();
            }
        }
        public string Effect
        {
            get => this._effect;
            set
            {
                this._effect = value;
                NotifyPropertyChanged();
            }
        }
        public string EffectKey
        {
            get => this._effectKey;
            set
            {
                this._effectKey = value;
                NotifyPropertyChanged();
            }
        }

        private string _effectName;
        private string _effect;
        private string _effectKey;

        public VfxChildIdentifierViewModel(VfxChildIdentifier identifier)
        {
            this.EffectName = identifier.EffectName;
            this.Effect = Hashtables.GetObject(identifier.Effect);
            this.EffectKey = Hashtables.GetHash(identifier.EffectKey);
        }

        public VfxChildIdentifier ToVfxChildIdentifier()
        {
            return new VfxChildIdentifier()
            {
                EffectName = this.EffectName,
                Effect = new MetaObjectLink(this.Effect.StartsWith("0x") ? Convert.ToUInt32(this.Effect, 16) : Fnv1a.HashLower(this.Effect)),
                EffectKey = new MetaHash(this.EffectKey.StartsWith("0x") ? Convert.ToUInt32(this.EffectKey, 16) : Fnv1a.HashLower(this.EffectKey)),
            };
        }
    }
}

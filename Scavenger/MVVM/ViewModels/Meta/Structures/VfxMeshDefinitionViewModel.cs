using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.Meta;
using LeagueToolkit.Meta.Classes;
using Scavenger.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Scavenger.MVVM.ViewModels.Meta.Structures
{
    public class VfxMeshDefinitionViewModel : PropertyNotifier
    {
        public bool LockMeshToAttachmanet
        {
            get => this._lockMeshToAttachment;
            set
            {
                this._lockMeshToAttachment = value;
                NotifyPropertyChanged();
            }
        }

        public string MeshName
        {
            get => this._meshName;
            set
            {
                this._meshName = value;
                NotifyPropertyChanged();
            }
        }
        public string SimpleMeshName
        {
            get => this._simpleMeshName;
            set
            {
                this._simpleMeshName = value;
                NotifyPropertyChanged();
            }
        }
        public string MeshSkeletonName
        {
            get => this._meshSkeletonName;
            set
            {
                this._meshSkeletonName = value;
                NotifyPropertyChanged();
            }
        }
        public string AnimationName
        {
            get => this._animationName;
            set
            {
                this._animationName = value;
                NotifyPropertyChanged();
            }
        }

        public MetaContainerViewModel<string> SubmeshesToDraw
        {
            get => this._submeshesToDraw;
            set
            {
                this._submeshesToDraw = value;
                NotifyPropertyChanged();
            }
        }
        public MetaContainerViewModel<string> SubmeshesToDrawAlways
        {
            get => this._submeshesToDrawAlways;
            set
            {
                this._submeshesToDrawAlways = value;
                NotifyPropertyChanged();
            }
        }

        public MetaContainerViewModel<string> AnimationVariants
        {
            get => this._animationVariants;
            set
            {
                this._animationVariants = value;
                NotifyPropertyChanged();
            }
        }

        private bool _lockMeshToAttachment;

        private string _meshName;
        private string _simpleMeshName;
        private string _meshSkeletonName;
        private string _animationName;

        private MetaContainerViewModel<string> _submeshesToDraw;
        private MetaContainerViewModel<string> _submeshesToDrawAlways;

        private MetaContainerViewModel<string> _animationVariants;

        public VfxMeshDefinitionViewModel() : this(new VfxMeshDefinitionData()) { }
        public VfxMeshDefinitionViewModel(VfxMeshDefinitionData meshDefinition)
        {
            this.LockMeshToAttachmanet = meshDefinition.LockMeshToAttachment;
            
            this.MeshName = meshDefinition.MeshName;
            this.SimpleMeshName = meshDefinition.SimpleMeshName;
            this.MeshSkeletonName = meshDefinition.MeshSkeletonName;
            this.AnimationName = meshDefinition.AnimationName;

            this.SubmeshesToDraw = new MetaContainerViewModel<string>(meshDefinition.SubmeshesToDraw.Select(x => Hashtables.GetHash(x)), () => string.Empty);
            this.SubmeshesToDrawAlways = new MetaContainerViewModel<string>(meshDefinition.SubmeshesToDrawAlways.Select(x => Hashtables.GetHash(x)), () => string.Empty);

            this.AnimationVariants = new MetaContainerViewModel<string>(meshDefinition.AnimationVariants, () => string.Empty);
        }

        public VfxMeshDefinitionData ToVfxMeshDefinitionData()
        {
            return new VfxMeshDefinitionData()
            {
                LockMeshToAttachment = this.LockMeshToAttachmanet,

                MeshName = this.MeshName,
                SimpleMeshName = this.SimpleMeshName,
                MeshSkeletonName = this.MeshSkeletonName,
                AnimationName = this.AnimationName,

                SubmeshesToDraw = this.SubmeshesToDraw.ToContainer(item => new MetaHash(Fnv1a.HashLower(item))),
                SubmeshesToDrawAlways = this.SubmeshesToDrawAlways.ToContainer(item => new MetaHash(Fnv1a.HashLower(item))),
            
                AnimationVariants = this.AnimationVariants.ToContainer()
            };
        }
    }
}

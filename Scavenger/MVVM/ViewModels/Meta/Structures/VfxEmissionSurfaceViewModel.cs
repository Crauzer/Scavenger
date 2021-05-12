using LeagueToolkit.Meta.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels.Meta.Structures
{
    public class VfxEmissionSurfaceViewModel : PropertyNotifier
    {
        public bool UseAvatarPose
        {
            get => this._useAvatarPose;
            set
            {
                this._useAvatarPose = value;
                NotifyPropertyChanged();
            }
        }
        public bool UseSurfaceNormalForBirthPhysics
        {
            get => this._useSurfaceNormalForBirthPhysicsl;
            set
            {
                this._useSurfaceNormalForBirthPhysicsl = value;
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
        public string SkeletonName
        {
            get => this._skeletonName;
            set
            {
                this._skeletonName = value;
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

        public float MeshScale
        {
            get => this._meshScale;
            set
            {
                this._meshScale = value;
                NotifyPropertyChanged();
            }
        }
        public ushort MaxJointWeights
        {
            get => this._maxJointWeights;
            set
            {
                this._maxJointWeights = value;
                NotifyPropertyChanged();
            }
        }

        private bool _useAvatarPose;
        private bool _useSurfaceNormalForBirthPhysicsl;

        private string _meshName;
        private string _skeletonName;
        private string _animationName;

        private float _meshScale;
        private ushort _maxJointWeights;

        public VfxEmissionSurfaceViewModel(VfxEmissionSurfaceData emissionSurfaceData)
        {
            this.UseAvatarPose = emissionSurfaceData.UseAvatarPose;
            this.UseSurfaceNormalForBirthPhysics = emissionSurfaceData.UseSurfaceNormalForBirthPhysics;

            this.MeshName = emissionSurfaceData.MeshName;
            this.SkeletonName = emissionSurfaceData.SkeletonName;
            this.AnimationName = emissionSurfaceData.AnimationName;

            this.MeshScale = emissionSurfaceData.MeshScale;
            this.MaxJointWeights = emissionSurfaceData.MaxJointWeights;
        }

        public VfxEmissionSurfaceData ToVfxEmissionSurfaceData()
        {
            return new VfxEmissionSurfaceData()
            {
                UseAvatarPose = this.UseAvatarPose,
                UseSurfaceNormalForBirthPhysics = this.UseSurfaceNormalForBirthPhysics,

                MeshName = this.MeshName,
                SkeletonName = this.SkeletonName,
                AnimationName = this.AnimationName,

                MeshScale = this.MeshScale,
                MaxJointWeights = this.MaxJointWeights
            };
        }
    }
}

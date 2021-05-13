using LeagueToolkit.Meta;
using LeagueToolkit.Meta.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels.Meta.Structures
{
    public abstract class VfxPrimitiveBaseViewModel : PropertyNotifier
    {
        public abstract VfxPrimitiveBase ToVfxPrimitiveBase();

        public static VfxPrimitiveBaseViewModel Build(VfxPrimitiveBase primitiveBase)
        {
            return primitiveBase switch
            {
                VfxPrimitiveArbitraryTrail primitive => new VfxPrimitiveArbitraryTrailViewModel(primitive),
                VfxPrimitiveCameraTrail primitive => new VfxPrimitiveCameraTrailViewModel(primitive),
                VfxPrimitiveArbitrarySegmentBeam primitive => new VfxPrimitiveArbitrarySegmentBeamViewModel(primitive),
                VfxPrimitiveCameraSegmentBeam primitive => new VfxPrimitiveCameraSegmentBeamViewModel(primitive),
                VfxPrimitivePlanarProjection primitive => new VfxPrimitivePlanarProjectionViewModel(primitive),
                VfxPrimitiveMesh primitive => new VfxPrimitiveMeshViewModel(primitive),
                VfxPrimitiveAttachedMesh primitive => new VfxPrimitiveAttachedMeshViewModel(primitive),
                VfxPrimitiveRay primitive => new VfxPrimitiveRayViewModel(primitive),
                VfxPrimitiveBeam primitive => new VfxPrimitiveBeamViewModel(primitive),
                VfxPrimitiveArbitraryQuad primitive => new VfxPrimitiveArbitraryQuadViewModel(primitive),
                VfxPrimitiveCameraQuad primitive => new VfxPrimitiveCameraQuadViewModel(primitive),
                _ => throw new NotImplementedException("Unknown Primitive Type")
            };
        }
    }

    public abstract class VfxPrimitiveTrailBaseViewModel : VfxPrimitiveBaseViewModel
    {
        public VfxTrailDefinitionViewModel Trail
        {
            get => this._trail;
            set
            {
                this._trail = value;
                NotifyPropertyChanged();
            }
        }

        private VfxTrailDefinitionViewModel _trail;
    }
    public class VfxPrimitiveArbitraryTrailViewModel : VfxPrimitiveTrailBaseViewModel
    {
        public VfxPrimitiveArbitraryTrailViewModel(VfxPrimitiveArbitraryTrail primitive)
        {
            this.Trail = new VfxTrailDefinitionViewModel(primitive.Trail);
        }

        public override VfxPrimitiveBase ToVfxPrimitiveBase()
        {
            return new VfxPrimitiveArbitraryTrail
            {
                Trail = new MetaEmbedded<VfxTrailDefinitionData>(this.Trail.ToVfxTrailDefinitionData())
            };
        }
    }
    public class VfxPrimitiveCameraTrailViewModel : VfxPrimitiveTrailBaseViewModel
    {
        public VfxPrimitiveCameraTrailViewModel(VfxPrimitiveCameraTrail primitive)
        {
            this.Trail = new VfxTrailDefinitionViewModel(primitive.Trail);
        }

        public override VfxPrimitiveBase ToVfxPrimitiveBase()
        {
            return new VfxPrimitiveCameraTrail
            {
                Trail = new MetaEmbedded<VfxTrailDefinitionData>(this.Trail.ToVfxTrailDefinitionData())
            };
        }
    }

    public abstract class VfxPrimitiveBeamBaseViewModel : VfxPrimitiveBaseViewModel
    {
        public VfxBeamDefinitionViewModel Beam
        {
            get => this._beam;
            set
            {
                this._beam = value;
                NotifyPropertyChanged();
            }
        }

        private VfxBeamDefinitionViewModel _beam;
    }
    public class VfxPrimitiveArbitrarySegmentBeamViewModel : VfxPrimitiveBeamBaseViewModel
    {
        public VfxPrimitiveArbitrarySegmentBeamViewModel(VfxPrimitiveArbitrarySegmentBeam primitive)
        {
            this.Beam = new VfxBeamDefinitionViewModel(primitive.Beam);
        }

        public override VfxPrimitiveBase ToVfxPrimitiveBase()
        {
            return new VfxPrimitiveArbitrarySegmentBeam
            {
                Beam = new MetaEmbedded<VfxBeamDefinitionData>(this.Beam.ToVfxBeamDefinitionData())
            };
        }
    }
    public class VfxPrimitiveCameraSegmentBeamViewModel : VfxPrimitiveBeamBaseViewModel
    {
        public VfxPrimitiveCameraSegmentBeamViewModel(VfxPrimitiveCameraSegmentBeam primitive)
        {
            this.Beam = new VfxBeamDefinitionViewModel(primitive.Beam);
        }

        public override VfxPrimitiveBase ToVfxPrimitiveBase()
        {
            return new VfxPrimitiveCameraSegmentBeam()
            {
                Beam = new MetaEmbedded<VfxBeamDefinitionData>(this.Beam.ToVfxBeamDefinitionData())
            };
        }
    }

    public abstract class VfxPrimitiveProjectionBaseViewModel : VfxPrimitiveBaseViewModel
    {
        public VfxProjectionDefinitionViewModel Projection
        {
            get => this._projection;
            set
            {
                this._projection = value;
                NotifyPropertyChanged();
            }
        }

        private VfxProjectionDefinitionViewModel _projection;
    }
    public class VfxPrimitivePlanarProjectionViewModel : VfxPrimitiveProjectionBaseViewModel
    {
        public VfxPrimitivePlanarProjectionViewModel(VfxPrimitivePlanarProjection primitive)
        {
            this.Projection = new VfxProjectionDefinitionViewModel(primitive.Projection);
        }

        public override VfxPrimitiveBase ToVfxPrimitiveBase()
        {
            return new VfxPrimitivePlanarProjection
            {
                Projection = new MetaEmbedded<VfxProjectionDefinitionData>(this.Projection.ToVfxProjectionDefinitionData())
            };
        }
    }

    public abstract class VfxPrimitiveMeshBaseViewModel : VfxPrimitiveBaseViewModel
    {
        public bool M3934657962
        {
            get => this._m3934657962;
            set
            {
                this._m3934657962 = value;
                NotifyPropertyChanged();
            }
        }
        public bool M4227234111
        {
            get => this._m4227234111;
            set
            {
                this._m4227234111 = value;
                NotifyPropertyChanged();
            }
        }
        public VfxMeshDefinitionViewModel Mesh
        {
            get => this._mesh;
            set
            {
                this._mesh = value;
                NotifyPropertyChanged();
            }
        }

        private bool _m3934657962;
        private bool _m4227234111;
        private VfxMeshDefinitionViewModel _mesh;
    }
    public class VfxPrimitiveMeshViewModel : VfxPrimitiveMeshBaseViewModel
    {
        public VfxPrimitiveMeshViewModel(VfxPrimitiveMesh primitive)
        {
            this.M3934657962 = primitive.m3934657962;
            this.M4227234111 = primitive.m4227234111;
            this.Mesh = new VfxMeshDefinitionViewModel(primitive.Mesh);
        }

        public override VfxPrimitiveBase ToVfxPrimitiveBase()
        {
            return new VfxPrimitiveMesh
            {
                m3934657962 = this.M3934657962,
                m4227234111 = this.M4227234111,
                Mesh = new MetaEmbedded<VfxMeshDefinitionData>(this.Mesh.ToVfxMeshDefinitionData())
            };
        }
    }
    public class VfxPrimitiveAttachedMeshViewModel : VfxPrimitiveMeshBaseViewModel
    {
        public VfxPrimitiveAttachedMeshViewModel(VfxPrimitiveAttachedMesh primitive)
        {
            this.M3934657962 = primitive.m3934657962;
            this.M4227234111 = primitive.m4227234111;
            this.Mesh = new VfxMeshDefinitionViewModel(primitive.Mesh);
        }

        public override VfxPrimitiveBase ToVfxPrimitiveBase()
        {
            return new VfxPrimitiveAttachedMesh()
            {
                m3934657962 = this.M3934657962,
                m4227234111 = this.M4227234111,
                Mesh = new MetaEmbedded<VfxMeshDefinitionData>(this.Mesh.ToVfxMeshDefinitionData())
            };
        }
    }

    public class VfxPrimitiveRayViewModel : VfxPrimitiveBaseViewModel
    {
        public VfxPrimitiveRayViewModel(VfxPrimitiveRay primitive) { }

        public override VfxPrimitiveBase ToVfxPrimitiveBase()
        {
            return new VfxPrimitiveRay();
        }
    }

    public class VfxPrimitiveBeamViewModel : VfxPrimitiveBaseViewModel
    {
        public VfxBeamDefinitionViewModel Beam
        {
            get => this._beam;
            set
            {
                this._beam = value;
                NotifyPropertyChanged();
            }
        }
        public VfxMeshDefinitionViewModel Mesh
        {
            get => this._mesh;
            set
            {
                this._mesh = value;
                NotifyPropertyChanged();
            }
        }

        private VfxBeamDefinitionViewModel _beam;
        private VfxMeshDefinitionViewModel _mesh;

        public VfxPrimitiveBeamViewModel(VfxPrimitiveBeam primitive)
        {
            this.Beam = new VfxBeamDefinitionViewModel(primitive.Beam);
            this.Mesh = new VfxMeshDefinitionViewModel(primitive.Mesh);
        }

        public override VfxPrimitiveBase ToVfxPrimitiveBase()
        {
            return new VfxPrimitiveBeam
            {
                Beam = new MetaEmbedded<VfxBeamDefinitionData>(this.Beam.ToVfxBeamDefinitionData()),
                Mesh = new MetaEmbedded<VfxMeshDefinitionData>(this.Mesh.ToVfxMeshDefinitionData())
            };
        }
    }

    public class VfxPrimitiveArbitraryQuadViewModel : VfxPrimitiveBaseViewModel
    {
        public VfxPrimitiveArbitraryQuadViewModel(VfxPrimitiveArbitraryQuad primitive) { }

        public override VfxPrimitiveBase ToVfxPrimitiveBase()
        {
            return new VfxPrimitiveArbitraryQuad();
        }
    }
    public class VfxPrimitiveCameraQuadViewModel : VfxPrimitiveBaseViewModel
    {
        public VfxPrimitiveCameraQuadViewModel(VfxPrimitiveCameraQuad primitive) { }

        public override VfxPrimitiveBase ToVfxPrimitiveBase()
        {
            return new VfxPrimitiveCameraQuad();
        }
    }

    public enum VfxPrimitiveType
    {
        ArbitraryTrail,
        CameraTrail,

        ArbitrarySegmentBeam,
        CameraSegmentBeam,

        PlanarProjection,

        Mesh,
        AttachedMesh,

        Ray,
        Beam,

        ArbitraryQuad,
        CameraQuad
    }
}

using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using Scavenger.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.Utilities
{
    public static class BinTreeUtilities
    {
        public static BinTreePropertyViewModel ConstructTreePropertyViewModel(BinTreeParentViewModel parent, BinTreeProperty genericProperty)
        {
            return genericProperty switch
            {
                BinTreeNone property => new BinTreePropertyViewModel(parent, property),
                BinTreeBool property => new BinTreeBoolViewModel(parent, property),
                BinTreeSByte property => new BinTreeSByteViewModel(parent, property),
                BinTreeByte property => new BinTreeByteViewModel(parent, property),
                BinTreeInt16 property => new BinTreeInt16ViewModel(parent, property),
                BinTreeUInt16 property => new BinTreeUInt16ViewModel(parent, property),
                BinTreeInt32 property => new BinTreeInt32ViewModel(parent, property),
                BinTreeUInt32 property => new BinTreeUInt32ViewModel(parent, property),
                BinTreeInt64 property => new BinTreeInt64ViewModel(parent, property),
                BinTreeUInt64 property => new BinTreeUInt64ViewModel(parent, property),
                BinTreeFloat property => new BinTreeFloatViewModel(parent, property),
                BinTreeVector2 property => new BinTreeVector2ViewModel(parent, property),
                BinTreeVector3 property => new BinTreeVector3ViewModel(parent, property),
                BinTreeVector4 property => new BinTreeVector4ViewModel(parent, property),
                //matrix
                BinTreeColor property => new BinTreeColorViewModel(parent, property),
                BinTreeHash property => new BinTreeHashViewModel(parent, property),
                BinTreeWadEntryLink property => new BinTreeWadEntryLinkViewModel(parent, property),
                BinTreeUnorderedContainer property => new BinTreeContainerViewModel(parent, property),
                BinTreeContainer property => new BinTreeContainerViewModel(parent, property),
                BinTreeString property => new BinTreeStringViewModel(parent, property),
                BinTreeEmbedded property => new BinTreeEmbeddedViewModel(parent, property),
                BinTreeStructure property => new BinTreeStructureViewModel(parent, property),
                BinTreeObjectLink property => new BinTreeObjectLinkViewModel(parent, property),
                BinTreeOptional property => new BinTreeOptionalViewModel(parent, property),
                BinTreeMap property => new BinTreeMapViewModel(parent, property),
                BinTreeBitBool property => new BinTreeBitBoolViewModel(parent, property),
                _ => new BinTreePropertyViewModel(parent, genericProperty),
            };
        }
    }
}

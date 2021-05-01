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
        public static BinTreePropertyViewModel ConstructTreePropertyViewModel(BinTreeProperty genericProperty)
        {
            return genericProperty switch
            {
                BinTreeNone property => new BinTreePropertyViewModel(property),
                BinTreeBool property => new BinTreeBoolViewModel(property),
                BinTreeSByte property => new BinTreeSByteViewModel(property),
                BinTreeByte property => new BinTreeByteViewModel(property),
                BinTreeInt16 property => new BinTreeInt16ViewModel(property),
                BinTreeUInt16 property => new BinTreeUInt16ViewModel(property),
                BinTreeInt32 property => new BinTreeInt32ViewModel(property),
                BinTreeUInt32 property => new BinTreeUInt32ViewModel(property),
                BinTreeInt64 property => new BinTreeInt64ViewModel(property),
                BinTreeUInt64 property => new BinTreeUInt64ViewModel(property),
                BinTreeFloat property => new BinTreeFloatViewModel(property),
                BinTreeVector2 property => new BinTreeVector2ViewModel(property),
                BinTreeVector3 property => new BinTreeVector3ViewModel(property),
                BinTreeVector4 property => new BinTreeVector4ViewModel(property),
                //matrix
                BinTreeColor property => new BinTreeColorViewModel(property),
                BinTreeHash property => new BinTreeHashViewModel(property),
                BinTreeWadEntryLink property => new BinTreeWadEntryLinkViewModel(property),
                BinTreeUnorderedContainer property => new BinTreeContainerViewModel(property),
                BinTreeContainer property => new BinTreeContainerViewModel(property),
                BinTreeString property => new BinTreeStringViewModel(property),
                BinTreeEmbedded property => new BinTreeEmbeddedViewModel(property),
                BinTreeStructure property => new BinTreeStructureViewModel(property),
                BinTreeObjectLink property => new BinTreeObjectLinkViewModel(property),
                BinTreeOptional property => new BinTreeOptionalViewModel(property),
                BinTreeMap property => new BinTreeMapViewModel(property),
                BinTreeBitBool property => new BinTreeBitBoolViewModel(property),
                _ => new BinTreePropertyViewModel(genericProperty),
            };
        }
    }
}

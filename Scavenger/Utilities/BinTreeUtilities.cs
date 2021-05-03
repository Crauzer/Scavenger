using LeagueToolkit.Helpers.Hashing;
using LeagueToolkit.Helpers.Structures;
using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using Scavenger.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
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
                BinTreeMatrix44 property => new BinTreeMatrix44ViewModel(parent, property),
                BinTreeColor property => new BinTreeColorViewModel(parent, property),
                BinTreeHash property => new BinTreeHashViewModel(parent, property),
                BinTreeWadEntryLink property => new BinTreeWadEntryLinkViewModel(parent, property),
                BinTreeUnorderedContainer property => new BinTreeUnorderedContainerViewModel(parent, property),
                BinTreeContainer property => new BinTreeContainerViewModel(parent, property),
                BinTreeString property => new BinTreeStringViewModel(parent, property),
                BinTreeEmbedded property => new BinTreeEmbeddedViewModel(parent, property),
                BinTreeStructure property => new BinTreeStructureViewModel(parent, property),
                BinTreeObjectLink property => new BinTreeObjectLinkViewModel(parent, property),
                BinTreeOptional property => new BinTreeOptionalViewModel(parent, property),
                BinTreeMap property => new BinTreeMapViewModel(parent, property),
                BinTreeBitBool property => new BinTreeBitBoolViewModel(parent, property),
                _ => null,
            };
        }

        public static BinTreeProperty BuildProperty(string name, string metaClass,
            IBinTreeParent parent, BinPropertyType propertyType, BinPropertyType primaryType, BinPropertyType secondaryType)
        {
            uint nameHash = Fnv1a.HashLower(name);
            uint metaClassHash = metaClass is not null ? Fnv1a.HashLower(metaClass) : 0;

            return propertyType switch
            {
                BinPropertyType.None => new BinTreeNone(parent, nameHash),
                BinPropertyType.Bool => new BinTreeBool(parent, nameHash, false),
                BinPropertyType.SByte => new BinTreeSByte(parent, nameHash, 0),
                BinPropertyType.Byte => new BinTreeByte(parent, nameHash, 0),
                BinPropertyType.Int16 => new BinTreeInt16(parent, nameHash, 0),
                BinPropertyType.UInt16 => new BinTreeUInt16(parent, nameHash, 0),
                BinPropertyType.Int32 => new BinTreeInt32(parent, nameHash, 0),
                BinPropertyType.UInt32 => new BinTreeUInt32(parent, nameHash, 0),
                BinPropertyType.Int64 => new BinTreeInt64(parent, nameHash, 0),
                BinPropertyType.UInt64 => new BinTreeUInt64(parent, nameHash, 0),
                BinPropertyType.Float => new BinTreeFloat(parent, nameHash, 0),
                BinPropertyType.Vector2 => new BinTreeVector2(parent, nameHash, new Vector2()),
                BinPropertyType.Vector3 => new BinTreeVector3(parent, nameHash, new Vector3()),
                BinPropertyType.Vector4 => new BinTreeVector4(parent, nameHash, new Vector4()),
                BinPropertyType.Matrix44 => new BinTreeMatrix44(parent, nameHash, new Matrix4x4()),
                BinPropertyType.Color => new BinTreeColor(parent, nameHash, new Color()),
                BinPropertyType.String => new BinTreeString(parent, nameHash, ""),
                BinPropertyType.Hash => new BinTreeHash(parent, nameHash, 0),
                BinPropertyType.WadEntryLink => new BinTreeWadEntryLink(parent, nameHash, 0),
                BinPropertyType.Container => new BinTreeContainer(parent, nameHash, primaryType, Enumerable.Empty<BinTreeProperty>()),
                BinPropertyType.UnorderedContainer => new BinTreeUnorderedContainer(parent, nameHash, primaryType, Enumerable.Empty<BinTreeProperty>()),
                BinPropertyType.Structure => new BinTreeStructure(parent, nameHash, metaClassHash, Enumerable.Empty<BinTreeProperty>()),
                BinPropertyType.Embedded => new BinTreeEmbedded(parent, nameHash, metaClassHash, Enumerable.Empty<BinTreeProperty>()),
                BinPropertyType.ObjectLink => new BinTreeObjectLink(parent, nameHash, 0),
                BinPropertyType.Optional => new BinTreeOptional(parent, nameHash, primaryType, BuildProperty("", "", null, primaryType, secondaryType, BinPropertyType.None)),
                BinPropertyType.Map => new BinTreeMap(parent, nameHash, primaryType, secondaryType, Enumerable.Empty<KeyValuePair<BinTreeProperty, BinTreeProperty>>()),
                BinPropertyType.BitBool => new BinTreeBitBool(parent, nameHash, 0),
                _ => new BinTreeNone(parent, nameHash),
            };
        }
    
        public static bool IsAsset(BinTreeStringViewModel treeString)
        {
            string extension = Path.GetExtension(treeString.Value);
            return string.IsNullOrEmpty(extension) is false;
        }
        public static bool IsPreviewableAsset(BinTreeStringViewModel treeString)
        {
            string extension = Path.GetExtension(treeString.Value);
            if (string.IsNullOrEmpty(extension) is false)
            {
                string binPath = treeString.Parent.BinTree.BinPath;
                
                int indexOfData = binPath.LastIndexOf("\\data\\");
                string binFolder = indexOfData == -1 ? Path.GetDirectoryName(binPath) : binPath.Remove(binPath.LastIndexOf("\\data\\"));
                string assetPath = Path.Combine(binFolder, treeString.Value);
                string assetExtension = Path.GetExtension(assetPath);

                return assetExtension switch
                {
                    ".skn" => true,
                    ".scb" => true,
                    ".sco" => true,
                    ".mapgeo" => true,
                    ".dds" => true,
                    _ => false
                };
            }

            return false;
        }
    }
}

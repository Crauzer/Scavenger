using LeagueToolkit.IO.PropertyBin;
using Newtonsoft.Json;
using Scavenger.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Scavenger.Utilities.Wrappers
{
    [Serializable]
    public class BinTreePropertyViewModelDataWrapper : ISerializable
    {
        public BinTreePropertyViewModel Property { get; set; }

        public BinTreePropertyViewModelDataWrapper(BinTreePropertyViewModel property)
        {
            this.Property = property;
        }
        protected BinTreePropertyViewModelDataWrapper(SerializationInfo info, StreamingContext context)
        {
            BinPropertyType propertyType = (BinPropertyType)Enum.Parse(typeof(BinPropertyType), info.GetString("property_type"));
            
            DeserializeProperty(info, propertyType);
        }

        private void DeserializeProperty(SerializationInfo info, BinPropertyType propertyType)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

            this.Property = propertyType switch
            {
                BinPropertyType.None => JsonConvert.DeserializeObject<BinTreePropertyViewModel>(info.GetString("property"), settings),
                BinPropertyType.Bool => JsonConvert.DeserializeObject<BinTreeBoolViewModel>(info.GetString("property"), settings),
                BinPropertyType.SByte => JsonConvert.DeserializeObject<BinTreeSByteViewModel>(info.GetString("property"), settings),
                BinPropertyType.Byte => JsonConvert.DeserializeObject<BinTreeByteViewModel>(info.GetString("property"), settings),
                BinPropertyType.Int16 => JsonConvert.DeserializeObject<BinTreeInt16ViewModel>(info.GetString("property"), settings),
                BinPropertyType.UInt16 => JsonConvert.DeserializeObject<BinTreeUInt16ViewModel>(info.GetString("property"), settings),
                BinPropertyType.Int32 => JsonConvert.DeserializeObject<BinTreeInt32ViewModel>(info.GetString("property"), settings),
                BinPropertyType.UInt32 => JsonConvert.DeserializeObject<BinTreeUInt32ViewModel>(info.GetString("property"), settings),
                BinPropertyType.Int64 => JsonConvert.DeserializeObject<BinTreeInt64ViewModel>(info.GetString("property"), settings),
                BinPropertyType.UInt64 => JsonConvert.DeserializeObject<BinTreeUInt64ViewModel>(info.GetString("property"), settings),
                BinPropertyType.Float => JsonConvert.DeserializeObject<BinTreeFloatViewModel>(info.GetString("property"), settings),
                BinPropertyType.Vector2 => JsonConvert.DeserializeObject<BinTreeVector2ViewModel>(info.GetString("property"), settings),
                BinPropertyType.Vector3 => JsonConvert.DeserializeObject<BinTreeVector3ViewModel>(info.GetString("property"), settings),
                BinPropertyType.Vector4 => JsonConvert.DeserializeObject<BinTreeVector4ViewModel>(info.GetString("property"), settings),
                BinPropertyType.Matrix44 => JsonConvert.DeserializeObject<BinTreeMatrix44ViewModel>(info.GetString("property"), settings),
                BinPropertyType.Color => JsonConvert.DeserializeObject<BinTreeColorViewModel>(info.GetString("property"), settings),
                BinPropertyType.String => JsonConvert.DeserializeObject<BinTreeStringViewModel>(info.GetString("property"), settings),
                BinPropertyType.Hash => JsonConvert.DeserializeObject<BinTreeHashViewModel>(info.GetString("property"), settings),
                BinPropertyType.WadEntryLink => JsonConvert.DeserializeObject<BinTreeWadEntryLinkViewModel>(info.GetString("property"), settings),
                BinPropertyType.Container => JsonConvert.DeserializeObject<BinTreeContainerViewModel>(info.GetString("property"), settings),
                BinPropertyType.UnorderedContainer => JsonConvert.DeserializeObject<BinTreeUnorderedContainerViewModel>(info.GetString("property"), settings),
                BinPropertyType.Structure => JsonConvert.DeserializeObject<BinTreeStructureViewModel>(info.GetString("property"), settings),
                BinPropertyType.Embedded => JsonConvert.DeserializeObject<BinTreeEmbeddedViewModel>(info.GetString("property"), settings),
                BinPropertyType.ObjectLink => JsonConvert.DeserializeObject<BinTreeObjectLinkViewModel>(info.GetString("property"), settings),
                BinPropertyType.Optional => JsonConvert.DeserializeObject<BinTreeOptionalViewModel>(info.GetString("property"), settings),
                BinPropertyType.Map => JsonConvert.DeserializeObject<BinTreeMapViewModel>(info.GetString("property"), settings),
                BinPropertyType.BitBool => JsonConvert.DeserializeObject<BinTreeBitBoolViewModel>(info.GetString("property"), settings),
                _ => throw new NotImplementedException(),
            };
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

            info.AddValue("property_type", this.Property.TreeProperty.Type.ToString());
            info.AddValue("property", JsonConvert.SerializeObject(this.Property, settings));
        }
    }
}

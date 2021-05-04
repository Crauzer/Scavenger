using Newtonsoft.Json;
using Scavenger.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Scavenger.Utilities.Wrappers
{
    [Serializable]
    public class BinTreeObjectViewModelDataWrapper : ISerializable
    {
        public BinTreeObjectViewModel Object { get; set; }

        public BinTreeObjectViewModelDataWrapper(BinTreeObjectViewModel objectViewModel)
        {
            this.Object = objectViewModel;
        }
        protected BinTreeObjectViewModelDataWrapper(SerializationInfo info, StreamingContext context)
        {
            JsonSerializerSettings settings = new() { TypeNameHandling = TypeNameHandling.All };

            this.Object = JsonConvert.DeserializeObject<BinTreeObjectViewModel>(info.GetString("object"), settings);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            JsonSerializerSettings settings = new() { TypeNameHandling = TypeNameHandling.All };

            info.AddValue("object", JsonConvert.SerializeObject(this.Object, settings));
        }
    }
}

using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.Meta;
using LeagueToolkit.Meta.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels.ObjectEditors
{
    public class VfxSystemDefinitionDataViewModel : ObjectEditorViewModel
    {
        private VfxSystemDefinitionData _vfxSystemDefinitionData;

        public VfxSystemDefinitionDataViewModel(MetaEnvironment metaEnvironment, BinTreeObject treeObject) : base(metaEnvironment, treeObject)
        {
            if(metaEnvironment.RegisteredObjects.ContainsKey(treeObject.PathHash))
            {
                metaEnvironment.DeregisterObject(treeObject.PathHash);
            }

            this._vfxSystemDefinitionData = MetaSerializer.Deserialize<VfxSystemDefinitionData>(metaEnvironment, treeObject);
        }
    }
}

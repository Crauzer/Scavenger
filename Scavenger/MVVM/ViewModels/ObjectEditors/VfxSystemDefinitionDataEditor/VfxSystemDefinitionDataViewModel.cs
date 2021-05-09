using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.Meta;
using LeagueToolkit.Meta.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Scavenger.MVVM.ViewModels.ObjectEditors.VfxSystemDefinitionDataEditor
{
    public class VfxSystemDefinitionDataViewModel : ObjectEditorViewModel
    {
        public string ParticleName
        {
            get => this._particleName;
            set
            {
                this._particleName = value;
                NotifyPropertyChanged();
            }
        }

        public VfxEmitterDefinitionDataViewModel SelectedComplexEmitter
        {
            get => this._selectedComplexEmitter;
            set
            {
                this._selectedComplexEmitter = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<VfxEmitterDefinitionDataViewModel> ComplexEmitters
        {
            get => this._complexEmitters;
            set
            {
                this._complexEmitters = value;
                NotifyPropertyChanged();
            }
        }

        private string _particleName;

        private VfxEmitterDefinitionDataViewModel _selectedComplexEmitter;
        private ObservableCollection<VfxEmitterDefinitionDataViewModel> _complexEmitters = new();

        private VfxSystemDefinitionData _vfxSystemDefinitionData;

        public VfxSystemDefinitionDataViewModel(MetaEnvironment metaEnvironment, BinTreeObject treeObject, string binPath) : base(metaEnvironment, treeObject, binPath)
        {
            if(metaEnvironment.RegisteredObjects.ContainsKey(treeObject.PathHash))
            {
                metaEnvironment.DeregisterObject(treeObject.PathHash);
            }

            this._vfxSystemDefinitionData = MetaSerializer.Deserialize<VfxSystemDefinitionData>(metaEnvironment, treeObject);

            this.ParticleName = this._vfxSystemDefinitionData.ParticleName;

            foreach(VfxEmitterDefinitionData emitter in this._vfxSystemDefinitionData.ComplexEmitterDefinitionData)
            {
                this.ComplexEmitters.Add(new VfxEmitterDefinitionDataViewModel(this, emitter));
            }
        }
    }
}

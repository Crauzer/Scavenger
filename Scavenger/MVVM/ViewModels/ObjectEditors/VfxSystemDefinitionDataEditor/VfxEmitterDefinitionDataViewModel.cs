using CSharpImageLibrary;
using LeagueToolkit.Meta.Classes;
using Scavenger.MVVM.Commands;
using Scavenger.MVVM.ViewModels.ObjectEditors.VfxSystemDefinitionDataEditor.Components;
using Scavenger.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Scavenger.MVVM.ViewModels.ObjectEditors.VfxSystemDefinitionDataEditor
{
    public class VfxEmitterDefinitionDataViewModel : PropertyNotifier
    {
        public VfxSystemDefinitionDataViewModel System { get; }

        public BitmapSource PreviewImage
        {
            get => this._previewImage;
            set
            {
                this._previewImage = value;
                NotifyPropertyChanged();
            }
        }
        public string EmitterName
        {
            get => this._emitterName;
            set
            {
                this._emitterName = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsEnabled
        {
            get => !this._isDisabled;
            set
            {
                this._isDisabled = !value;
                NotifyPropertyChanged();
            }
        }
        public sbyte Importance
        {
            get => (sbyte)this._importance;
            set
            {
                this._importance = (byte)value;
            }
        }

        public EmitterComponent SelectedComponent
        {
            get => this._selectedComponent;
            set
            {
                this._selectedComponent = value;

                this.System.SelectedComplexEmitter = this;

                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<EmitterComponent> Components
        {
            get => this._components;
            set
            {
                this._components = value;
                NotifyPropertyChanged();
            }
        }

        private BitmapSource _previewImage;
        private string _emitterName;
        private bool _isDisabled;
        private byte _importance;

        private EmitterComponent _selectedComponent;
        private ObservableCollection<EmitterComponent> _components = new();

        public ICommand AddComponentCommand => new RelayCommand(OnAddComponent);

        public VfxEmitterDefinitionDataViewModel(VfxSystemDefinitionDataViewModel system, VfxEmitterDefinitionData vfxEmitterDefinitionData)
        {
            this.System = system;
            this.EmitterName = vfxEmitterDefinitionData.EmitterName;
            this.IsEnabled = !vfxEmitterDefinitionData.Disabled;
            this.Importance = (sbyte)vfxEmitterDefinitionData.Importance;

            this.Components.Add(new BirthEmitterComponent(system, vfxEmitterDefinitionData));
            this.Components.Add(new PositionEmitterComponent(system, vfxEmitterDefinitionData));
            this.Components.Add(new RotationEmitterComponent(system, vfxEmitterDefinitionData));
            this.Components.Add(new ScaleEmitterComponent(system, vfxEmitterDefinitionData));
            this.Components.Add(new TextureEmitterComponent(system, vfxEmitterDefinitionData));
            this.Components.Add(new RenderEmitterComponent(system, vfxEmitterDefinitionData));

            try
            {
                MemoryStream imageStream = new MemoryStream(File.ReadAllBytes(BinUtilities.ResolveAssetPath(system.BinPath, vfxEmitterDefinitionData.Texture)));
                this.PreviewImage = new ImageEngineImage(imageStream).GetWPFBitmap(ShowAlpha:true);
            }
            catch (Exception) { }
        }

        private void OnAddComponent(object o)
        {

        }
    }
}

using LeagueToolkit.Meta.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scavenger.MVVM.ViewModels.Meta.Structures
{
    public class VfxProjectionDefinitionViewModel : PropertyNotifier
    {
        public float Fading
        {
            get => this._fading;
            set
            {
                this._fading = value;
                NotifyPropertyChanged();
            }
        }
        public float YRange
        {
            get => this._yRange;
            set
            {
                this._yRange = value;
                NotifyPropertyChanged();
            }
        }

        private float _fading;
        private float _yRange;

        public VfxProjectionDefinitionViewModel(VfxProjectionDefinitionData projection)
        {
            this.Fading = projection.Fading;
            this.YRange = projection.YRange;
        }

        public VfxProjectionDefinitionData ToVfxProjectionDefinitionData()
        {
            return new VfxProjectionDefinitionData()
            {
                Fading = this.Fading,
                YRange = this.YRange
            };
        }
    }
}

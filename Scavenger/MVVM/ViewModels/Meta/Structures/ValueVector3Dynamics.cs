using LeagueToolkit.Meta.Classes;
using Scavenger.MVVM.Commands;
using Scavenger.MVVM.ViewModels.PrimitiveStructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Text;
using System.Windows.Input;

namespace Scavenger.MVVM.ViewModels.Meta.Structures
{
    public class ValueVector3Dynamics : PropertyNotifier
    {
        public ObservableCollection<ValueVector3DynamicsKey> Keys
        {
            get => this._keys;
            set
            {
                this._keys = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<ValueVector3DynamicsKey> _keys = new();
        private VfxProbabilityTableDataViewModel[] _probabilityTables = new VfxProbabilityTableDataViewModel[3];

        public ICommand AddKeyCommand => new RelayCommand(OnAddKey);

        public ValueVector3Dynamics(VfxAnimatedVector3fVariableData dynamics)
        {
            if(dynamics is not null)
            {
                for (int i = 0; i < dynamics.Times.Count; i++)
                {
                    this.Keys.Add(new ValueVector3DynamicsKey(dynamics.Times[i], dynamics.Values[i]));
                }
            }
        }

        private void OnAddKey(object o)
        {
            this.Keys.Add(new ValueVector3DynamicsKey(0f, new Vector3()));
        }

        public VfxAnimatedVector3fVariableData ToVfxAnimatedVector3fVariableData()
        {
            return new VfxAnimatedVector3fVariableData() 
            {
                
            };
        }
    }

    public class ValueVector3DynamicsKey : PropertyNotifier
    {
        public float Time
        {
            get => this._time;
            set
            {
                this._time = value;
                NotifyPropertyChanged();
            }
        }
        public Vector3ViewModel Value
        {
            get => this._value;
            set
            {
                this._value = value;
                NotifyPropertyChanged();
            }
        }

        private float _time;
        private Vector3ViewModel _value;

        public ValueVector3DynamicsKey(float time, Vector3 value)
        {
            this.Time = time;
            this.Value = new Vector3ViewModel(value);
        }
    }
}

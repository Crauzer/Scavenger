using LeagueToolkit.Meta;
using LeagueToolkit.Meta.Classes;
using Scavenger.MVVM.Commands;
using Scavenger.MVVM.ViewModels.PrimitiveStructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public VfxProbabilityTableDataViewModel ProbabilityTableX
        {
            get => this._probabilityTables[0];
            set
            {
                this._probabilityTables[0] = value;
                NotifyPropertyChanged();
            }
        }
        public VfxProbabilityTableDataViewModel ProbabilityTableY
        {
            get => this._probabilityTables[1];
            set
            {
                this._probabilityTables[1] = value;
                NotifyPropertyChanged();
            }
        }
        public VfxProbabilityTableDataViewModel ProbabilityTableZ
        {
            get => this._probabilityTables[2];
            set
            {
                this._probabilityTables[2] = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<ValueVector3DynamicsKey> _keys = new();
        private VfxProbabilityTableDataViewModel[] _probabilityTables = new VfxProbabilityTableDataViewModel[3]
        {
            new VfxProbabilityTableDataViewModel(),
            new VfxProbabilityTableDataViewModel(),
            new VfxProbabilityTableDataViewModel()
        };

        public ICommand AddKeyCommand => new RelayCommand(OnAddKey);
        public ICommand RemoveKeyCommand => new RelayCommand(OnRemoveKey);

        public ValueVector3Dynamics() { }
        public ValueVector3Dynamics(VfxAnimatedVector3fVariableData dynamics)
        {
            if (dynamics is not null)
            {
                for (int i = 0; i < dynamics.Times.Count; i++)
                {
                    this.Keys.Add(new ValueVector3DynamicsKey(dynamics.Times[i], dynamics.Values[i]));
                }

                for (int i = 0; i < dynamics.ProbabilityTables.Count; i++)
                {
                    this._probabilityTables[i] = new VfxProbabilityTableDataViewModel(dynamics.ProbabilityTables[i]);
                }
            }
        }

        private void OnAddKey(object o)
        {
            this.Keys.Add(new ValueVector3DynamicsKey(0f, new Vector3()));
        }
        private void OnRemoveKey(object o)
        {
            if (o is ValueVector3DynamicsKey key)
            {
                this.Keys.Remove(key);
            }
        }

        public VfxAnimatedVector3fVariableData ToVfxAnimatedVector3fVariableData()
        {
            return new VfxAnimatedVector3fVariableData()
            {
                Times = new MetaContainer<float>(this.Keys.Select(x => x.Time).ToList()),
                Values = new MetaContainer<Vector3>(this.Keys.Select(x => x.Value.ToVector()).ToList()),
                ProbabilityTables = new MetaContainer<VfxProbabilityTableData>(this._probabilityTables.Select(x => x.ToVfxProbabilityTableData()).ToList())
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

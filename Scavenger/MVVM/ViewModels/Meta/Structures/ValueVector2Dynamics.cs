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
    public class ValueVector2Dynamics : PropertyNotifier
    {
        public ObservableCollection<ValueVector2DynamicsKey> Keys
        {
            get => this._keys;
            set
            {
                this._keys = value;
                NotifyPropertyChanged();
            }
        }

        public VfxProbabilityTableViewModel ProbabilityTableX
        {
            get => this._probabilityTables[0];
            set
            {
                this._probabilityTables[0] = value;
                NotifyPropertyChanged();
            }
        }
        public VfxProbabilityTableViewModel ProbabilityTableY
        {
            get => this._probabilityTables[1];
            set
            {
                this._probabilityTables[1] = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<ValueVector2DynamicsKey> _keys = new();
        private VfxProbabilityTableViewModel[] _probabilityTables = new VfxProbabilityTableViewModel[2]
        {
            new VfxProbabilityTableViewModel(),
            new VfxProbabilityTableViewModel()
        };

        public ICommand AddKeyCommand => new RelayCommand(OnAddKey);
        public ICommand RemoveKeyCommand => new RelayCommand(OnRemoveKey);

        public ValueVector2Dynamics() { }
        public ValueVector2Dynamics(VfxAnimatedVector2fVariableData dynamics)
        {
            if (dynamics is not null)
            {
                for (int i = 0; i < dynamics.Times.Count; i++)
                {
                    this.Keys.Add(new ValueVector2DynamicsKey(dynamics.Times[i], dynamics.Values[i]));
                }

                for (int i = 0; i < dynamics.ProbabilityTables.Count; i++)
                {
                    this._probabilityTables[i] = new VfxProbabilityTableViewModel(dynamics.ProbabilityTables[i]);
                }
            }
        }

        private void OnAddKey(object o)
        {
            this.Keys.Add(new ValueVector2DynamicsKey(0f, new Vector2()));
        }
        private void OnRemoveKey(object o)
        {
            if (o is ValueVector2DynamicsKey key)
            {
                this.Keys.Remove(key);
            }
        }

        public VfxAnimatedVector2fVariableData ToVfxAnimatedVector2fVariableData()
        {
            return new VfxAnimatedVector2fVariableData()
            {
                Times = new MetaContainer<float>(this.Keys.Select(x => x.Time).ToList()),
                Values = new MetaContainer<Vector2>(this.Keys.Select(x => x.Value.ToVector()).ToList()),
                ProbabilityTables = new MetaContainer<VfxProbabilityTableData>(this._probabilityTables.Select(x => x.ToVfxProbabilityTableData()).ToList())
            };
        }
    }

    public class ValueVector2DynamicsKey : PropertyNotifier
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
        public Vector2ViewModel Value
        {
            get => this._value;
            set
            {
                this._value = value;
                NotifyPropertyChanged();
            }
        }

        private float _time;
        private Vector2ViewModel _value;

        public ValueVector2DynamicsKey(float time, Vector2 value)
        {
            this.Time = time;
            this.Value = new Vector2ViewModel(value);
        }
    }
}

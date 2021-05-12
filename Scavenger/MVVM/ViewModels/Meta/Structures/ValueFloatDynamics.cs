using LeagueToolkit.Meta;
using LeagueToolkit.Meta.Classes;
using Scavenger.MVVM.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Scavenger.MVVM.ViewModels.Meta.Structures
{
    public class ValueFloatDynamics : PropertyNotifier
    {
        public ObservableCollection<ValueFloatDynamicsKey> Keys
        {
            get => this._keys;
            set
            {
                this._keys = value;
                NotifyPropertyChanged();
            }
        }

        public VfxProbabilityTableViewModel ProbabilityTable
        {
            get => this._probabilityTables[0];
            set
            {
                this._probabilityTables[0] = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<ValueFloatDynamicsKey> _keys = new();
        private VfxProbabilityTableViewModel[] _probabilityTables = new VfxProbabilityTableViewModel[1]
        {
            new VfxProbabilityTableViewModel()
        };

        public ICommand AddKeyCommand => new RelayCommand(OnAddKey);
        public ICommand RemoveKeyCommand => new RelayCommand(OnRemoveKey);

        public ValueFloatDynamics() { }
        public ValueFloatDynamics(VfxAnimatedFloatVariableData dynamics)
        {
            if (dynamics is not null)
            {
                for (int i = 0; i < dynamics.Times.Count; i++)
                {
                    this.Keys.Add(new ValueFloatDynamicsKey(dynamics.Times[i], dynamics.Values[i]));
                }

                for (int i = 0; i < dynamics.ProbabilityTables.Count; i++)
                {
                    this._probabilityTables[i] = new VfxProbabilityTableViewModel(dynamics.ProbabilityTables[i]);
                }
            }
        }

        private void OnAddKey(object o)
        {
            this.Keys.Add(new ValueFloatDynamicsKey(0f, 0f));
        }
        private void OnRemoveKey(object o)
        {
            if (o is ValueFloatDynamicsKey key)
            {
                this.Keys.Remove(key);
            }
        }

        public VfxAnimatedFloatVariableData ToVfxAnimatedFloatVariableData()
        {
            return new VfxAnimatedFloatVariableData()
            {
                Times = new MetaContainer<float>(this.Keys.Select(x => x.Time).ToList()),
                Values = new MetaContainer<float>(this.Keys.Select(x => x.Value).ToList()),
                ProbabilityTables = new MetaContainer<VfxProbabilityTableData>(this._probabilityTables.Select(x => x.ToVfxProbabilityTableData()).ToList())
            };
        }
    }

    public class ValueFloatDynamicsKey : PropertyNotifier
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
        public float Value
        {
            get => this._value;
            set
            {
                this._value = value;
                NotifyPropertyChanged();
            }
        }

        private float _time;
        private float _value;

        public ValueFloatDynamicsKey(float time, float value)
        {
            this.Time = time;
            this.Value = value;
        }
    }
}

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
    public class ValueColorDynamics : PropertyNotifier
    {
        public ObservableCollection<ValueColorDynamicsKey> Keys
        {
            get => this._keys;
            set
            {
                this._keys = value;
                NotifyPropertyChanged();
            }
        }

        public VfxProbabilityTableViewModel ProbabilityTableR
        {
            get => this._probabilityTables[0];
            set
            {
                this._probabilityTables[0] = value;
                NotifyPropertyChanged();
            }
        }
        public VfxProbabilityTableViewModel ProbabilityTableG
        {
            get => this._probabilityTables[1];
            set
            {
                this._probabilityTables[1] = value;
                NotifyPropertyChanged();
            }
        }
        public VfxProbabilityTableViewModel ProbabilityTableB
        {
            get => this._probabilityTables[2];
            set
            {
                this._probabilityTables[2] = value;
                NotifyPropertyChanged();
            }
        }
        public VfxProbabilityTableViewModel ProbabilityTableA
        {
            get => this._probabilityTables[3];
            set
            {
                this._probabilityTables[3] = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<ValueColorDynamicsKey> _keys = new();
        private VfxProbabilityTableViewModel[] _probabilityTables = new VfxProbabilityTableViewModel[4]
        {
            new VfxProbabilityTableViewModel(),
            new VfxProbabilityTableViewModel(),
            new VfxProbabilityTableViewModel(),
            new VfxProbabilityTableViewModel()
        };

        public ICommand AddKeyCommand => new RelayCommand(OnAddKey);
        public ICommand RemoveKeyCommand => new RelayCommand(OnRemoveKey);

        public ValueColorDynamics() { }
        public ValueColorDynamics(VfxAnimatedColorVariableData dynamics)
        {
            if (dynamics is not null)
            {
                for (int i = 0; i < dynamics.Times.Count; i++)
                {
                    this.Keys.Add(new ValueColorDynamicsKey(dynamics.Times[i], dynamics.Values[i]));
                }

                for (int i = 0; i < dynamics.ProbabilityTables.Count; i++)
                {
                    this._probabilityTables[i] = new VfxProbabilityTableViewModel(dynamics.ProbabilityTables[i]);
                }
            }
        }

        private void OnAddKey(object o)
        {
            this.Keys.Add(new ValueColorDynamicsKey(0f, new Vector4()));
        }
        private void OnRemoveKey(object o)
        {
            if (o is ValueColorDynamicsKey key)
            {
                this.Keys.Remove(key);
            }
        }

        public VfxAnimatedColorVariableData ToVfxAnimatedColorVariableData()
        {
            return new VfxAnimatedColorVariableData()
            {
                Times = new MetaContainer<float>(this.Keys.Select(x => x.Time).ToList()),
                Values = new MetaContainer<Vector4>(this.Keys.Select(x => x.Value.ToVector4()).ToList()),
                ProbabilityTables = new MetaContainer<VfxProbabilityTableData>(this._probabilityTables.Select(x => x.ToVfxProbabilityTableData()).ToList())
            };
        }
    }

    public class ValueColorDynamicsKey : PropertyNotifier
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
        public ColorViewModel Value
        {
            get => this._value;
            set
            {
                this._value = value;
                NotifyPropertyChanged();
            }
        }

        private float _time;
        private ColorViewModel _value;

        public ValueColorDynamicsKey(float time, Vector4 value)
        {
            this.Time = time;
            this.Value = new ColorViewModel(value);
        }
    }
}

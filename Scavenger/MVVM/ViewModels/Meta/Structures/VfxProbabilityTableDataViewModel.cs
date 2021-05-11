using LeagueToolkit.Meta;
using LeagueToolkit.Meta.Classes;
using Scavenger.MVVM.Commands;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Scavenger.MVVM.ViewModels.Meta.Structures
{
    public class VfxProbabilityTableDataViewModel : PropertyNotifier
    {
        public float SingleValue
        {
            get => this._singleValue;
            set
            {
                this._singleValue = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<VfxProbabilityTableDataKeyViewModel> Keys
        {
            get => this._keys;
            set
            {
                this._keys = value;
                NotifyPropertyChanged();
            }
        }

        private float _singleValue;
        private ObservableCollection<VfxProbabilityTableDataKeyViewModel> _keys = new();

        public ICommand AddKeyCommand => new RelayCommand(OnAddKey);
        public ICommand RemoveKeyCommand => new RelayCommand(OnRemoveKey);

        public VfxProbabilityTableDataViewModel() { }
        public VfxProbabilityTableDataViewModel(VfxProbabilityTableData probabilityTableData)
        {
            if (probabilityTableData is not null)
            {
                this.SingleValue = probabilityTableData.SingleValue;

                for (int i = 0; i < probabilityTableData.KeyTimes.Count; i++)
                {
                    this.Keys.Add(new VfxProbabilityTableDataKeyViewModel(probabilityTableData.KeyTimes[i], probabilityTableData.KeyValues[i]));
                }
            }
        }

        private void OnAddKey(object parameter)
        {
            this.Keys.Add(new VfxProbabilityTableDataKeyViewModel(0f, 0f));
        }
        private void OnRemoveKey(object parameter)
        {
            if (parameter is VfxProbabilityTableDataKeyViewModel key)
            {
                this.Keys.Remove(key);
            }
        }

        public VfxProbabilityTableData ToVfxProbabilityTableData()
        {
            return new VfxProbabilityTableData()
            {
                SingleValue = this.SingleValue,
                KeyTimes = new MetaContainer<float>(this.Keys.Select(x => x.Time).ToList()),
                KeyValues = new MetaContainer<float>(this.Keys.Select(x => x.Value).ToList())
            };
        }
    }

    public class VfxProbabilityTableDataKeyViewModel : PropertyNotifier
    {
        public float Time
        {
            get => this._keyTime;
            set
            {
                this._keyTime = value;
                NotifyPropertyChanged();
            }
        }
        public float Value
        {
            get => this._keyValue;
            set
            {
                this._keyValue = value;
                NotifyPropertyChanged();
            }
        }

        private float _keyTime;
        private float _keyValue;

        public VfxProbabilityTableDataKeyViewModel(float keyTime, float keyValue)
        {
            this.Time = keyTime;
            this.Value = Value;
        }
    }
}

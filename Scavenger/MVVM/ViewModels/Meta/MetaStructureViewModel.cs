namespace Scavenger.MVVM.ViewModels.Meta
{
    public class MetaStructureViewModel<T> : PropertyNotifier, ISomeableProperty where T : PropertyNotifier, new()
    {
        public bool IsSome
        {
            get => this._isSome;
            set
            {
                this._isSome = value;
                NotifyPropertyChanged();
            }
        }
        public T Structure
        {
            get => this._structure;
            set
            {
                this._structure = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isSome;
        private T _structure;
    
        public MetaStructureViewModel()
        {
            this.IsSome = false;
            this.Structure = new T();
        }
        public MetaStructureViewModel(T structure)
        {
            this.IsSome = structure is not null;
            this.Structure = structure;
        }
    }

    public interface ISomeableProperty
    {

    }
}

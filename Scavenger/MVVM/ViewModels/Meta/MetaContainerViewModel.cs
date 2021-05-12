using LeagueToolkit.Meta;
using Scavenger.MVVM.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Scavenger.MVVM.ViewModels.Meta
{
    public class MetaContainerViewModel<T> : PropertyNotifier where T : new()
    {
        public ObservableCollection<T> Items
        {
            get => this._items;
            set
            {
                this._items = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<T> _items = new();

        public ICommand AddItemCommand => new RelayCommand(OnAddItem);
        public ICommand RemoveItemCommand => new RelayCommand(OnRemoveItem);

        public MetaContainerViewModel() { }
        public MetaContainerViewModel(IEnumerable<T> container)
        {
            this.Items = new ObservableCollection<T>(container);
        }

        private void OnAddItem(object parameter)
        {
            this.Items.Add(new T());
        }
        private void OnRemoveItem(object parameter)
        {
            if (parameter is T item)
            {
                this.Items.Remove(item);
            }
        }

        public MetaContainer<T> ToContainer()
        {
            return new MetaContainer<T>(this.Items);
        }
    }
}

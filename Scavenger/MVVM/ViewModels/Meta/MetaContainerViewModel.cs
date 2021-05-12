using LeagueToolkit.Meta;
using Scavenger.MVVM.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Scavenger.MVVM.ViewModels.Meta
{
    public class MetaContainerViewModel<T> : PropertyNotifier
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
        private Func<T> _itemGenerator;

        public ICommand AddItemCommand => new RelayCommand(OnAddItem);
        public ICommand RemoveItemCommand => new RelayCommand(OnRemoveItem);

        public MetaContainerViewModel(Func<T> itemGenerator) 
        {
            this._itemGenerator = itemGenerator;
        }
        public MetaContainerViewModel(IEnumerable<T> container, Func<T> itemGenerator) : this(itemGenerator)
        {
            this.Items = new ObservableCollection<T>(container);
        }

        private void OnAddItem(object parameter)
        {
            this.Items.Add(this._itemGenerator());
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
        public MetaContainer<I> ToContainer<I>(Func<T, I> itemTransformer)
        {
            return new MetaContainer<I>(this.Items.Select(itemTransformer).ToList());
        }
    }
}

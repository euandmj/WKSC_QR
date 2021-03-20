using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Threading;

namespace Core.Collections
{
    public class SortedObservableSet<T>
       : ICollection<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly SortedSet<T> _innerSet;
        private readonly Dispatcher _dispatcher;

        public int Count => ((ICollection<T>)this._innerSet).Count;

        public bool IsReadOnly => ((ICollection<T>)this._innerSet).IsReadOnly;

        public SortedObservableSet(IComparer<T> comp, Dispatcher disp = null)
        {
            _dispatcher = disp;
            _innerSet = new SortedSet<T>(comp);
        }


        public void Add(T item)
        {
            ((ICollection<T>)this._innerSet).Add(item);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        public void Clear()
        {
            ((ICollection<T>)this._innerSet).Clear();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public bool Contains(T item)
        {
            return ((ICollection<T>)this._innerSet).Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ((ICollection<T>)this._innerSet).CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            if(((ICollection<T>)this._innerSet).Remove(item))
            {
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
                return true;
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)this._innerSet).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this._innerSet).GetEnumerator();
        }
    }
}

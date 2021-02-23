using System.Collections;
using System;
using System.Collections.Generic;

namespace Core
{
    /// <summary>
    /// A collection which behaves like a <see cref="Stack{T}"/> other than that when pushing an 
    /// object into this stack, if the object is already contained within the stack then the object will be removed and placed
    /// at the topmost index
    /// Has performance of <see cref="List{T}"/> and NOT <see cref="HashSet{T}"/>
    /// </summary>
    public class HashStack<T>
        : IEnumerable<T>, IReadOnlyCollection<T>, ICollection

    {
        protected readonly IList<T> _internalStack = new List<T>();
        private readonly object syncLock = new object();

        public int Count => _internalStack.Count;

        public bool IsReadOnly => true;

        public bool IsSynchronized => false; // TODO: make threadsafe?

        public object SyncRoot => syncLock;

        public HashStack() { }

        public HashStack(IEnumerable<T> items)
        {
            if (items is null) throw new ArgumentNullException(nameof(items));

            foreach (var i in items)
                Push(i);
        }

        public void Push(T item)
        {
            if(_internalStack.Contains(item))
            {
                _internalStack.Remove(item);
            }
            _internalStack.Insert(0, item);
        }

        public T Pop()
        {
            if (_internalStack.Count == 0) return default;

            T item = Peek();

            _internalStack.Remove(item);
            return item;
        }

        public bool Remove(T item)
        {
            return _internalStack.Remove(item);
        }

        public void Clear() => _internalStack.Clear();

        public T Peek()
        {
            if (_internalStack.Count == 0) return default;
            if (_internalStack is IList<T> list) return list[0];
            else
            {
                using var e = _internalStack.GetEnumerator();
                if (e.MoveNext()) return e.Current;
            }
            return default;
        }

        public T this[int i]
        {
            get => _internalStack[i];
        }


        public IEnumerator<T> GetEnumerator()
        {
            return this._internalStack.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this._internalStack).GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            for (int i = 0; i < array.Length && i < this.Count; i++)
            {
                array.SetValue(this[i], i);
            }
        }
    }
}

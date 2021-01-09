using System;
using System.Collections;
using System.Collections.Generic;

namespace Core
{
    public class HashStack<T>
        : IEnumerable<T>

    {
        protected readonly ISet<T> _internalStack = new HashSet<T>();

        public int Count => _internalStack.Count;

        public bool IsReadOnly => true;

        public void Push(T item)
        {
            if(_internalStack.Contains(item))
            {
                _internalStack.Remove(item);
            }
            if (!_internalStack.Add(item))
                throw new Exception("adding item failed");
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
            if (_internalStack is IList<T> list) return list[0];
            else
            {
                using var e = _internalStack.GetEnumerator();
                if (e.MoveNext()) return e.Current;
            }
            return default;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)this._internalStack).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this._internalStack).GetEnumerator();
        }
    }
}

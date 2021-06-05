using System;
using System.Collections.Generic;

namespace Data.Providers
{
    public interface IProvider<TItem, in TKey>
    {
        TItem GetItem(TKey key);
        IEnumerable<TItem> GetItems(); 
        IEnumerable<TItem> GetItems(Func<TItem, bool> filter);
    }
}

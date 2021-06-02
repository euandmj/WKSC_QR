using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Services
{

    public interface IDataStore<TItem, in TKey>
    {
        event NewDataEvent<TItem> Refreshed;
        TItem GetItem(TKey key);
        Task<TItem> GetItemAsync(TKey id);
        IEnumerable<TItem> GetItems();
        IEnumerable<TItem> GetItems(Func<TItem, bool> filter);
        Task<IEnumerable<TItem>> GetItemsAsync(); 
        Task<IEnumerable<TItem>> GetItemsAsync(Func<TItem, bool> filter);
        void AddItem(TItem item);
        void DeleteItem(TKey id);
    }

    public delegate void NewDataEvent<TItem>(EventArgs e, Func<IEnumerable<TItem>> func);
}

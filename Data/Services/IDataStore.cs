using Data.Providers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Services
{

    public interface IDataStore<TItem, in TKey> : IProvider<TItem, TKey>
    {
        event NewDataEvent<TItem> Refreshed;
        Task<TItem> GetItemAsync(TKey id);
        Task<IEnumerable<TItem>> GetItemsAsync(); 
        Task<IEnumerable<TItem>> GetItemsAsync(Func<TItem, bool> filter);
        void AddItem(TItem item);
        void DeleteItem(TKey id);
    }

    public delegate void NewDataEvent<TItem>(EventArgs e, Func<IEnumerable<TItem>> func);
}

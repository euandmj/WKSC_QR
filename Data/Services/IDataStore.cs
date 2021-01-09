using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Services
{
    public interface IDataStore<T>
    {        
        Task<T> GetItemAsync(Guid id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        Task<IEnumerable<T>> GetRecentItemsAsync();
        void AddRecentItem(T item);
        void DeleteRecentItem(Guid id);
    }
}

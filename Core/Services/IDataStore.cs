using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IDataStore<T>
    {        
        Task<T> GetItemAsync(Guid id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        Task<IEnumerable<T>> GetRecentItemsAsync();
        Task<bool> AddRecentItemAsync(T item);
        Task<bool> UpdateRecentItemAsync(T item);
        Task<bool> DeleteRecentItemAsync(Guid id);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Services
{
    public interface IDataStore<T>
    {        
        Task<T> GetItem(Guid id);
        Task<IEnumerable<T>> GetItems();
        void AddItem(T item);
        void DeleteItem(Guid id);
    }
}

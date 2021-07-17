using Core;
using Core.Models;
using Data.CSV;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Services
{
    // TODO: add writes
    public class CsvBackedYardItemDataStore
        : IDataStore<YardItem, string>, IDisposable
    {
        private IList<YardItem> _cache;
        private bool disposedValue;
        private readonly FileWatchingCSVAdapter<YardItem> _csvStore;
        public event NewDataEvent<YardItem> Refreshed;

        public CsvBackedYardItemDataStore(
            string filename,
            IColumnSchema columnSchema)
        {
            _csvStore = new FileWatchingCSVAdapter<YardItem>(new YardItemCSVSchema(columnSchema), filename);            
            
            SetCache(_csvStore.GetItems());

            _csvStore.ChangedEvent += (e, func) =>
            {
                //TODO: enumerates twice. dont like.
                SetCache(func());
                Refreshed?.Invoke(e, func);
            };
        }

        public void Reinitialise()
        {
            //if (_csvStore is null) return;
            //_csvStore.Dispose();
            //_csvStore = new FileWatchingCSVAdapter<YardItem>(new YardItemCSVSchema(columnSchema), filename);

            //SetCache(_csvStore.GetItems());
        }
        
        private void SetCache(IEnumerable<YardItem> items)
        {            
            _cache = _csvStore.GetItemsWithLastUpdated().Select(x =>
            {
                x.item.LastUpdated = x.lastUpdated; // padding datetime into YardItem is necessary here because _csvStore treats it as T. 
                return x.item;
            }).ToList();
        }

        public YardItem GetItem(string id)
        {
            return null;
        }

        public async Task<YardItem> GetItemAsync(string id)
        {
            return null;
            //return await Task.FromResult(null);
        }

        public async Task<IEnumerable<YardItem>> GetItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<YardItem>> GetItemsAsync(Func<YardItem, bool> filter)
        {
            throw new NotImplementedException();
        }

        public void AddItem(YardItem item)
        {
            throw new NotImplementedException();
        }

        public void DeleteItem(string id)
        {
            throw new NotImplementedException();
        }

        public YardItem GetItem()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<YardItem> GetItems()
        {
            return _cache;
        }

        public IEnumerable<YardItem> GetItems(Func<YardItem, bool> filter)
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _csvStore.Dispose();
                    _cache = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~CsvBackedYardItemDataStore()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

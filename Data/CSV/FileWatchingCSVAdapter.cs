using Core;
using Data.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Data.CSV
{
    public class FileWatchingCSVAdapter<T>
        : AdapterCSV<T> where T : IEquatable<T> 
    {
        public event NewDataEvent<T> ChangedEvent;        

        private readonly FileSystemWatcher _fsWatcher;
        private readonly IDictionary<int, DateTime> _rowLastUpdated;

        public FileWatchingCSVAdapter(ICsvSchema<T> schema, string path)
            : base(schema, path)
        {

            base.ReadCSV();

            _rowLastUpdated = new Dictionary<int, DateTime>(base._dataTable.Rows.Count);

            _fsWatcher = new FileSystemWatcher(Path.GetDirectoryName(path))
            {
                EnableRaisingEvents = true,
                NotifyFilter = NotifyFilters.Attributes
                                  | NotifyFilters.CreationTime
                                  | NotifyFilters.DirectoryName
                                  | NotifyFilters.FileName
                                  | NotifyFilters.LastAccess
                                  | NotifyFilters.LastWrite
                                  | NotifyFilters.Security
                                  | NotifyFilters.Size,
                Filter = Path.GetFileName(path)
            }; 
            _fsWatcher.Changed += (s, e) => RefreshStore();
        }

        public DateTime LastUpdated { get; private set; }

        private void RefreshStore()
        {
            try
            {
                using var tempAdapter = new AdapterCSV<T>(_schema, _path);
                tempAdapter.ReadCSV();
                using var newDt = tempAdapter.CopyDataTable();

                foreach (var row in this._dataTable.CompareTables(newDt))
                {
                    _rowLastUpdated[row] = DateTime.Now;
                }

                this._dataTable.Dispose();
                this._dataTable = newDt;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                ChangedEvent?.Invoke(EventArgs.Empty, GetRecentlyChanged);
                LastUpdated = DateTime.Now;
            }
        }

        public IEnumerable<(T item, DateTime lastUpdated)> GetItemsWithLastUpdated()
        {
            return
                base.GetItems()
                .Select((x, i) =>
                {
                    _ = _rowLastUpdated.TryGetValue(i, out var lastUpdated);
                    return (x, lastUpdated);
                });
        }

        public IEnumerable<T> GetRecentlyChanged()
        {
            var rows = _rowLastUpdated.Where(x => x.Value > LastUpdated).ToList();
            rows.Sort((a, b) => DateTime.Compare(a.Value, b.Value));


            foreach(var row in rows)
            {
                yield return _schema.ParseRow(_dataTable.Rows[row.Key]);
            }
        }
    }
}

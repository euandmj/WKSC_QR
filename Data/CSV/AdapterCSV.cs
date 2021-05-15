using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace Data.CSV
{
    public class AdapterCSV<T>
        : IDisposable where T : IEquatable<T>
    {
        private const char DELIMITER = ',';
        /*
         * Parse CSV to DataTable
         * Parse DataTable to YardItem[] 
         */
        protected DataTable _dataTable;
        protected ICsvSchema<T> _schema;
        protected readonly string _path;
        public char Delimiter = DELIMITER;
        private bool disposedValue;

        public AdapterCSV(ICsvSchema<T> schema, string path)
        {
            if (schema.AssociatedType != typeof(T))
                throw new ArgumentException("T and schema type mismatch", nameof(schema));
            _path = path ?? throw new ArgumentNullException(nameof(path));

            _dataTable = new DataTable();
            _schema = schema;

            foreach(var columnName in schema.ColumnSchema.Columns.Keys)
            {
                _dataTable.Columns.Add(columnName);
            }
        }

        public void ReadCSV()
        {
            try
            { 
                using var fs = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var reader = new StreamReader(fs, System.Text.Encoding.Default);
                //string[] cells = null;

                //if (!_schema.Equals(cells))
                //    throw new InvalidDataException("csv file does not match the schema");
                
                while (!reader.EndOfStream)
                {
                    var cells = GetNextLine(reader);

                    bool validLine = cells.Any(x => !string.IsNullOrWhiteSpace(x));

                    if (validLine)
                    {
                        var items = _schema.ParseCells(cells);

                        if (items != null)
                        {
                            _dataTable.Rows.Add(items);
                        }
                    }

                }
            }
            catch (IOException ex)
            {
                throw new CSVFailedToLoadException(ex.Message, ex);
            }
        }

        public virtual IEnumerable<T> GetItems()
        {
            try
            {
                return _schema.ParseTable(_dataTable);
            }
            catch(Exception ex)
            {
                throw new DataSetParseException(ex.Message, ex);

            }
        }

        public IEnumerable<T> GetDiffs(IEnumerable<T> otherItems)
        {
            //var otherItems = _schema.ParseTable(other);
            var thisItems = GetItems();

            foreach(var (other, @this) in otherItems.Zip(thisItems, Tuple.Create))
            {
                if(!@this.Equals(other))
                {
                    //yield return (other, @this);
                    yield return @this;
                }
            }
        }

        public DataTable CopyDataTable() => _dataTable.Copy();

        private string[] GetNextLine(StreamReader stream)
        {
            return stream.ReadLine()?.Split(Delimiter);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _dataTable.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

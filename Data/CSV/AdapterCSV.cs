using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace Data.CSV
{
    public class AdapterCSV<T>
        : IDisposable
    {
        private const char DELIMITER = ',';
        /*
         * Parse CSV to DataTable
         * Parse DataTable to YardItem[] 
         */
        protected DataTable _dataTable;
        protected ICSVSchema<T> _schema;

        public char Delimiter = DELIMITER;
        private bool disposedValue;

        public AdapterCSV(ICSVSchema<T> schema)
        {
            if (schema.AssociatedType != typeof(T))
                throw new ArgumentException("T and schema type mismatch", nameof(schema));


            _dataTable = new DataTable();
            _schema = schema;

            foreach(var columnName in schema.ColumnSchema.Columns.Keys)
            {
                _dataTable.Columns.Add(columnName);
            }
        }


        public void ReadCSV(string path)
        {
            try
            {
                using (var reader = new StreamReader(path))
                {
                    var cells = GetNextLine(reader);

                    //if (!_schema.Equals(cells))
                    //    throw new InvalidDataException("csv file does not match the schema");

                    while (!reader.EndOfStream)
                    {
                        cells = GetNextLine(reader);

                        // detect schema and record into the Schema.
                        // try to stay flexible, assert and and remember ranges: 
                        // i.e. if name is sometimes col 3 or 4, try

                        bool validLine = cells.Any(x => !string.IsNullOrWhiteSpace(x));

                        if (validLine)
                        {
                            var items = _schema.ParseLines(cells);

                            _dataTable.Rows.Add(items);

                        }

                    }
                }
            }
            catch(IOException ex)
            {
                throw new CSVFailedToLoadException(ex.Message, ex);
            }
        }

        public IEnumerable<T> GetItems()
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

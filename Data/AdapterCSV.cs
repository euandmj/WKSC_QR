using Data.Models;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Data
{
    public class AdapterCSV<T>
    {
        private const char DELIMITER = ',';
        /*
         * Parse CSV to DataTable
         * Parse DataTable to YardItem[] 
         */
        protected DataTable _dataTable;
        protected ICSVSchema<T> _schema;

        public char Delimiter = DELIMITER;


        public AdapterCSV(ICSVSchema<T> schema)
        {
            _dataTable = new DataTable();
            _schema = schema;


            foreach(var (columnName, type) in schema.Columns)
            {
                _dataTable.Columns.Add(columnName, type);
            }
        }


        public void ReadCSV(string path)
        {
            using (var reader = new StreamReader(path))
            {
                var cells = GetNextLine(reader);

                if (!_schema.Equals(cells))
                    throw new InvalidDataException("csv file does not match the schema");

                while (!reader.EndOfStream)
                {
                    cells = GetNextLine(reader);

                    var items = _schema.ParseLines(cells);

                    _dataTable.Rows.Add(items);
                }
            }
        }

        public IEnumerable<T> GetItems()
        {
            return _schema.ParseTable(_dataTable);
        }

        private string[] GetNextLine(StreamReader stream)
        {
            return stream.ReadLine()?.Split(Delimiter);
        }
    }
}

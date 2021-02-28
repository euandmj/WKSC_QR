﻿using Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

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
            if (schema.AssociatedType != typeof(T))
                throw new ArgumentException("T and schema type mismatch", nameof(schema));


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

                //if (!_schema.Equals(cells))
                //    throw new InvalidDataException("csv file does not match the schema");

                while (!reader.EndOfStream)
                {
                    cells = GetNextLine(reader);

                    // detect schema and record into the Schema.
                    // try to stay flexible, assert and and remember ranges: 
                    // i.e. if name is sometimes col 3 or 4, try

                    bool validLine = cells.Any(x => !string.IsNullOrWhiteSpace(x));

                    if(validLine)
                    {
                        var items = _schema.ParseLines(cells);

                        _dataTable.Rows.Add(items);

                    }

                }
            }
        }

        public IEnumerable<T> GetItems()
        {
            try
            {
            return _schema.ParseTable(_dataTable);

            }catch(Exception ex)
            {
                return null;

            }
        }

        private string[] GetNextLine(StreamReader stream)
        {
            return stream.ReadLine()?.Split(Delimiter);
        }
    }
}

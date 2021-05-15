using System;
using System.Collections.Generic;
using System.Data;

namespace Data.CSV
{
    public interface ICsvSchema<T>
    {
        Type AssociatedType { get; }
        IColumnSchema ColumnSchema { get; }
        T ParseRow(DataRow dr);
        IEnumerable<T> ParseTable(DataTable dr);
        object[] ParseCells(string[] items);
    }
}
